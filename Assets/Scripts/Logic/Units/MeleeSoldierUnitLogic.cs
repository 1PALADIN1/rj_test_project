using Conf;
using Core;

namespace Logic
{
    public class MeleeSoldierUnitLogic : UnitLogic
    {
        private readonly int _attackDistance;
        private readonly int _abilityDamageRate;
        private readonly int _damage;
        private readonly int _manaRegen;
    
        public MeleeSoldierUnitLogic(MeleeSoldierUnitInfo info, IUnit unit, ICore core) : base(unit, core)
        {
            _damage = info.Damage;
            _manaRegen = info.ManaRegen;
            _attackDistance = info.AttackDistance;
            _abilityDamageRate = info.AbilityDamageRate;
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
            if (target != null && target.IsAlive())
            {
                target.Damage(_abilityDamageRate * _damage);
            }
        }
    }
}