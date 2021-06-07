using Grimmz.UI.Wallet;
using Grimmz.Utils;

namespace Grimmz.Modules.Wallet
{
    public class Wallet : Singleton<Wallet>
    {
        public WalletData Data => _data;

        private WalletData _data = null;

        public void Init()
        {
            _data = new WalletData();
            UIWallet.Instance?.Init(_data);
        }

        public void DeInit()
        {
            
        }
    }
}
