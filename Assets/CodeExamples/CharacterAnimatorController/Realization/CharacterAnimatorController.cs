using System;
using System.Collections.Generic;
using DG.Tweening;
using MyServices.CharacterAnimatorController.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

namespace MyServices.CharacterAnimatorController.Realization
{
    public class CharacterAnimatorController : ICharacterAnimatorController
    {
        private Animator _animator;

        private List<Action<object>> _addedActionsList = new List<Action<object>>();
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private Dictionary<string, Tween> _lerpTweens = new Dictionary<string, Tween>();
        private Dictionary<int, Tween> _lerpLayersTweens = new Dictionary<int, Tween>();
        
        public CharacterAnimatorController(
            CharacterAnimatorProtocol characterAnimatorProtocol)
        {
            _animator = characterAnimatorProtocol.Animator;
        }
        
        public void SetAction(AnimatorTransitionType transitionType, string transitionName, ref Action<object> animationEvent)
        {
            animationEvent += value =>
            {
                switch (transitionType)
                {
                    case AnimatorTransitionType.Trigger:
                        _animator.SetTrigger(transitionName);
                        break;
                    case AnimatorTransitionType.Integer:
                        _animator.SetInteger(transitionName, (int)value);
                        break;
                    case AnimatorTransitionType.Bool:
                        _animator.SetBool(transitionName, (bool)value);
                        break;
                }
            };

            _addedActionsList.Add(animationEvent);
        }

        public void SetFloatAction(string transitionName, ReactiveProperty<float> value)
        {
            var disposable = value.Subscribe(f =>
            {
                _animator.SetFloat(transitionName, f);
            });
            
            _compositeDisposable.Add(disposable);
        }

        public void SetFloatActionWithLerp(string transitionName, ReactiveProperty<float> value, float speed)
        {
            var disposable = value.Subscribe(f =>
            {
                if (_lerpTweens.ContainsKey(transitionName))
                {
                    _lerpTweens[transitionName]?.Kill();
                    _lerpTweens.Remove(transitionName);
                }

                var startValue = _animator.GetFloat(transitionName);

                if (float.IsNaN(startValue))
                {
                    startValue = 0f;
                }
                
                var tween = DOTween.To(
                        ()=> startValue, 
                        x=> startValue = x, 
                        f, 
                        Mathf.Abs(startValue - f) / speed)
                    .OnUpdate(() =>
                    {
                        _animator.SetFloat(transitionName, startValue);
                    });
                
                _lerpTweens.Add(transitionName, tween);
            });
            
            _compositeDisposable.Add(disposable);
        }

        public void SetLayerWeightWithLerp(int index, float value, float lerpSpeed)
        {
            if (_lerpLayersTweens.ContainsKey(index))
            {
                _lerpLayersTweens[index]?.Kill();
                _lerpLayersTweens.Remove(index);
            }

            var startValue = _animator.GetLayerWeight(index);
                
            var tween = DOTween.To(
                    ()=> startValue, 
                    x=> startValue = x, 
                    value, 
                    Mathf.Abs(startValue - value) / lerpSpeed)
                .OnUpdate(() =>
                {
                    _animator.SetLayerWeight(index, startValue);
                });
                
            _lerpLayersTweens.Add(index, tween);
        }

        public void Dispose()
        {
            _animator = null;

            for (int i = 0; i < _addedActionsList.Count; i++)
            {
                _addedActionsList[i] = null;
            }
            _addedActionsList.Clear();

            foreach (var lerpKVP in _lerpTweens)
            {
                lerpKVP.Value?.Kill();
            }
            _lerpTweens.Clear();
            _lerpTweens = null;
            
            _compositeDisposable.Dispose();
            _compositeDisposable = null;
        }
        
        public class Factory : PlaceholderFactory<CharacterAnimatorProtocol, CharacterAnimatorController>
        { }
    }

    public struct CharacterAnimatorProtocol
    {
        public readonly Animator Animator;
        
        public CharacterAnimatorProtocol(Animator animator)
        {
            Animator = animator;
        }
    }
}