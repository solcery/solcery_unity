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
            var collectionData = new CollectionData();

            collectionData.CardTypes = new List<CollectionCardType>() {
                    new CollectionCardType() { MintAddress = "1", Metadata = new CardMetadata() { Picture = 1, Coins = 1, Name = "1", Description = "1"}},
                    new CollectionCardType() { MintAddress = "2", Metadata = new CardMetadata() { Picture = 2, Coins = 2, Name = "2", Description = "2"}},
                    new CollectionCardType() { MintAddress = "3", Metadata = new CardMetadata() { Picture = 3, Coins = 3, Name = "3", Description = "3"}},
                    new CollectionCardType() { MintAddress = "4", Metadata = new CardMetadata() { Picture = 4, Coins = 4, Name = "4", Description = "4"}},
                    new CollectionCardType() { MintAddress = "5", Metadata = new CardMetadata() { Picture = 5, Coins = 5, Name = "5", Description = "5"}},
                    new CollectionCardType() { MintAddress = "6", Metadata = new CardMetadata() { Picture = 6, Coins = 6, Name = "6", Description = "6"}},
                    new CollectionCardType() { MintAddress = "7", Metadata = new CardMetadata() { Picture = 7, Coins = 7, Name = "7", Description = "7"}},
                    new CollectionCardType() { MintAddress = "8", Metadata = new CardMetadata() { Picture = 8, Coins = 8, Name = "8", Description = "8"}},
                    new CollectionCardType() { MintAddress = "9", Metadata = new CardMetadata() { Picture = 9, Coins = 9, Name = "9", Description = "9"}},
                };

            Collection.Instance?.UpdateCollection(collectionData);
        }

        public void DeInit()
        {

        }
    }
}
