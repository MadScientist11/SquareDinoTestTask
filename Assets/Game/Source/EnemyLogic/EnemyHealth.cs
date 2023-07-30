using System;
using Game.Source.LevelLogic;
using Game.Source.PlayerLogic;
using Game.Source.Services;
using Game.Source.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Source.EnemyLogic
{
    public interface IDamageable
    {
        void TakeDamage(IDamageProvider damageProvider);
    }

    public class EnemyHealth : MonoBehaviour, IDamageable
    {

        [SerializeField] private Animator _enemyAnimator;
        [SerializeField] private BoxCollider _enemyCollider;

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
        }

        public void TakeDamage(IDamageProvider damageProvider)
        {
            _currentHealth -= damageProvider.ProvideDamage();
            float normalizedHealth = Mathf.Max(0, _currentHealth / _enemyConfiguration.MaxHealth);
            _hpBar.SetValue(normalizedHealth); 
            
            if (_currentHealth <= 0)
            {
                Die();
                return;
            }
        }

        private void Die()
        {
            _enemyCollider.enabled = false;
            _enemyAnimator.enabled = false;
            _hpBar.gameObject.SetActive(false);
            Enemy enemy = this.GetComponent<Enemy>();
            _levelController.CurrentLocation.SetEnemyNeutralized(enemy);
        }
    }
}