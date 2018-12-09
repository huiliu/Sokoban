using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Logic;

namespace Sokoban.Client
{
    [RequireComponent(typeof(SpriteRef))]
    public class LevelNodeComponent
        : MonoBehaviour
        , IPointerDownHandler
    {
        [SerializeField]
        private List<Image> Stars;
        [SerializeField]
        private GameObject StarNode;
        [SerializeField]
        private GameObject QuestionNode;

        private SpriteRef refs;
        private SpriteRef Sprites
        {
            get
            {
                if (this.refs == null)
                    this.refs = this.GetComponent<SpriteRef>();

                return this.refs;
            }
        }

        private int LevelID;
        public void Setup(int levelID, int starValue)
        {
            this.LevelID = levelID;
            var record = UserDataMgr.Instance.GetRecord("Easy", levelID);
            this.InitUserStatus(record.Score);
        }

        private void InitUserStatus(int starValue)
        {
            if (starValue < 0)
            {
                this.ShowStar(false);
                return;
            }

            foreach (var img in this.Stars)
            {
                img.sprite = this.Sprites["Gray"];
            }

            for (var i = 0; i < starValue; ++i)
            {
                this.Stars[i].sprite = this.Sprites["Yellow"];
            }

            this.ShowStar(true);
        }

        private void ShowStar(bool flag)
        {
            this.StarNode.SetActive(flag);
            this.QuestionNode.SetActive(!flag);
        }

        public event Action<int> OnSelectLevel;
        public void OnPointerDown(PointerEventData eventData)
        {
            this.OnSelectLevel.SafeInvoke(this.LevelID);
        }
    }
}
