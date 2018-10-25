namespace Logic
{
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
            var downCell = this.Cell.GetDownCell();
            if (downCell == null)
                return false;

            if (downCell.Entity != null && !downCell.Entity.MoveDown())
                return false;

            return this.MoveTo(downCell);
        }

        public override bool MoveLeft()
        {
            var c = this.Cell.GetLeftCell();
            if (c == null)
                return false;

            if (c.Entity != null && !c.Entity.MoveLeft())
                return false;

            return this.MoveTo(c);
        }

        public override bool MoveRight()
        {
            var c = this.Cell.GetRightCell();
            if (c == null)
                return false;

            if (c.Entity != null && !c.Entity.MoveRight())
                return false;

            return this.MoveTo(c);
        }

        public override bool MoveUp()
        {
            var c = this.Cell.GetUpCell();
            if (c == null)
                return false;

            if (c.Entity != null && !c.Entity.MoveUp())
                return false;

            return this.MoveTo(c);
        }

        private bool MoveTo(Cell c)
        {

            this.Cell.ResetEntity();
            this.Cell = c;
            this.Cell.SetEntity(this);

            this.Cell.Map.CheckWin();

            return true;
        }
    }
}
