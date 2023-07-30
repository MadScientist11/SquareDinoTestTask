using System;
using UnityEngine;

namespace Game.Source.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        public Projectile Prefab;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LaunchProjectile();
            }
        }

        private void LaunchProjectile()
        {
            Projectile projectile = Instantiate(Prefab, _spawnPosition.position, Quaternion.identity);
            
        }
    }
}
