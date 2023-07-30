using System;
using System.Security.Cryptography;
using Game.Source.EnemyLogic;
using Game.Source.PlayerLogic;
using Game.Source.Services;
using UnityEngine;

namespace Game.Source
{
    public class ProjectileDamage : IDamageProvider
    {
        private IDamageProvider _baseDamageProvider;
        private int _projectileDamage;

        public ProjectileDamage(IDamageProvider baseDamageProvider, int projectileDamage)
        {
            _projectileDamage = projectileDamage;
            _baseDamageProvider = baseDamageProvider;
        }


        public int ProvideDamage()
        {
           return _baseDamageProvider.ProvideDamage() + _projectileDamage;
        }
    }
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        private IDamageProvider _projectileDamageProvider;
        public Action<Projectile> Release { get; set; }

        public void Initialize(IDamageProvider baseProvider)
        {
            _projectileDamageProvider = new ProjectileDamage(baseProvider,5);
        }
       
        private void Update()
        {
            transform.Translate(-transform.forward*3 * Time.deltaTime);
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
    
        public void ReleaseToPool()
        {
            Release?.Invoke(this);
        }
    }
}