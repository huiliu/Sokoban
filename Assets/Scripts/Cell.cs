using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum CellType
{
    Floor,
    Goal,
}

public struct Point
{
    public int row;
    public int col;
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
    }

    public bool CanEnter(Entity e)
    {
        if (this.Entity == null)
            return true;

        return false;
    }

    public Entity Entity { get; private set; }
    public void SetEntity(Entity e)
    {
        this.Entity = e;
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
}
