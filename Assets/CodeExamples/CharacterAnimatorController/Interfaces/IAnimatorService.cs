using System;
using UnityEngine;

namespace MyServices.CharacterAnimatorController.Interfaces
{
    public interface IAnimatorService : IDisposable
    {
        ICharacterAnimatorController CreateAnimator(Animator animator);
    }
}