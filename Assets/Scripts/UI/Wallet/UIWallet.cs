using System.Threading;
using Grimmz.Modules.Wallet;
using Grimmz.Utils;
using TMPro;
using UnityEngine;

namespace Grimmz.UI.Wallet
{
    public class UIWallet : Singleton<UIWallet>
    {
        [SerializeField] private GameObject connectWalletPopup = null;

        private CancellationTokenSource _cts;

        public void Init(WalletConnection connection)
        {
            _cts = new CancellationTokenSource();
            connection.Subscribe(OnWalletConnectionChange, _cts.Token);
        }

        public void DeInit()
        {
            _cts.Cancel();
        }

        private void OnWalletConnectionChange(bool isConnected)
        {
            connectWalletPopup.SetActive(!isConnected);
        }
    }
}
