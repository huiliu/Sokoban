using UnityEngine;
using Logic;

public class KeyboardInput
    : IInput
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            this.Up.SafeInvoke();

        if (Input.GetKeyDown(KeyCode.S))
            this.Down.SafeInvoke();

        if (Input.GetKeyDown(KeyCode.A))
            this.Left.SafeInvoke();

        if (Input.GetKeyDown(KeyCode.D))
            this.Right.SafeInvoke();
    }
}
