using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class GameContent
    {
        public BoardDisplayData DisplayData;
        public List<CardType> CardTypes;

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, CardType> CardTypesById;

        public GameContent Prettify()
        {
            DisplayData?.Prettify();
            CreateTypesDictionary();

            return this;
        }

        public CardType GetCardTypeById(int cardTypeId)
        {
            if (CardTypesById.TryGetValue(cardTypeId, out var cardType))
            {
                return cardType;
            }

            return null;
        }

        private void CreateTypesDictionary()
        {
            CardTypesById = new Dictionary<int, CardType>();

            foreach (var cardType in CardTypes)
            {
                var cardTypeId = cardType.Id;

                if (CardTypesById.ContainsKey(cardTypeId))
                    CardTypesById[cardTypeId] = cardType;
                else
                    CardTypesById.Add(cardTypeId, cardType);
            }
        }
    }
}
