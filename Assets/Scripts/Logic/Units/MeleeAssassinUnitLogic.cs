using Conf;
using Core;
using UnityEngine;

namespace Logic
{
    public class MeleeAssassinUnitLogic : UnitLogic
    {
        private readonly int _attackDistance;
        private readonly int _abilityDamageRate;
        private readonly int _critChance;
        private readonly int _critRate;
        private readonly int _damage;
        private readonly int _manaRegen;

        public MeleeAssassinUnitLogic(MeleeAssassinUnitInfo info, IUnit unit, ICore core) : base(unit, core)
        {
            _damage = info.Damage;
            _manaRegen = info.ManaRegen;
            _attackDistance = info.AttackDistance;
            _abilityDamageRate = info.AbilityDamageRate;
            _critChance = info.CritChance;
            _critRate = info.CritRate;
        }

        public override void OnTurn()
        {
            var target = Core.GetNearestEnemy(Unit);
            if (target != null && target.IsAlive())
            {
                if (Core.GetDistance(Unit, target) > 1)
                {
                    Unit.MoveTo(target.X, target.Y);
                }
                else
                {
                    var damage = Random.Range(0, 100) < _critChance ? _damage * _critRate : _damage;
                    target.Damage(damage);
                }
            }
            Unit.AddMana(_manaRegen);
        }
    
        public override void OnAbility()
        {
            var target = Core.GetNearestEnemy(Unit);
            if (target != null && target.IsAlive() && Core.GetDistance(Unit, target) <= _attackDistance)
            {
                target.Damage(_damage * _abilityDamageRate);
            }
        }
    }
}