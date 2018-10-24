using UnityEngine;
using Logic;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public class PusherComponent
    : SmoothMove
{
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    private SpriteRenderer SpriteRenderer;
    private Animator Animator;
    protected void Start()
    {
        this.Animator = GetComponent<Animator>();
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private Pusher Pusher;
    public void Setup(Pusher pusher)
    {
        this.Pusher = pusher;
    }

    public Direction FaceDirection { get; private set; }
    public void SetFaceDirection(Direction direction)
    {
        this.FaceDirection = direction;
        this.SpriteRenderer.sprite = GetFaceSprint(direction);
    }

    private Sprite GetFaceSprint(Direction direction)
    {
        switch(direction)
        {
            case Direction.Down:
                return this.downSprite;
            case Direction.Left:
                return this.leftSprite;
            case Direction.Right:
                return this.rightSprite;
            case Direction.Up:
                return this.upSprite;
            default:
                Base.Log.Assert(false, "unknow Direction: {0}", direction);
                return null;
        }

    }

    public void Move(Direction direction)
    {
        
    }
}
