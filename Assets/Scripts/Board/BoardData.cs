using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardData
    {
        public List<BoardCardType> CardTypes;
        public List<BoardCardData> Cards;
        public List<PlayerData> Players;
        public int EndTurnCardId;

        [NonSerialized] public Dictionary<int, BoardCardType> CardTypesById;
        [NonSerialized] public Dictionary<CardPlace, List<BoardCardData>> Places;
        [NonSerialized] public BoardCardType EndTurnCardType;
        [NonSerialized] public PlayerData Me;
        [NonSerialized] public PlayerData Enemy;
        [NonSerialized] public int MyIndex;
        [NonSerialized] public int EnemyIndex;

        public BoardCardType GetCardType(int cardTypeId)
        {
            if (CardTypesById.TryGetValue(cardTypeId, out var cardType))
            {
                return cardType;
            }

            return null;
        }

        public BoardData Prettify()
        {
            CreateTypesDictionary();
            CreatePlacesDictionary();
            AssignPlayers();
            FindEndTurnCard();

            return this;
        }

        private void CreateTypesDictionary()
        {
            CardTypesById = new Dictionary<int, BoardCardType>();

            foreach (var cardType in CardTypes)
            {
                var cardTypeId = cardType.CardTypeId;

                if (CardTypesById.ContainsKey(cardTypeId))
                    CardTypesById[cardTypeId] = cardType;
                else
                    CardTypesById.Add(cardTypeId, cardType);
            }
        }

        private void CreatePlacesDictionary()
        {
            Places = new Dictionary<CardPlace, List<BoardCardData>>();

            foreach (var card in Cards)
            {
                if (Places.ContainsKey(card.CardPlace))
                {
                    Places[card.CardPlace].Add(card);
                }
                else
                {
                    Places.Add(card.CardPlace, new List<BoardCardData>() { card });
                }
            }
        }

        private void AssignPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsMe)
                {
                    Me = Players[i];
                    MyIndex = i;
                }
                else
                {
                    Enemy = Players[i];
                    EnemyIndex = i;
                }
            }
        }

        private void FindEndTurnCard()
        {
            foreach (var card in Cards)
            {
                if (card.CardId == EndTurnCardId)
                {
                    var endTurnCardData = card;
                    EndTurnCardType = GetCardType(endTurnCardData.CardTypeId);
                }
            }
        }
    }
}
