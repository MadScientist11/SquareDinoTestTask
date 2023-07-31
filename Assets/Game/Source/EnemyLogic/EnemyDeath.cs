using System;
using Game.Source.Services;
using Game.Source.UI;
using UnityEngine;
using VContainer;

namespace Game.Source.EnemyLogic
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private Ragdoll _ragdoll;
        [SerializeField] private HpBar _hpBar;

        private ILevelController _levelController;

        [Inject]
        public void Construct(ILevelController levelController)
        {
            _levelController = levelController;
        }

        private void Start()
        {
            _ragdoll.Disable();
            _enemyHealth.HealthChanged += CheckIfDead;
        }

        private void CheckIfDead(float health, float maxHealth)
        {
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _ragdoll.Enable();
            _hpBar.gameObject.SetActive(false);
            Enemy enemy = this.GetComponent<Enemy>();
            _levelController.CurrentLocation.SetEnemyNeutralized(enemy);
        }
    }
}