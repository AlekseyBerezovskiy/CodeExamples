using Zenject;

namespace CodeExamples.CharacterAnimatorController
{
    public class AnimatorServiceInstaller : Installer<AnimatorServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<
                    CharacterAnimatorProtocol, 
                    CharacterAnimatorController, 
                    CharacterAnimatorController.Factory>();

            Container
                .Bind<IAnimatorService>()
                .To<AnimatorService>()
                .AsSingle();
        }
    }
}