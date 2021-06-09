using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Solcery.Modules.Wallet
{
    public class WalletConnection
    {
        public AsyncReactiveProperty<bool> IsConnected => _isConnected;
        private AsyncReactiveProperty<bool> _isConnected;

        public WalletConnection()
        {
#if UNITY_EDITOR
            _isConnected = new AsyncReactiveProperty<bool>(true);
#else
            _isConnected = new AsyncReactiveProperty<bool>(false);
#endif
        }
    }
}
