using System.Threading;
using Solcery.Modules.Fight;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public class UIPlay : Singleton<UIPlay>
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

            if (Fight.Instance.FightData.Value == null)
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
                UpdateFight(Fight.Instance.FightData.Value);
            }

            Reactives.SubscribeWithoutCurrent(Fight.Instance?.FightData, UpdateFight, _cts.Token);
        }

        private void UpdateFight(FightData fightData)
        {
            if (fightData == null) return;

            this.fight?.gameObject?.SetActive(true);
            this.fight?.UpdateFight(fightData);
        }

        public void DeInit()
        {
            _cts.Cancel();

            fight?.DeInit();
            cardCollection?.DeInit();
            createFightButton?.onClick?.RemoveAllListeners();
        }
    }
}

