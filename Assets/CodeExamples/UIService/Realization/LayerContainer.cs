using UnityEngine;

namespace MyServices.UIService.Realization
{
    public class LayerContainer : MonoBehaviour
    {
        public Transform[] Layers => layers;
        
        [SerializeField] private Transform[] layers;
    }
}