using System;
using Game.Source.PlayerLogic;
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

        [Inject]
        public void Construct(IDataProvider dataProvider)
        {
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
            _currentHealth -= damageProvider.ProvideDamage();
            HealthChanged?.Invoke(_currentHealth, _enemyConfiguration.MaxHealth);
        }
    }
}