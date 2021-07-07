using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solcery.Utils;

namespace Solcery.Modules.Collection
{
    public class Collection : Singleton<Collection>
    {
        public AsyncReactiveProperty<CollectionData> CollectionData => _collectionData;
        private AsyncReactiveProperty<CollectionData> _collectionData = new AsyncReactiveProperty<CollectionData>(null);

        public void UpdateCollection(CollectionData collectionData)
        {
            _collectionData.Value = collectionData;
        }

        public void Init()
        {
            
        }

        public void DeInit()
        {

        }
    }
}
