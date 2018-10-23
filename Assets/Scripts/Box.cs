
public class Box
    : Entity
{
    public Box(Cell c)
        : base(c)
    {
        this.Type = EntityType.Box;
    }

    public override bool MoveDown()
    {
        return this.MoveTo(this.Cell.GetDownCell());
    }

    public override bool MoveLeft()
    {
        return this.MoveTo(this.Cell.GetLeftCell());
    }

    public override bool MoveRight()
    {
        return this.MoveTo(this.Cell.GetRightCell());
    }

    public override bool MoveUp()
    {
        return this.MoveTo(this.Cell.GetUpCell());
    }

    private bool MoveTo(Cell cell)
    {
        if (cell == null || !cell.CanEnter(this))
            return false;

        this.Cell.ResetEntity();
        this.Cell = cell;
        this.Cell.SetEntity(this);

        return true;
    }
}
