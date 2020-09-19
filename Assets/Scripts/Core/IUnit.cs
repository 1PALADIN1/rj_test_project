namespace Core
{
    public interface IUnit
    {
        TeamFlag Team { get; }
        
        int X { get; }
        int Y { get; }
        
        int MaxHealth { get; }
        int Health { get; }
        int MaxMana { get; }
        int Mana { get; }
        
        bool IsAlive();
        
        void AddMana(int mana);
        void SubMana(int mana);
        void Heal(int heal);
        void Damage(int damage);
        void MoveTo(int x, int y);
        void AddStun();
    }
}