using System.Threading;
using Solcery.Modules.Wallet;
using Solcery.Utils;
using TMPro;
using UnityEngine;

namespace Solcery.UI.Wallet
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
