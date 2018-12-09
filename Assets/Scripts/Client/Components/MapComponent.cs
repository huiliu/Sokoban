using System;
using System.Collections.Generic;
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

    public event Action<int> OnWin;

    private List<GameObject> Entities = new List<GameObject>();
    public void SetupMap(LevelMap Map)
    {
        var cells = Map.AllCell;
        foreach(var row in cells)
        {
            foreach (var c in row)
                this.InstantiateCell(c);
        }
    }

    public void Reset()
    {
        foreach (var e in this.Entities)
            Destroy(e);

        this.Entities.Clear();
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

        go.transform.localPosition = c.Position.ToMapLayerPosition();
        go.transform.SetParent(this.MapNode.transform, false);

        this.Entities.Add(go);
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

            var pc = go.GetComponent<PusherComponent>();
            pc.Setup(e as Pusher);
            pc.OnWin = this.HandleOnWin;
        }

        go.transform.localPosition = c.Position.ToEntityLayerPosition();
        go.transform.SetParent(this.EntityNode.transform, false);

        this.Entities.Add(go);
    }

    private void HandleOnWin(int c)
    {
        this.OnWin.SafeInvoke(c);
        Base.Log.Info("Game", "You Win!");
    }
}
