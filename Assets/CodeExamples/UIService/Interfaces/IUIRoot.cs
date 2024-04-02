using MyServices.UIService.Realization;
using UnityEngine;

namespace MyServices.UIService.Interfaces
{
    public interface IUIRoot
    {
        Canvas Canvas { get; set; }
        Camera Camera { get; set; }
        LayerContainer Container { get; }
        Transform PoolContainer { get; }
    }
}
