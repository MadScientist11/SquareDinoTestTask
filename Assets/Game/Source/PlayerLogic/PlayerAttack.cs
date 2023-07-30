using System;
using Game.Source.Services;
using UnityEngine;
using VContainer;

namespace Game.Source.PlayerLogic
{
    public interface IDamageProvider
    {
        int ProvideDamage();
    }

    public class PlayerAttackDamage : IDamageProvider
    {
        private int _damage;

        public PlayerAttackDamage(int damage)
        {
            _damage = damage;
        }
        
        public int ProvideDamage()
        {
            return _damage;
        }
    }

    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;

        private IInputService _inputService;
        private IProjectileFactory _projectileFactory;


        [Inject]
        public void Construct(IProjectileFactory projectileFactory, IInputService inputService)
        {
            _projectileFactory = projectileFactory;
            _inputService = inputService;
        }

        private void OnEnable()
        {
            _inputService.OnFireInputDetected += LaunchProjectile;
        }

        private void OnDisable()
        {
            _inputService.OnFireInputDetected -= LaunchProjectile;
        }

        private void LaunchProjectile()
        {
            Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            PlayerAttackDamage damageProvider = new PlayerAttackDamage(2);
            
            Projectile projectile =
                _projectileFactory.GetOrCreateProjectile(_spawnPosition.position,
                    Quaternion.LookRotation(ray.direction));
            
            projectile.Initialize(damageProvider);

        }
    }
}