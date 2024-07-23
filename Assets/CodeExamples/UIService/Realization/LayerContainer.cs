using UnityEngine;

namespace CodeExamples.UIService
{
    public class LayerContainer : MonoBehaviour
    {
        public Transform[] Layers => layers;
        
        [SerializeField] private Transform[] layers;
    }
}