using UnityEngine;

namespace Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void ChangeAnimationState(ECharacterAnimationState state) => _animator.SetInteger("MoveState", (int)state);
    
        public enum ECharacterAnimationState
        {
            Idle = 0,
            Walk = 1,
            Run = 2
        }
    }
}
