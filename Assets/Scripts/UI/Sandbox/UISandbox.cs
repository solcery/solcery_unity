using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Grimmz.Modules.FightModule;
using Grimmz.Utils;
using Grimmz.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Grimmz.UI.Sandbox
{
    public class UISandbox : Singleton<UISandbox>
    {
        public UICardCollection CardCollection => cardCollection;

        [SerializeField] private Button createFightButton = null;
        [SerializeField] private UIFight fight = null;
        [SerializeField] private UICardCollection cardCollection = null;

        public void Init()
        {
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

            FightModule.Instance?.Fight.WithoutCurrent().ForEachAsync(f =>
            {
                if (f != null) UpdateFight(f);
            }, this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void UpdateFight(Fight fight)
        {
            this.fight.gameObject.SetActive(true);
            this.fight.UpdateFight(fight);
        }

        public void DeInit()
        {
            fight.DeInit();
            cardCollection.DeInit();
            createFightButton.onClick.RemoveAllListeners();
        }
    }
}

