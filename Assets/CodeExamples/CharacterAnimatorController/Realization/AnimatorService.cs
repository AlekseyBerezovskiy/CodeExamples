using MyServices.CharacterAnimatorController.Interfaces;
using MyServices.CharacterAnimatorController.Realization;
using UnityEngine;

namespace MyServices.CharacterAnimatorController
{
    public class AnimatorService : IAnimatorService
    {
        private Realization.CharacterAnimatorController.Factory _characterAnimatorFactory;

        public AnimatorService(
            Realization.CharacterAnimatorController.Factory characterAnimatorFactory)
        {
            _characterAnimatorFactory = characterAnimatorFactory;
        }
        
        public ICharacterAnimatorController CreateAnimator(Animator animator)
        {
            return _characterAnimatorFactory.Create(new CharacterAnimatorProtocol(animator));
        }

        public void Dispose()
        {
            _characterAnimatorFactory = null;
        }
    }

    public enum AnimatorTransitionType
    {
        Trigger,
        Integer,
        Bool
    }
}