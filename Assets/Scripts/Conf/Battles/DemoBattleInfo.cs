namespace Conf
{
    public class DemoBattleInfo : BattleInfo
    {
        public DemoBattleInfo()
        {
            AddBlue(new MeleeHedgehogUnitInfo(), 38, 35);
            AddBlue(new MeleeAssassinUnitInfo(), 40, 30);
            AddBlue(new MeleeAssassinUnitInfo(), 42, 30);
            AddBlue(new MeleeSoldierUnitInfo(),  44, 32);
            AddBlue(new MeleeHedgehogUnitInfo(), 46, 35);
            AddBlue(new MeleeSoldierUnitInfo(),  48, 32);
            AddBlue(new MeleeAssassinUnitInfo(), 50, 30);
            AddBlue(new MeleeAssassinUnitInfo(), 52, 30);
            AddBlue(new MeleeHedgehogUnitInfo(), 54, 35);
            
            
            AddRed(new RangeArcherUnitInfo(),    42, 48);
            AddRed(new MeleeSoldierUnitInfo(),   44, 52);
            AddRed(new MeleeAssassinUnitInfo(),  46, 50);
            AddRed(new MeleeSoldierUnitInfo(),   48, 52);
            AddRed(new MeleeAssassinUnitInfo(),  50, 50);
            AddRed(new MeleeSoldierUnitInfo(),   52, 52);
            AddRed(new RangeArcherUnitInfo(),    54, 48);
        }
    }
}