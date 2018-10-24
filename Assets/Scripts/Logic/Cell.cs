
namespace Logic
{
    public enum CellType
    {
        Floor,
        Goal,
    }

    public struct Point
    {
        public int row { get; private set; }
        public int col { get; private set; }

        public Point(int r, int c)
        {
            this.row = r;
            this.col = c;
        }
    }

    public class Cell
    {
        public Map Map { get; private set; }
        public CellType Type { get; private set; }
        public Point Position { get; private set; }
        public Cell(Map map, CellType t, Point p)
        {
            this.Map = map;
            this.Type = t;
            this.Position = p;
            this.Arrived = true;
        }

        public bool CanEnter(Entity e)
        {
            if (this.Entity == null)
                return true;

            return false;
        }

        public bool Arrived { get; private set; }
        public Entity Entity { get; private set; }
        public void SetEntity(Entity e)
        {
            this.Entity = e;
            this.Arrived = this.Type == CellType.Floor || (this.Type == CellType.Goal && this.Entity.Type == EntityType.Box);
        }

        public void ResetEntity()
        {
            this.Entity = null;
        }

        public Cell GetDownCell()
        {
            return this.Map.GetCell(this.Position.row + 1, this.Position.col);
        }

        public Cell GetLeftCell()
        {
            return this.Map.GetCell(this.Position.row, this.Position.col - 1);
        }

        public Cell GetRightCell()
        {
            return this.Map.GetCell(this.Position.row, this.Position.col + 1);
        }

        public Cell GetUpCell()
        {
            return this.Map.GetCell(this.Position.row - 1, this.Position.col);
        }

        public override string ToString()
        {
            if (this.Entity == null)
            {
                return this.Type == CellType.Floor
                    ? Utils.Floor.ToString()
                    : Utils.Goal.ToString();
            }
            else
            {
                if (this.Type == CellType.Floor)
                    return this.Entity.Type == EntityType.Box
                        ? Utils.Box.ToString()
                        : this.Entity.Type == EntityType.Wall ? Utils.Wall.ToString() : Utils.Pusher.ToString();
                else
                    return this.Entity.Type == EntityType.Box ? Utils.BoxOnGoal.ToString() : Utils.PusherOnGoal.ToString();
            }
        }
    }
}
