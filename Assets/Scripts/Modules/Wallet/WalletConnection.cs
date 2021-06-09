using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Grimmz.Modules.Wallet
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

        public void Subscribe(Action<bool> callback, CancellationToken cancellationToken)
        {
            _isConnected.ForEachAsync(isConnected =>
            {
                callback?.Invoke(isConnected);
            }, cancellationToken);
        }
    }
}
