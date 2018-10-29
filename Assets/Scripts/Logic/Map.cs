using System;
using System.Collections.Generic;

namespace Logic
{
    public class Map
    {
        private List<List<Cell>> Cells = new List<List<Cell>>();
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Map() { }

        public void SetData(List<List<Cell>> data)
        {
            this.Cells = data;
            this.Height = this.Cells.Count;
            this.Width = 0;
            foreach (var row in this.Cells)
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

        public override string ToString()
        {
            string map = "";
            foreach (var row in this.Cells)
            {
                string r = "";
                foreach (var c in row)
                    r += c.ToString();
                map += (r + "\n");
            }

            return map;
        }

        public List<List<Cell>> AllCell { get { return this.Cells; } }
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

        public int MoveCount;
        public bool Finished
        {
            get
            {
                foreach (var row in this.Cells)
                    foreach (var c in row)
                        if (!c.Arrived)
                            return false;

                return true;
            }
        }
        public Pusher Pusher { get; private set; }

        public Action OnWin;
        public Action OnFailed;
        public void CheckWin()
        {
            if (this.Finished)
            {
                this.OnWin.SafeInvoke();
            }
        }
    }
}
