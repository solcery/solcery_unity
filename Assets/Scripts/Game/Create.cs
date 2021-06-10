using Solcery.UI.Create;
using Solcery.Utils;

namespace Solcery
{
    public class Create : Singleton<Create>
    {
        public void Init()
        {
            UICreate.Instance?.Init();
        }

        public void DeInit()
        {
            UICreate.Instance?.DeInit();
        }
    }
}
