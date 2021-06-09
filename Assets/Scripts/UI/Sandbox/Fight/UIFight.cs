using TMPro;
using UnityEngine;

namespace Solcery.UI.Sandbox
{
    public class UIFight : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hp1Text = null;
        [SerializeField] private TextMeshProUGUI hp2Text = null;

        public void Init()
        {

        }

        public void DeInit()
        {

        }

        public void UpdateFight(FightData fightData)
        {
            hp1Text.text = fightData.HP1.ToString();
            hp2Text.text = fightData.HP2.ToString();
        }
    }
}
