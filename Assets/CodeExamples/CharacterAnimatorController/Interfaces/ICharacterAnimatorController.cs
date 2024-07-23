using System;
using UniRx;

namespace CodeExamples.CharacterAnimatorController
{
    public interface ICharacterAnimatorController : IDisposable
    {
        void SetAction(
            AnimatorTransitionType transitionType, 
            string transitionName, 
            ref Action<object> animationEvent);
        void SetFloatAction(
            string transitionName, 
            ReactiveProperty<float> value);
        void SetFloatActionWithLerp(
            string transitionName, 
            ReactiveProperty<float> value, 
            float speed);
        void SetLayerWeightWithLerp(
            int index, 
            float value, 
            float lerpSpeed);
    }
}