using TMPro;
using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpText = null;
        [SerializeField] private TextMeshProUGUI coinsText = null;

        public void UpdatePlayerData(PlayerData playerData)
        {
            SetHP(playerData.HP);
            SetCoins(playerData.Coins);
        }

        private void SetHP(int hp)
        {
            if (hpText != null) hpText.text = hp.ToString();
        }

        private void SetCoins(int coins)
        {
            if (coinsText != null) coinsText.text = coins.ToString();
        }
    }
}
