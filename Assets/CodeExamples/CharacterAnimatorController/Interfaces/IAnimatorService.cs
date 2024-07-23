using System;
using UnityEngine;

namespace CodeExamples.CharacterAnimatorController
{
    public interface IAnimatorService : IDisposable
    {
        ICharacterAnimatorController CreateAnimator(
            Animator animator);
    }
}