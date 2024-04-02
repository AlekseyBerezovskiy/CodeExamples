using MyServices.UIService.Interfaces;
using MyServices.UIService.Realization;
using Zenject;

namespace MyServices.UIService.Installer 
{
    public class UIFrameworkInstaller : Installer<UIFrameworkInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IUIRoot>()
                .To<UIRoot>()
                .FromComponentInNewPrefabResource(nameof(UIRoot))
                .AsSingle();

            Container
                .Bind<IUIService>()
                .To<Realization.UIService>()
                .AsSingle();
        }
    }
}
