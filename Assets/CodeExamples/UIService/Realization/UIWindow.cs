using System;
using UnityEngine;

namespace CodeExamples.UIService
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        public EventHandler OnShowEvent { get; set; }
        public EventHandler OnHideEvent { get; set; }
        public abstract void Show();
        public abstract void Hide();
        public virtual void Dispose() { }
        protected virtual void OnShowEnd() { }
        protected virtual void OnHideEnd() { }
    }
}
