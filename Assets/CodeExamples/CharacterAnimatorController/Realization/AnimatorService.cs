using UnityEngine;

namespace CodeExamples.CharacterAnimatorController
{
    public class AnimatorService : IAnimatorService
    {
        private CharacterAnimatorController.Factory _characterAnimatorFactory;

        public AnimatorService(
            CharacterAnimatorController.Factory characterAnimatorFactory)
        {
            _characterAnimatorFactory = characterAnimatorFactory;
        }
        
        public ICharacterAnimatorController CreateAnimator(Animator animator)
        {
            return _characterAnimatorFactory.Create(
                new CharacterAnimatorProtocol(animator));
        }

        public void Dispose()
        {
            _characterAnimatorFactory = null;
        }
    }
}