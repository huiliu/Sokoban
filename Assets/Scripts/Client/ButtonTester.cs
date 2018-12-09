using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Base;

namespace Assets.Scripts.Client
{
    public class ButtonTester
        : MonoBehaviour
        , IPointerDownHandler
        , IPointerUpHandler
    {
        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                Log.Error("Message From AddListener!");
            });
        }

        public void OnClick()
        {
            Log.Error("Message From OnClick!");
        }

        private void Update()
        {
            Log.Warning("update", "Message From Update");
        }

        private void LateUpdate()
        {
            Log.Info("lateUpdate", "Message From LateUpdate");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Log.Info("pointerDown", "Message From OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Log.Info("pointerUp", "Message From OnPointerUp");
        }
    }
}
