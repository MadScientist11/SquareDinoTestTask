using System;
using Game.Source.DamageSystem;
using Game.Source.Services;
using Game.Source.UI;
using UnityEngine;
using VContainer;

namespace Game.Source.EnemyLogic
{
    public interface IDamageable
    {
        void TakeDamage(IDamageProvider damageProvider);
    }

    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public event Action<float, float> HealthChanged;

        [SerializeField] private HpBar _hpBar;

        private float _currentHealth;
        private EnemyConfiguration _enemyConfiguration;
        private ILevelController _levelController;

        [Inject]
        public void Construct(IDataProvider dataProvider, ILevelController levelController)
        {
            _levelController = levelController;
            _enemyConfiguration = dataProvider.EnemyConfig;
        }

        private void Start()
        {
            _currentHealth = _enemyConfiguration.MaxHealth;
            HealthChanged += _hpBar.SetValue;
        }

        private void OnDestroy()
        {
            HealthChanged -= _hpBar.SetValue;
        }

        public void TakeDamage(IDamageProvider damageProvider)
        {
            if(!TheEnemyIsFromCurrentLocation())
                return;
            
            _currentHealth -= damageProvider.ProvideDamage();
            HealthChanged?.Invoke(_currentHealth, _enemyConfiguration.MaxHealth);
        }

        private bool TheEnemyIsFromCurrentLocation() => 
            _levelController.CurrentLocation.LocationEnemies.Contains(this.GetComponent<Enemy>());
    }
}