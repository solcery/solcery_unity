using System.Threading;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;

namespace Solcery.UI.Wallet
{
    public class UIWallet : Singleton<UIWallet>
    {
        [SerializeField] private GameObject connectWalletPopup = null;

        private CancellationTokenSource _cts;

        public void Init(Solcery.Modules.Wallet.Wallet wallet)
        {
            _cts = new CancellationTokenSource();
            Reactives.SubscribeTo(wallet.Connection?.IsConnected, OnWalletConnectionChange, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
        }

        private void OnWalletConnectionChange(bool isConnected)
        {
            connectWalletPopup.SetActive(!isConnected);
        }
    }
}
