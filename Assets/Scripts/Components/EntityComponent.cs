using Logic;

public class EntityComponent
    : SmoothMove
{
    private Entity Entity;
    public void SetupEntity(Entity entity)
    {

    }

    private void Update()
    {
        this.TryMove();
    }

    private void TryMove()
    {
        if (this.CheckMove())
            this.MoveTo(this.Entity.Cell.Position.ToEntityLayerPosition());
    }

    private bool CheckMove()
    {
        var targetPos = this.Entity.Cell.Position.ToEntityLayerPosition();
        return this.transform.position != targetPos;
    }

    protected override void MoveEnded()
    {
        base.MoveEnded();
    }
}
