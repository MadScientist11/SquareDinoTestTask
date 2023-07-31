using System;
using Game.Source.Services;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Game.Source.PlayerLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Vector3 _currentDestination;


        private bool _wayPointReached  = false;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
      
        private void Update()
        {
            if (IsDestinationReached() && !_wayPointReached)
            {
                _wayPointReached = true;
                
                _inputService.EnablePlayerInput();
                _playerAnimator.PlayIdleAnimation();
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _wayPointReached = false;
            
            _inputService.DisablePlayerInput();
            _playerAnimator.PlayRunAnimation();
        }

        private bool IsDestinationReached()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
