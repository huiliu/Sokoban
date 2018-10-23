public class Pusher
    : Entity
{
    public Pusher(Cell c)
        : base(c)
    {
        this.Type = EntityType.Pusher;
    }

    public override bool MoveDown()
    {
        throw new System.NotImplementedException();
    }

    public override bool MoveLeft()
    {
        throw new System.NotImplementedException();
    }

    public override bool MoveRight()
    {
        throw new System.NotImplementedException();
    }

    public override bool MoveUp()
    {
        throw new System.NotImplementedException();
    }
}
