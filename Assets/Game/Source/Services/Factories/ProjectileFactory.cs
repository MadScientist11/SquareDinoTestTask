using System;
using UnityEngine;
using VContainer;

namespace Game.Source.Services.Factories
{
    public interface IProjectileFactory 
    {
        Projectile GetOrCreateProjectile(Vector3 spawnPoint, Quaternion rotation);
    }

    public class ProjectileFactory : PooledFactory<Projectile>, IProjectileFactory
    {
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public Projectile GetOrCreateProjectile(Vector3 spawnPoint, Quaternion rotation)
        {
            Projectile projectile = Get(null);
            projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            projectile.transform.position = spawnPoint;
            projectile.transform.rotation = rotation;
            projectile.Show();
            return projectile;
        }

        protected override Projectile Create()
        {
            return _gameFactory.InstancePrefabInjected<Projectile>(GameConstants.Assets.ProjectilePath);
        }

        protected override void Release(Projectile obj)
        {
            base.Release(obj);
            obj.Hide();
        }

        protected override Projectile Get(Func<Projectile, bool> predicate)
        {
            Projectile projectile = base.Get(predicate);
            return projectile;
        }
    }
}