
namespace Logic
{
    public enum EntityType
    {
        Wall,
        Box,
        Pusher,
    }

    public abstract class Entity
    {
        public Cell Cell { get; protected set; }
        public Entity(Cell cell)
        {
            this.Cell = cell;
        }

        public EntityType Type { get; protected set; }

        public abstract bool MoveUp();
        public abstract bool MoveDown();
        public abstract bool MoveLeft();
        public abstract bool MoveRight();

        public override string ToString()
        {
            return string.Format("Entity[type:{0} Pos: {1}]", this.Type, this.Cell.Position);
        }
    }
}
