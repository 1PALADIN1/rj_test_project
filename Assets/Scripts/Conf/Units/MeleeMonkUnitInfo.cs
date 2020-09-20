namespace Conf
{
    public class MeleeMonkUnitInfo : UnitInfo
    {
        public int MaxShieldRate;
        public int DamageAbsorptionPercent;
        public int ShieldExplosionDamageRate;
        public int ShieldExplosionHealRate;
        public int DamageHealPercent;
        
        public MeleeMonkUnitInfo()
        {
            MaxHealth = 1800;
            Speed = 3;
            Damage = 5;
            ManaRegen = 5;
            AttackDistance = 2;
            MaxShieldRate = 100;
            DamageAbsorptionPercent = 50;
            ShieldExplosionDamageRate = 250;
            ShieldExplosionHealRate = 100;
            DamageHealPercent = 10;
        }
    }
}