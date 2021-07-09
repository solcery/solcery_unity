using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Solcery.Utils;
using UnityEngine;

namespace Solcery.Modules.Collection
{
    public class Collection : Singleton<Collection>
    {
        [Multiline(20)]
        public string testJson;

        public AsyncReactiveProperty<CollectionData> CollectionData => _collectionData;
        private AsyncReactiveProperty<CollectionData> _collectionData = new AsyncReactiveProperty<CollectionData>(null);

        public void UpdateCollection(CollectionData collectionData)
        {
            _collectionData.Value = collectionData;
        }

        public void Init()
        {
            // var collectionData = new CollectionData();

            // collectionData.CardTypes = new List<CollectionCardType>() {
            //         new CollectionCardType() { MintAddress = "1", Metadata = new CardMetadata() { Picture = 1, Coins = 1, Name = "1", Description = "1"}},
            //         new CollectionCardType() { MintAddress = "2", Metadata = new CardMetadata() { Picture = 2, Coins = 2, Name = "2", Description = "2"}},
            //         new CollectionCardType() { MintAddress = "3", Metadata = new CardMetadata() { Picture = 3, Coins = 3, Name = "3", Description = "3"}},
            //     };

            // collectionData.RulesetData = new Ruleset.RulesetData()
            // {
            //     CardMintAddresses = new List<string>(),
            //     Deck = new List<Ruleset.PlaceData>()
            //     {
            //         new Ruleset.PlaceData()
            //         {
            //             PlaceId = 0,
            //             IndexAmount = new List<Ruleset.CardIndexAmount>(),
            //         },
            //     },
            //     DisplayData = new Ruleset.RulesetDisplayData()
            // };

            // _collectionData.Value = collectionData;

            var collectionData = JsonConvert.DeserializeObject<CollectionData>(testJson);
            UpdateCollection(collectionData);
        }

        public void DeInit()
        {

        }
    }
}
