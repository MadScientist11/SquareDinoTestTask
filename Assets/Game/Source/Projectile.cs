using System;
using Game.Source.Services;
using UnityEngine;

namespace Game.Source
{
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        public Action<Projectile> Release { get; set; }
        
        private void Update()
        {
            transform.Translate(-transform.forward*3 * Time.deltaTime);
        }

        public void Show()
        {
            
        }

        public void Hide()
        {
            
        }
    }
}