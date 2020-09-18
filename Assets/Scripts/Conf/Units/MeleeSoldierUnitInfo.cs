namespace Conf
{
    public class MeleeSoldierUnitInfo : UnitInfo
    {
        public int AbilityDamageRate;
        
        public MeleeSoldierUnitInfo()
        {
            MaxHealth = 1000;
            MaxMana = 100;
            Speed = 1;
            Damage = 15;
            ManaRegen = 20;
            AttackDistance = 1;
            AbilityDamageRate = 10;
        }
    }
}