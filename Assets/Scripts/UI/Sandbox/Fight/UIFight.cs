using TMPro;
using UnityEngine;

namespace Grimmz.UI.Sandbox
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

        public void UpdateFight(Fight fight)
        {
            hp1Text.text = fight.HP1.ToString();
            hp2Text.text = fight.HP2.ToString();
        }
    }
}
