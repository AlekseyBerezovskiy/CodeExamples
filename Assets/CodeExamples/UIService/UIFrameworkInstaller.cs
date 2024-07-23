using Zenject;

namespace CodeExamples.UIService 
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
                .To<UIService>()
                .AsSingle();
        }
    }
}
