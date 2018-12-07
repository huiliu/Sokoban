﻿using System.Collections.Generic;
using UnityEngine;
using Logic;

namespace GraphGame.Client
{
    public class LevelComponentEx
        : MonoBehaviour
    {
        [SerializeField]
        private GameObject LevelNode;

        private void ShowLevels()
        {
            var count = MapMgr.Instance.Maps.Count;
            for(var i = 0; i < count; ++i)
            {
                this.AddLevelNode(i);
            }
        }

        private List<GameObject> Nodes = new List<GameObject>();
        private void AddLevelNode(int id)
        {
            var go = Instantiate(this.LevelNode);
            go.transform.SetParent(this.transform);
            go.SetActive(true);
            this.Nodes.Add(go);

            var c = go.GetComponent<LevelNodeComponent>();
            c.Setup(id, -1);
        }

        private void RemoveAllNode()
        {
            foreach (var go in this.Nodes)
                Destroy(go);

            this.Nodes.Clear();
        }

        private void OnEnable()
        {
            this.ShowLevels();
        }

        private void OnDisable()
        {
            this.RemoveAllNode();
        }
    }
}
