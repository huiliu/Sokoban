using Sokoban.Client;
using System.Collections.Generic;
using UnityEngine;
using Logic;

public class InputComponent
    : MonoBehaviour
{
    [SerializeField]
    private JoystickInput JoystickInput;

    private KeyboardInput KeyboardInput;
    private void Awake()
    {
        this.KeyboardInput = this.GetOrAddComponent<KeyboardInput>();
        this.Commands = new Queue<SokobanCommand>();

        this.InitJoystick();
        this.InitKeyboard();
    }

    private Pusher Pusher;
    public void Setup(Pusher pusher)
    {
        this.Pusher = pusher;
    }

    public void InitJoystick()
    {
        if (this.JoystickInput == null)
            return;

        this.JoystickInput.Up = this.Up;
        this.JoystickInput.Down = this.Down;
        this.JoystickInput.Left = this.Left;
        this.JoystickInput.Right = this.Right;
    }

    public void InitKeyboard()
    {
        this.KeyboardInput.Up = this.Up;
        this.KeyboardInput.Down = this.Down;
        this.KeyboardInput.Left = this.Left;
        this.KeyboardInput.Right = this.Right;
    }

    public SokobanCommand GetCommand()
    {
        if (this.Commands.Count == 0)
            return null;

        return this.Commands.Dequeue();
    }

    private Queue<SokobanCommand> Commands;
    private void Up()
    {
        this.Commands.Enqueue(new SokobanMoveUp(this.Pusher));
    }

    private void Down()
    {
        this.Commands.Enqueue(new SokobanMoveDown(this.Pusher));
    }

    private void Left()
    {
        this.Commands.Enqueue(new SokobanMoveLeft(this.Pusher));
    }

    private void Right()
    {
        this.Commands.Enqueue(new SokobanMoveRight(this.Pusher));
    }
}
