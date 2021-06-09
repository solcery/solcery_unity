using Grimmz.UI.Wallet;
using Grimmz.Utils;

namespace Grimmz.Modules.Wallet
{
    public class Wallet : Singleton<Wallet>
    {
        public WalletConnection Connection => _connection;
        private WalletConnection _connection;

        public void Init()
        {
            _connection = new WalletConnection();
            UIWallet.Instance?.Init(_connection);
        }

        public void DeInit()
        {
            UIWallet.Instance?.DeInit();
        }
    }
}
