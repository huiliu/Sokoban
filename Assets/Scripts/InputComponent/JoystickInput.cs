using UnityEngine;

public class JoystickInput
    : MonoBehaviour
    , IInput
{
    public bool Up { get; protected set; }
    public bool Down { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }

    private void Start()
    {
        InputMgr.Instance.BindVirtualJoystick(this);
    }

    public void OnUp()
    {
        this.Up = true;
    }

    public void OnRight()
    {
        this.Right = true;
    }

    public void OnDown()
    {
        this.Down = true;
    }

    public void OnLeft()
    {
        this.Left = true;
    }

    protected void LateUpdate()
    {
        this.Up = false;
        this.Right = false;
        this.Down = false;
        this.Left = false;
    }
}
