using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardData
    {
        public List<CardData> Cards;
        public List<PlayerData> Players;
        public int EndTurnCardId;

        [NonSerialized] public Dictionary<CardPlace, List<CardData>> Places;
        [NonSerialized] public CardData EndTurnCard;

        public BoardData Prettify()
        {
            CreatePlacesDictionary();
            MakeMeTheFirstPlayer();
            FindEndTurnCard();

            return this;
        }

        private void CreatePlacesDictionary()
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
        }

        private void MakeMeTheFirstPlayer()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsMe)
                {
                    var me = Players[i];
                    var temp = Players[0];
                    Players[0] = me;
                    Players[i] = temp;
                }
            }
        }

        private void FindEndTurnCard()
        {
            foreach (var card in Cards)
            {
                if (card.CardId == EndTurnCardId)
                    EndTurnCard = card;
            }
        }
    }
}
