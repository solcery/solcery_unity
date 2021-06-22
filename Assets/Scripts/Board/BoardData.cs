using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardData
    {
        public List<CardData> Cards;
        [NonSerialized] public Dictionary<CardPlace, List<CardData>> Places;
        public List<PlayerData> Players;

        public BoardData Prettify()
        {
            Places = new Dictionary<CardPlace, List<CardData>>();

            foreach (var card in Cards)
            {
                if (Places.ContainsKey(card.CardPlace))
                {
                    Places[card.CardPlace].Add(card);
                }
                else
                {
                    Places.Add(card.CardPlace, new List<CardData>() { card });
                }
            }

            return this;
        }
    }
}
