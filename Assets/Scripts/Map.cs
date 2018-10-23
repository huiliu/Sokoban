using System.Collections.Generic;

public class Map
{
    private List<List<Cell>> Cells = new List<List<Cell>>();
    public int Width    { get; private set; }
    public int Height   { get; private set; }
    public Map() { }

    public void SetData(List<List<Cell>> data)
    {
        this.Cells = data;
        this.Height = this.Cells.Count - 1;
        this.Width = 0;
        foreach(var row in this.Cells)
        {
            foreach (var c in row)
            {
                if (this.Pusher == null &&
                    c.Entity != null &&
                    c.Entity.Type == EntityType.Pusher)
                {
                    this.Pusher = c.Entity as Pusher;
                }
            }

            if (this.Width < row.Count)
                this.Width = row.Count;
        }
    }

    public Cell GetCell(Point p)
    {
        return this.GetCell(p.row, p.col);
    }

    public Cell GetCell(int r, int c)
    {
        if (r < 0 || r >= this.Height - 1)
            return null;

        if (c < 0 || c >= this.Width - 1)
            return null;

        return Cells[r][c];
    }

    public bool Finished { get; private set; }
    public Pusher Pusher { get; private set; }
}
