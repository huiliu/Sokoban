using UnityEngine;
using Logic;

public class JoystickInput
    : IInput
{
    public void OnUp()
    {
        this.Up.SafeInvoke();
    }

    public void OnRight()
    {
        this.Right.SafeInvoke();
    }

    public void OnDown()
    {
        this.Down.SafeInvoke();
    }

    public void OnLeft()
    {
        this.Left.SafeInvoke();
    }
}
