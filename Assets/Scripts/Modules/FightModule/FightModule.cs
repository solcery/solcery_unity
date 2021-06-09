using Cysharp.Threading.Tasks;
using Grimmz.Utils;
using UnityEngine;

namespace Grimmz.Modules.FightModule
{
    public class FightModule : Singleton<FightModule>
    {
        public AsyncReactiveProperty<Fight> Fight => _fight;
        private AsyncReactiveProperty<Fight> _fight = new AsyncReactiveProperty<Fight>(null);

        public void UpdateFight(Fight fight)
        {
            _fight.Value = fight;
        }

        public void Init()
        {
            // Debug.Log("FightModule Init");
        }

        public void DeInit()
        {
            // Debug.Log("FightModule DeInit");
        }
    }
}

