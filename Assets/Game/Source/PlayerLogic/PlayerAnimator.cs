using UnityEngine;

namespace Game.Source.PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;

        private const string IdleState = "Idle";
        private const string RunState = "Run";
        
        public void PlayIdleAnimation()
        {
            _playerAnimator.Play(IdleState);
        }
        
        public void PlayRunAnimation()
        {
            _playerAnimator.Play(RunState);
        }
    }
}
