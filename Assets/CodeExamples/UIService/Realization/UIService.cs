using System;
using System.Collections.Generic;
using MyServices.UIService.Interfaces;
using UnityEngine;
using Zenject;

namespace MyServices.UIService.Realization 
{
    public class UIService : IUIService
    {
        private Transform _deactivatedContainer;
        
        private readonly IUIRoot _uIRoot;
        private readonly IInstantiator _instantiator;
        
        private readonly Dictionary<Type,UIWindow> _viewStorage = new Dictionary<Type,UIWindow>();
        private readonly Dictionary<Type, GameObject> _initWindows= new Dictionary<Type, GameObject>();

        private const string GeneralWindowsSource = "UIWindows";
        
        public UIService(
            IInstantiator instantiator,
            IUIRoot uIRoot)
        {

            _instantiator = instantiator;
            _uIRoot = uIRoot;
        }

        public T Show<T>(int layer = 0) where T : UIWindow
        {
            var window = Get<T>();
            if(window != null)
            {
                var windowTransform = window.transform;
                
                windowTransform.SetParent(_uIRoot.Container.Layers[layer], false);
                windowTransform.localScale = Vector3.one;
                windowTransform.localRotation = Quaternion.identity;
                windowTransform.localPosition = Vector3.zero;

                var component = window.GetComponent<T>();
                
                //always resize to screen size
                var rect = component.transform as RectTransform;
                if (rect != null)
                {
                    rect.offsetMax = Vector2.zero;
                    rect.offsetMin = Vector2.zero;
                }
                
                component.Show();
                return component;
            }
            return null;
        }

        public T Get<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_initWindows.ContainsKey(type))
            {
                var view = _initWindows[type];            
                return view.GetComponent<T>();
            }
            return null;
        }

        public void Hide<T>(Action onEnd = null) where T : UIWindow
        {
            var window = Get<T>();
            if(window!=null)
            {
                window.transform.SetParent(_uIRoot.PoolContainer);
                window.Hide();
                onEnd?.Invoke();
            }
        }

        public void HideAll(Action onEnd = null)
        {
            foreach (var viewsKVP in _initWindows)
            {
                viewsKVP.Value.transform.SetParent(_uIRoot.PoolContainer);
                onEnd?.Invoke();
            }
        }

        public void InitWindows(Transform poolDeactiveContiner = null)
        {
            _deactivatedContainer = poolDeactiveContiner == null ? _uIRoot.PoolContainer : poolDeactiveContiner;
            foreach (var windowKVP in _viewStorage)
            {
                Init(windowKVP.Key, _deactivatedContainer);
            }
        }

        public void LoadWindows(string source)
        {
            var windows = Resources.LoadAll(source, typeof(UIWindow));

            foreach (var window in windows)
            {
                var windowType = window.GetType();
                _viewStorage.Add(windowType, (UIWindow) window);
            }
            
            windows = Resources.LoadAll(GeneralWindowsSource, typeof(UIWindow));

            foreach (var window in windows)
            {
                var windowType = window.GetType();
                _viewStorage.Add(windowType, (UIWindow) window);
            }
        }    
    
        private void Init(Type t, Transform parent = null)
        {
            if(_viewStorage.ContainsKey(t))
            {
                GameObject view = null;
                if(parent!=null)
                {
                    view = _instantiator.InstantiatePrefab(_viewStorage[t], parent);
                }
                else
                {
                    view = _instantiator.InstantiatePrefab(_viewStorage[t]);
                }
                _initWindows.Add(t, view);
            }
        }
    }
}
