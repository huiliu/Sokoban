using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic;

namespace Sokoban.Client
{
    public class LevelComponentEx
        : MonoBehaviour
    {
        [SerializeField]
        private GameObject LevelNode;

        public void ShowLevels(int count)
        {
            count = count > 9 ? 9 : count;
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
            /// TODO: 当UI Canvas 发生缩放时，GridLayout的子对象亦会缩放，导致显示不正确
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            this.Nodes.Add(go);

            var c = go.GetComponent<LevelNodeComponent>();
            c.OnSelectLevel += OnSelectLevel;
            c.Setup(id, -1);
        }

        public event Action<int> OnStartGame;
        private void OnSelectLevel(int levelId)
        {
            this.OnStartGame.SafeInvoke(levelId);
        }

        private void RemoveAllNode()
        {
            foreach (var go in this.Nodes)
                Destroy(go);

            this.Nodes.Clear();
        }
    }
}
