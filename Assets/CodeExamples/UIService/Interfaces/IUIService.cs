using System;
using MyServices.UIService.Realization;
using UnityEngine;

namespace MyServices.UIService.Interfaces 
{
    public interface IUIService
    {
        T Show<T>(int layer = 0) where T : UIWindow;
        void Hide<T>(Action onEnd = null) where T : UIWindow;
        void HideAll(Action onEnd = null);
        T Get<T>() where T : UIWindow;
        void InitWindows(Transform poolDeactiveContiner = null);
        void LoadWindows(string source);
    }
}
