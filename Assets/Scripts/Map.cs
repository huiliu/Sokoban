using System.Collections.Generic;

public class Map
{

    private List<List<Cell>> Cells = new List<List<Cell>>();
    public int Width;
    public int Height;
    public Map() { }

    public void SetData(List<List<Cell>> data)
    {
        this.Cells = data;
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
