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

    protected void Update()
    {
        this.Up = Input.GetKey(KeyCode.W);
        this.Down = Input.GetKey(KeyCode.S);
        this.Left = Input.GetKey(KeyCode.A);
        this.Right = Input.GetKey(KeyCode.D);
    }

    //protected void LateUpdate()
    //{
    //    this.ResetCommand();
    //}

    //private void ResetCommand()
    //{
    //    this.Up = false;
    //    this.Down = false;
    //    this.Left = false;
    //    this.Right = false;
    //}
}
