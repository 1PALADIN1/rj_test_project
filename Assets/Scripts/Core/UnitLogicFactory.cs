using System;
using Conf;
using Logic;

namespace Core
{
    public static class UnitLogicFactory
    {
        public static UnitLogic Create(UnitInfo info, IUnit unit, ICore core)
        {
            switch (info)
            {
                case MeleeSoldierUnitInfo i: return new MeleeSoldierUnitLogic(i, unit, core);
                case MeleeAssassinUnitInfo i: return new MeleeAssassinUnitLogic(i, unit, core);
                case MeleeHedgehogUnitInfo i: return new MeleeHedgehogUnitLogic(i, unit, core);
                case RangeArcherUnitInfo i: return new RangeArcherUnitLogic(i, unit, core);
                default:
                    throw new ArgumentOutOfRangeException(nameof(info), info, null);
            }
        }
    }
}