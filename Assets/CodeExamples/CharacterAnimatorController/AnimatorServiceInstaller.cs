using MyServices.CharacterAnimatorController.Interfaces;
using MyServices.CharacterAnimatorController.Realization;
using Zenject;

namespace MyServices.CharacterAnimatorController
{
    public class AnimatorServiceInstaller : Installer<AnimatorServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<
                    CharacterAnimatorProtocol, 
                    Realization.CharacterAnimatorController, 
                    Realization.CharacterAnimatorController.Factory>();

            Container
                .Bind<IAnimatorService>()
                .To<AnimatorService>()
                .AsSingle();
        }
    }
}