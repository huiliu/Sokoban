using UnityEngine;

public class KeyboardInput
    : MonoBehaviour
    , IInput
{
    public bool Up { get; protected set; }
    public bool Down { get; protected set; }
    public bool Left { get; protected set; }
    public bool Right { get; protected set; }

    private void Start()
    {
        InputMgr.Instance.BindKeyboard(this);
    }

    private void Update()
    {
        this.Up = Input.GetKeyDown(KeyCode.W);
        this.Down = Input.GetKeyDown(KeyCode.S);
        this.Left = Input.GetKeyDown(KeyCode.A);
        this.Right = Input.GetKeyDown(KeyCode.D);
    }
}
