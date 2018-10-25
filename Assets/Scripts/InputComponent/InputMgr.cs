using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InputMgr
    : MonoBehaviour
{
    public bool Up      { get; private set; }
    public bool Down    { get; private set; }
    public bool Left    { get; private set; }
    public bool Right   { get; private set; }

    private JoystickInput JoystickInput;
    public void BindVirtualJoystick(JoystickInput input)
    {
        this.JoystickInput = input;
    }

    protected void Update()
    {
        this.Up = Input.GetKeyDown(KeyCode.W);
        this.Down = Input.GetKeyDown(KeyCode.S);
        this.Left = Input.GetKeyDown(KeyCode.A);
        this.Right = Input.GetKeyDown(KeyCode.D);
    }
}
