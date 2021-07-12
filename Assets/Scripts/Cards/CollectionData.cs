using System;
using System.Collections.Generic;
using Solcery.Ruleset;

namespace Solcery
{
    [Serializable]
    public class CollectionData
    {
        public List<CollectionCardType> CardTypes;
        public RulesetData RulesetData;

        [Newtonsoft.Json.JsonIgnore]
        private Dictionary<string, CollectionCardType> _cardTypesByMintAddress;

        public CollectionData Prettify()
        {
            CreateCardTypesByMintAddressDictionary();

            RulesetData?.Prettify();

            return this;
        }

        public CollectionCardType GetCardTypeByMintAddress(string mintAddress)
        {
            if (_cardTypesByMintAddress.TryGetValue(mintAddress, out var cardType))
                return cardType;

            return null;
        }

        private void CreateCardTypesByMintAddressDictionary()
        {
            _cardTypesByMintAddress = new Dictionary<string, CollectionCardType>();

            foreach (var cardType in CardTypes)
            {
                var mintAddress = cardType.MintAddress;

                if (!_cardTypesByMintAddress.ContainsKey(mintAddress))
                    _cardTypesByMintAddress.Add(mintAddress, cardType);
                else
                    _cardTypesByMintAddress[mintAddress] = cardType;
            }
        }
    }
}
