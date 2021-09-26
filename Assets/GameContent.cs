using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class GameContent
    {
        public BoardDisplayData DisplayData;
        public List<BoardCardType> CardTypes;

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, BoardCardType> CardTypesById;

        public GameContent Prettify()
        {
            DisplayData?.Prettify();
            CreateTypesDictionary();

            return this;
        }

        private void CreateTypesDictionary()
        {
            CardTypesById = new Dictionary<int, BoardCardType>();

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
