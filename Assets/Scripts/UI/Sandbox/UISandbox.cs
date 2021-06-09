using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Solcery.Modules.FightModule;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Sandbox
{
    public class UISandbox : Singleton<UISandbox>
    {
        public UICardCollection CardCollection => cardCollection;

        [SerializeField] private Button createFightButton = null;
        [SerializeField] private UIFight fight = null;
        [SerializeField] private UICardCollection cardCollection = null;

        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            fight.Init();
            cardCollection.Init();

            if (FightModule.Instance.Fight.Value == null)
            {
                createFightButton.onClick.AddListener(() =>
                {
                    UnityToReact.Instance?.CallCreateFight();
                    createFightButton.gameObject.SetActive(false);
                });
            }
            else
            {
                fight.gameObject.SetActive(true);
                createFightButton.gameObject.SetActive(false);
                UpdateFight(FightModule.Instance.Fight.Value);
            }

            Reactives.SubscribeTo(FightModule.Instance?.Fight, UpdateFight, _cts.Token);
        }

        private void UpdateFight(Fight fight)
        {
            this.fight?.gameObject?.SetActive(true);
            this.fight?.UpdateFight(fight);
        }

        public void DeInit()
        {
            _cts?.Cancel();

            fight?.DeInit();
            cardCollection?.DeInit();
            createFightButton?.onClick?.RemoveAllListeners();
        }
    }
}

