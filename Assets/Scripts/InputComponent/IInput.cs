using System;
using UnityEngine;

public abstract class IInput
    : MonoBehaviour
{
    public Action Up;
    public Action Down;
    public Action Left;
    public Action Right;
}
