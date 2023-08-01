using System;
using System.Collections;
using Game.Source.Services;
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


        private bool _wayPointReached = false;
        private IInputService _inputService;
        private PlayerConfiguration _playerConfiguration;
        private ILevelController _levelController;

        [Inject]
        public void Construct(IInputService inputService, IDataProvider dataProvider, ILevelController levelController)
        {
            _levelController = levelController;
            _inputService = inputService;
            _playerConfiguration = dataProvider.PlayerConfig;
        }

        private void Start()
        {
            _navMeshAgent.speed = _playerConfiguration.Speed;
        }

        private void Update()
        {
            if (IsDestinationReached() && !_wayPointReached)
            {
                _wayPointReached = true;

                _inputService.EnablePlayerInput();
                _playerAnimator.PlayIdleAnimation();
                OnDestinationReached?.Invoke();
            }
        }

        public void SetDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _wayPointReached = false;

            _playerAnimator.PlayRunAnimation();
            Debug.Log("Run");

            _inputService.DisablePlayerInput();
        }

        private IEnumerator RunAnimationDelay()
        {
            yield return new WaitForSeconds(0.05f);
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