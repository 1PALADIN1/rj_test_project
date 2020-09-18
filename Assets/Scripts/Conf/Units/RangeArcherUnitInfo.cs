namespace Conf
{
    public class RangeArcherUnitInfo : UnitInfo
    {
        public int KillChance;
        
        public RangeArcherUnitInfo()
        {
            MaxHealth = 270;
            MaxMana = 100;
            Speed = 3;
            Damage = 60;
            ManaRegen = 50;
            AttackDistance = 10;
            KillChance = 25;
        }
    }
}