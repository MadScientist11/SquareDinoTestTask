using System;
using Game.Source.LevelLogic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Game.Source.PlayerLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action OnDestinationReached;
        
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private Vector3 _currentDestination;


        private bool _wayPointReached  = false;
        private Level _level;

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }

        private void Start()
        {
            //Set Speed..
           SetDestination(_level.Locations[0].LocationWayPoint.Position);
        }

        private void Update()
        {
            if (IsDestinationReached() && !_wayPointReached)
            {
                _wayPointReached = true;
                OnDestinationReached?.Invoke();
                _playerAnimator.PlayIdleAnimation();
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;

            _wayPointReached = false;
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
