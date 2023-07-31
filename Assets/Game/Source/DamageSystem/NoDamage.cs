namespace Game.Source.DamageSystem
{
    public class NoDamage : IDamageProvider
    {
        public int ProvideDamage()
        {
            return 0;
        }
    }
}