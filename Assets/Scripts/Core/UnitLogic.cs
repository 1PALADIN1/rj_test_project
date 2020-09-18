namespace Core
{
    public abstract class UnitLogic
    {
        protected readonly IUnit Unit;
        protected readonly ICore Core;
    
        protected UnitLogic(IUnit unit, ICore core)
        {
            Unit = unit;
            Core = core;
        }
    
        public virtual void OnSpawn()
        {
        }
    
        public virtual int OnDamage(int damage)
        {
            return damage;
        }

        public virtual int OnHeal(int heal)
        {
            return heal;
        }

        public virtual int OnBeforeManaChange(int delta)
        {
            return delta;
        }
    
        public virtual void OnTurn()
        {
        }

        public virtual void OnAbility()
        {
        }

        public virtual void OnDie()
        {
        }
    }
}