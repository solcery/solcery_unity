using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Grimmz.Modules.Wallet;
using Grimmz.Utils;
using TMPro;
using UnityEngine;

namespace Grimmz.UI.Wallet
{
    public class UIWallet : Singleton<UIWallet>
    {
        [SerializeField] private GameObject connectWalletPopup = null;
        [SerializeField] private TextMeshProUGUI _walletConnectedText;
        private WalletData _data = null;

        public void Init(WalletData data)
        {
            _data = data;

            _data.IsWalletConnected.ForEachAsync(w =>
            {
                SetWalletConnected(w);
            }, this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void SetWalletConnected(bool isConnected)
        {
            connectWalletPopup.SetActive(!isConnected);
        }
    }
}
