using UnityEngine;
using Logic;

public class MapComponent
    : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Floor;
    [SerializeField] private GameObject Goal;
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject BoxOnGoal;
    [SerializeField] private GameObject Pusher;
    [SerializeField] private GameObject PusherOnGoal;

    public int TileWidth = 64;
    public int TileHeigh = 64;

    public void SetUpMap(Map Map)
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
    }
}
