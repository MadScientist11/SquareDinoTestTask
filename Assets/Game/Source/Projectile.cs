using System;
using Game.Source.DamageSystem;
using Game.Source.EnemyLogic;
using Game.Source.LevelSystem;
using Game.Source.PlayerLogic;
using Game.Source.Services;
using Game.Source.Services.Factories;
using UnityEngine;
using VContainer;

namespace Game.Source
{
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        private IDamageProvider _projectileDamageProvider;
        private LevelBoundingBox _levelBoundingBox;
        public Action<Projectile> Release { get; set; }

        [Inject]
        public void Construct(Level level)
        {
            _levelBoundingBox = level.LevelBoundingBox;
        }

        public void Initialize(IDamageProvider baseProvider)
        {
            _projectileDamageProvider = new ProjectileDamage(baseProvider,5);
        }
       
        private void Update()
        {
            if (!_levelBoundingBox.Contains(this.transform.position))
            {
                ReleaseToPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_projectileDamageProvider);
                ReleaseToPool();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    
        private void ReleaseToPool()
        {
            Release?.Invoke(this);
        }
    }
}