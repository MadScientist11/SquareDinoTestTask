using System.Diagnostics;
using Game.Source.Services;
using UnityEngine;
using VContainer;
using Debug = UnityEngine.Debug;

namespace Game.Source.PlayerLogic
{
    public interface IDamageProvider
    {
        int ProvideDamage();
    }

    public class PlayerAttackDamage : IDamageProvider
    {
        private int _damage;

        public PlayerAttackDamage(int damage)
        {
            _damage = damage;
        }

        public int ProvideDamage()
        {
            return _damage;
        }
    }

    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private float _attackCooldown;

        private IInputService _inputService;
        private IProjectileFactory _projectileFactory;


        [Inject]
        public void Construct(IProjectileFactory projectileFactory, IInputService inputService)
        {
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

        private void Update() => 
            DebugPlayerAttack();

        private void LaunchProjectile()
        {
            Plane plane = new Plane(Vector3.up, _spawnPosition.position);

            Ray ray = Camera.main.ScreenPointToRay(_inputService.MousePosition);

            if (plane.Raycast(ray, out var enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 vectorToHit = (hitPoint - _spawnPosition.position);

                bool canThrowInTheDirection = Vector3.Dot(vectorToHit.normalized, _spawnPosition.forward) > 0;
                if(!canThrowInTheDirection)
                    return;

                Projectile projectile = InitializeProjectile(vectorToHit);
                ThrowProjectile(projectile, vectorToHit);
            }
        }

        private Projectile InitializeProjectile(Vector3 vectorToHit)
        {
            PlayerAttackDamage damageProvider = new PlayerAttackDamage(2);
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
            rb.AddForce(direction * throwStrength * 1.5f, ForceMode.VelocityChange);
            rb.AddTorque(projectile.transform.forward * -15, ForceMode.Impulse);
        }

        [Conditional("UNITY_EDITOR")]
        private void DebugPlayerAttack()
        {
            Plane plane = new Plane(Vector3.up, _spawnPosition.position);
            DrawHelper.DrawPlane(_spawnPosition.position, Vector3.up);

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