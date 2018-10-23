
public class Wall
    : Entity
{
    public Wall(Cell cell)
        : base(cell)
    {
        this.Type = EntityType.Wall;
    }

    public override bool MoveDown()
    {
        return false;
    }

    public override bool MoveLeft()
    {
        return false;
    }

    public override bool MoveRight()
    {
        return false;
    }

    public override bool MoveUp()
    {
        return false;
    }
}