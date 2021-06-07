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
        [SerializeField] private TextMeshProUGUI _walletConnectedText;
        private WalletData _data = null;

        public void Init(WalletData data)
        {
            // Debug.Log("UIWallet Init");

            _data = data;

            _data.IsWalletConnected.WithoutCurrent().ForEachAsync(i =>
            {
                SetWalletConnectedText(i);
            }, this.GetCancellationTokenOnDestroy()).Forget();
        }

        private void SetWalletConnectedText(bool isConnected)
        {
            _walletConnectedText.text = isConnected ? "wallet connected" : "wallet disconnected";
        }
    }
}
