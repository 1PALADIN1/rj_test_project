using Conf;

namespace Core
{
    public interface IBattle
    {
        void Start(BattleInfo info);
        void Tick();
        void Finish();
        bool IsFinished();
    }
}