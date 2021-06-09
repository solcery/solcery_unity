using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.Fight
{
    public class Fight : Singleton<Fight>
    {
        public AsyncReactiveProperty<FightData> FightData => _fightData;
        private AsyncReactiveProperty<FightData> _fightData = new AsyncReactiveProperty<FightData>(null);

        public void UpdateFight(FightData fightData)
        {
            _fightData.Value = fightData;
        }

        public void Init()
        {
            
        }

        public void DeInit()
        {
            
        }
    }
}

