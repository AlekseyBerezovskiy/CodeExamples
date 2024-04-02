using System;
using MyServices.UIService.Interfaces;
using UnityEngine;

namespace MyServices.UIService.Realization
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        public EventHandler OnShowEvent { get; set; }
        public EventHandler OnHideEvent { get; set; }
        public abstract void Show();
        public abstract void Hide();
        protected virtual void OnShowEnd() { }
        protected virtual void OnHideEnd() { }
    }
}
