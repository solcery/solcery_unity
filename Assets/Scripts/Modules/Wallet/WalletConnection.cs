using Cysharp.Threading.Tasks;

namespace Solcery.Modules
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
