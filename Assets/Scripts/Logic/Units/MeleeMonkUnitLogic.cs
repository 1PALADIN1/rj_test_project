using System;
using Conf;
using Core;

namespace Logic
{
    public class MeleeMonkUnitLogic : UnitLogic
    {
        private readonly int _attackDistance;
        private readonly int _maxShieldRate;
        private readonly int _damage;
        private readonly int _manaRegen;
        private readonly int _damageAbsorptionPercent;
        private readonly int _shieldExplosionHealRate;
        private readonly int _shieldExplosionDamageRate;
        private readonly int _damageHealPercent;

        private int _shieldRate;
        private int _collectedDamage;
        
        public MeleeMonkUnitLogic(MeleeMonkUnitInfo info, IUnit unit, ICore core) : base(unit, core)
        {
            _damage = info.Damage;
            _manaRegen = info.ManaRegen;
            _maxShieldRate = info.MaxShieldRate;
            _attackDistance = info.AttackDistance;
            _damageAbsorptionPercent = info.DamageAbsorptionPercent;
            _shieldExplosionHealRate = info.ShieldExplosionHealRate;
            _shieldExplosionDamageRate = info.ShieldExplosionDamageRate;
            _damageHealPercent = info.DamageHealPercent;
        }

        public override void OnSpawn()
        {
            _shieldRate = _maxShieldRate;
            _collectedDamage = 0;
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
            HealFromCollectedDamage();
        }

        public override int OnDamage(int damage)
        {
            if (IsShieldBroken())
            {
                _collectedDamage += damage;
                return damage;
            }
            
            var absorptionDamage = (int)Math.Round(damage * _damageAbsorptionPercent / 100f);
            var newShieldRate = _shieldRate - absorptionDamage;

            if (newShieldRate > 0)
            {
                damage -= absorptionDamage;
                _shieldRate = newShieldRate;
            }
            else
            {
                damage -= _shieldRate;
                _shieldRate = 0;
                
                var nearestEnemy = Core.GetNearestEnemy(Unit);
                if (nearestEnemy != null && nearestEnemy.IsAlive())
                {
                    nearestEnemy.Damage(_shieldExplosionDamageRate);
                }
                
                Unit.Heal(_shieldExplosionHealRate);
            }

            _collectedDamage += damage;
            return damage;
        }

        public override int OnBeforeManaChange(int delta)
        {
            if (!IsShieldBroken())
            {
                _shieldRate += delta;
            }
            
            delta = 0;
            return delta;
        }

        public override void OnStun()
        {
            HealFromCollectedDamage();
        }

        public override void OnDie()
        {
            if (IsShieldBroken())
            {
                return;
            }
            
            var nearestFriend = Core.GetNearestFriend(Unit);
            if (nearestFriend != null && nearestFriend.IsAlive())
            {
                nearestFriend.Heal(_shieldRate);
            }
        }

        private bool IsShieldBroken()
        {
            return _shieldRate <= 0;
        }

        private void HealFromCollectedDamage()
        {
            var healValue = (int)Math.Round(_collectedDamage * _damageHealPercent / 100f);
            
            if (healValue == 0)
            {
                return;
            }
            
            Unit.Heal(healValue);
            _collectedDamage = 0;
        }
    }
}