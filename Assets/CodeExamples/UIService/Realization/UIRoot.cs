using MyServices.UIService.Interfaces;
using UnityEngine;

namespace MyServices.UIService.Realization
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Canvas Canvas { get; set; }
        
        public Camera Camera
        {
            get => camera;
            set
            {
                canvas.worldCamera = value;
                canvas.planeDistance = CanvasPlaneDistance;
                
                camera = value;
            }
        }

        public LayerContainer Container => container;

        public Transform PoolContainer => poolContainer;
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera camera;
        [SerializeField] private LayerContainer container;
        [SerializeField] private Transform poolContainer;
        
        private const float CanvasPlaneDistance = .4f;
    }
}
