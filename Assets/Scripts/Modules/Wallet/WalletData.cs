using Cysharp.Threading.Tasks;

namespace Grimmz.Modules.Wallet
{
    public class WalletData
    {
        public AsyncReactiveProperty<bool> IsWalletConnected => _isWallectConnected;

        private AsyncReactiveProperty<bool> _isWallectConnected = new AsyncReactiveProperty<bool>(false);
    }
}
