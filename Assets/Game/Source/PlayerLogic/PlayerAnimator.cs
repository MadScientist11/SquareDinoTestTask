using UnityEngine;

namespace Game.Source.PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _playerAnimator;
        
        private static readonly int Run = Animator.StringToHash("Run");

        private const string IdleState = "Idle";
        private const string RunState = "Run";
        
        public void PlayIdleAnimation()
        {
            _playerAnimator.SetBool(Run, false);
        }
        
        public void PlayRunAnimation()
        {
            _playerAnimator.SetBool(Run, true);

        }
    }
}
