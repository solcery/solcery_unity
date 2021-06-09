using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.CardCollection
{
    public class CardCollection : Singleton<CardCollection>
    {
        public AsyncReactiveProperty<Collection> Collection => _collection;
        private AsyncReactiveProperty<Collection> _collection = new AsyncReactiveProperty<Collection>(null);

        public void UpdateCollection(Collection collection)
        {
            _collection.Value = collection;
        }

        public void Init()
        {
            
        }

        public void DeInit()
        {
            
        }
    }
}
