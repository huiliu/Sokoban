
public class InputMgr
{
    public bool Up      { get { return this.Keyboard.Up || this.JoystickInput.Up; } }
    public bool Right   { get { return this.Keyboard.Right || this.JoystickInput.Right; } }
    public bool Down    { get { return this.Keyboard.Down || this.JoystickInput.Down; } }
    public bool Left    { get { return this.Keyboard.Left || this.JoystickInput.Left; } }

    private static InputMgr instance = new InputMgr();
    public static InputMgr Instance { get { return instance; } }

    private IInput JoystickInput;
    public void BindVirtualJoystick(JoystickInput input)
    {
        this.JoystickInput = input;
    }

    private IInput Keyboard;
    public void BindKeyboard(IInput keyboard)
    {
        this.Keyboard = keyboard;
    }
}
