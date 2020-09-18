using Conf;
using Core;

namespace Logic
{
    public class RangeArcherUnitLogic : UnitLogic
    {
        private readonly int _attackDistance;
        private readonly int _killChance;
        private readonly int _damage;
        private readonly int _manaRegen;

        public RangeArcherUnitLogic(RangeArcherUnitInfo info, IUnit unit, ICore core) : base(unit, core)
        {
            _damage = info.Damage;
            _manaRegen = info.ManaRegen;
            _attackDistance = info.AttackDistance;
            _killChance = info.KillChance;

        }
    
        public override void OnTurn()
        {
            var target = Core.GetNearestEnemy(Unit);
            if (target != null && target.IsAlive())
            {
                if (Core.GetDistance(Unit, target) > _attackDistance)
                {
                    Unit.MoveTo(target.X, target.Y);
                }
                else
                {
                    target.Damage(_damage);
                }
            }
            Unit.AddMana(_manaRegen);
        }
    
        public override void OnAbility()
        {
            var target = Core.GetNearestEnemy(Unit);
            if (target != null && target.IsAlive() && UnityEngine.Random.Range(0, 100) < _killChance)
            {
                target.Damage(target.MaxHealth);
            }
        }
	
        public override int OnDamage(int damage)
        {
            Unit.AddMana(_manaRegen);
            return damage;
        }
    }
}