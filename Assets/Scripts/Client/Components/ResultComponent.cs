using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Logic;

namespace Sokoban.Client
{
    public class ResultComponent
        : MonoBehaviour
    {
        public Action OnReturn;
        public Action OnNext;

        [SerializeField] private Button btnReturn;
        [SerializeField] private Button btnNext;
        [SerializeField] private StarComponent StarComponent;

        private void Awake()
        {
            this.btnReturn.onClick.AddListener(() =>
            {
                OnReturn.SafeInvoke();
                this.SetActiveEx(false);
            });

            this.btnNext.onClick.AddListener(() =>
            {
                OnNext.SafeInvoke();
                this.SetActiveEx(false);
            });
        }

        public void Show(int star)
        {
            this.StarComponent.ShowStar(star);
            this.SetActiveEx(true);
        }
    }
}
