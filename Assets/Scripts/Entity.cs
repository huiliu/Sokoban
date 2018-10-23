using UnityEngine;

public enum EntityType
{
    Wall,
    Floor,
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
}
