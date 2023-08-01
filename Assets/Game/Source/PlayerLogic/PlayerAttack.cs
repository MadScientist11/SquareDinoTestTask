using System.Diagnostics;
using Game.Source.DamageSystem;
using Game.Source.Services;
using Game.Source.Services.Factories;
using UnityEngine;
using VContainer;
using Debug = UnityEngine.Debug;

namespace Game.Source.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _planeOrigin;

        private IInputService _inputService;
        private IProjectileFactory _projectileFactory;
        private PlayerConfiguration _playerConfiguration;

        private float _attackCooldown;

        [Inject]
        public void Construct(IProjectileFactory projectileFactory, IInputService inputService,
            IDataProvider dataProvider)
        {
            _playerConfiguration = dataProvider.PlayerConfig;
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

        private void Update()
        {
            _attackCooldown -= Time.deltaTime;
            DebugPlayerAttack();
        }

        private void LaunchProjectile()
        {
            if (_attackCooldown > 0)
                return;

            Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            if (HitCollider(out var hit))
            {
                Vector3 vectorToHit = (hit.point - _spawnPosition.position);
                PerformProjectileLaunch(vectorToHit);
                return;
            }

            Plane plane = new Plane(Vector3.up, _planeOrigin.position);
            if (plane.Raycast(ray, out var enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 vectorToHit = (hitPoint - _planeOrigin.position);
                PerformProjectileLaunch(vectorToHit);
            }

            bool HitCollider(out RaycastHit raycastHit) => 
                Physics.Raycast(ray, out raycastHit, 25);
        }

        private void PerformProjectileLaunch(Vector3 vectorToHit)
        {
            if (!CanThrowInTheDirection(vectorToHit.normalized))
                return;

            Projectile projectile = InitializeProjectile(vectorToHit);
            ThrowProjectile(projectile, vectorToHit);
            _attackCooldown = _playerConfiguration.AttackCooldown;
        }

        private bool CanThrowInTheDirection(Vector3 direction) =>
            Vector3.Dot(direction, _spawnPosition.forward) > 0;

        private Projectile InitializeProjectile(Vector3 vectorToHit)
        {
            CritChance damageProvider = new CritChance(new NoDamage(), 2);
            Projectile projectile =
                _projectileFactory.GetOrCreateProjectile(_spawnPosition.position,
                    Quaternion.LookRotation(Vector3.up, vectorToHit.normalized) *
                    Quaternion.AngleAxis(90, Vector3.up));
            projectile.Initialize(damageProvider);
            return projectile;
        }

        private void ThrowProjectile(Projectile projectile, Vector3 vectorToHit)
        {
            Vector3 direction = vectorToHit.normalized;
            float throwStrength = Mathf.Clamp(vectorToHit.magnitude, 2, 10);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.maxAngularVelocity = 15;
            rb.AddForce(direction * throwStrength * _playerConfiguration.ThrowStrengthMultiplier, ForceMode.VelocityChange);
            rb.AddTorque(-projectile.transform.forward * _playerConfiguration.ThrowTorqueMultiplier, ForceMode.VelocityChange);
        }

        [Conditional("UNITY_EDITOR")]
        private void DebugPlayerAttack()
        {
            Plane plane = new Plane(Vector3.up, _planeOrigin.position);
            DrawHelper.DrawPlane(_planeOrigin.position, Vector3.up);

            Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            if (plane.Raycast(ray, out var enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Debug.DrawLine(_spawnPosition.position, hitPoint, Color.red);
            }

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 20);
        }
    }
}