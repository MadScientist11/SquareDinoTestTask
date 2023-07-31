namespace Game.Source.DamageSystem
{
    public class ProjectileDamage : IDamageProvider
    {
        private readonly IDamageProvider _baseDamageProvider;
        private readonly int _projectileDamage;

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
}