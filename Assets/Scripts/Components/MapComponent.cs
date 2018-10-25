using UnityEngine;
using Logic;

public class MapComponent
    : MonoBehaviour
{
    [SerializeField] private GameObject MapNode;
    [SerializeField] private GameObject EntityNode;

    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Floor;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject BoxOnGoal;
    [SerializeField] private GameObject Pusher;
    [SerializeField] private GameObject PusherOnGoal;

    public int TileWidth = 32;
    public int TileHeigh = 32;

    public void SetupMap(Map Map)
    {
        var cells = Map.AllCell;
        foreach(var row in cells)
        {
            foreach (var c in row)
                this.InstantiateCell(c);
        }
    }

    private void InstantiateCell(Cell c)
    {
        this.InitGround(c);
        if (c.Entity != null)
        {
            this.InitEntity(c);
        }
    }

    private void InitGround(Cell c)
    {
        var go = null as GameObject;
        if (c.Type == CellType.Goal)
            go = Instantiate(this.Goal);
        else
            go = Instantiate(this.Floor);

        go.transform.position = c.Position.ToMapLayerPosition();
        go.transform.parent = this.MapNode.transform;
    }

    private void InitEntity(Cell c)
    {
        var e = c.Entity;
        Base.Log.Assert(e != null);

        var go = null as GameObject;
        if (e.Type == EntityType.Wall)
            go = Instantiate(this.Wall);
        else if (e.Type == EntityType.Box)
        {
            go = c.Type == CellType.Floor
                ? Instantiate(this.Box)
                : Instantiate(this.BoxOnGoal);

            go.GetComponent<BoxComponent>().SetBox(e as Box);
        }
        else if (e.Type == EntityType.Pusher)
        {
            go = c.Type == CellType.Floor
                ? Instantiate(this.Pusher)
                : Instantiate(this.PusherOnGoal);

            go.GetComponent<PusherComponent>().Setup(e as Pusher);
        }

        go.transform.position = c.Position.ToEntityLayerPosition();
        go.transform.parent = this.EntityNode.transform;
    }
}
