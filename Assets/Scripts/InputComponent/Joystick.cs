using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick
    : MonoBehaviour
    , IPointerDownHandler
    , IPointerUpHandler
    , IDragHandler
{
    [SerializeField]
    private Transform Pad;
    [SerializeField]
    private GameObject Cursor;
    [SerializeField]
    private Image DirectionArea;
    [SerializeField]
    private int DirectionCount;

    public Vector2 Direction { get; private set; }

    private Canvas Canvas;
    private float padRadius;
    private float DirectionAreaAngle;

    protected void Start()
    {
        this.Canvas = this.gameObject.GetComponentInParent<Canvas>();
        this.padRadius = this.Pad.GetComponent<RectTransform>().rect.width / 2 * this.Canvas.scaleFactor;

        if (this.DirectionCount > 0)
            this.InitDirectionAngle();
    }

    private void InitDirectionAngle()
    {
        this.DirectionAreaAngle = 360.0f / this.DirectionCount;
        this.DirectionArea.fillAmount = 1.0f / this.DirectionCount;
        this.DirectionArea.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.SetBtnPosition(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.SetBtnPosition(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.SetBtnPosition(this.Pad.transform.position);
    }

    private void SetBtnPosition(Vector3 pos)
    {
        var v = pos - this.Pad.transform.position;
        if (v.magnitude > this.padRadius)
            this.Cursor.transform.position = this.Pad.transform.position + v.normalized * this.padRadius;
        else
            this.Cursor.transform.position = pos;

        this.Direction = v;
        if (this.DirectionCount >= 2)
        {
            this.RefreshDirectionArea(v);
            this.Direction = this.TuneDirection(v);
        }
    }

    private Vector3 eulerAngles = Vector3.zero;
    private void RefreshDirectionArea(Vector3 dir)
    {
        this.eulerAngles.z = GetDirectionAreaAngle(dir);
        this.DirectionArea.transform.eulerAngles = this.eulerAngles;

        this.DirectionArea.gameObject.SetActive(dir.sqrMagnitude > Vector2.kEpsilon);
    }

    private Vector2 TuneDirection(Vector2 dir)
    {
        var angle = this.GetDirectionAreaAngle(dir) + 90 - this.DirectionAreaAngle / 2;
        return Vector2.right.Rotate(angle);
        //return dir;
    }

    private float GetDirectionAreaAngle(Vector2 dir)
    {
        var dirAngle = this.GetDirectionAngle(dir);
        var count = Mathf.FloorToInt(dirAngle / this.DirectionAreaAngle + 0.5f);

        return this.DirectionAreaAngle * count - 90 + this.DirectionAreaAngle / 2;
    }

    private float GetDirectionAngle(Vector2 dir)
    {
        var angle = Vector2.Angle(Vector2.right, dir);
        //var cross = Vector2.right.Cross(dir);
        //if (cross < 0)
        if (dir.y < 0)
            angle = 360 - angle;

        return angle;
    }
}
