namespace Core
{
    public interface ICore
    {
        IUnit GetNearestEnemy(IUnit unit);
        IUnit GetNearestFriend(IUnit unit);
        float GetDistance(int x1, int y1, int x2, int y2);
        float GetDistance(IUnit first, IUnit second);
    }
}