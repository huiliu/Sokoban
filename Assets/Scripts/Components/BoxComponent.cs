using UnityEngine;
using Logic;

public class BoxComponent
    : SmoothMove
{
    private Box Box;
    public void SetBox(Box box)
    {
        this.Box = box;
    }

    private void Update()
    {
        if (this.Box != null)
            this.TryMove();
    }

    private void TryMove()
    {
        if (this.CheckMove())
            this.MoveTo(this.Box.Cell.Position.ToEntityLayerPosition());
    }

    private bool CheckMove()
    {
        var targetPos = this.Box.Cell.Position.ToEntityLayerPosition();
        return this.transform.position != targetPos;
    }

    protected override void MoveEnded()
    {
        base.MoveEnded();
    }
}
