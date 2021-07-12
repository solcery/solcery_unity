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

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, BoardCardType> CardTypesById;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<CardPlace, List<BoardCardData>> Places;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public BoardCardType EndTurnCardType;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Me;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Enemy;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int MyIndex = -1;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int EnemyIndex = -1;

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
                var cardTypeId = cardType.Id;

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
            bool atLeastOneMe = false;
            bool atLeastOneEnemy = false;

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsMe)
                {
                    Me = Players[i];
                    MyIndex = i;
                    atLeastOneMe = true;
                }
                else
                {
                    Enemy = Players[i];
                    EnemyIndex = i;
                    atLeastOneEnemy = true;
                }
            }

            if (!atLeastOneMe)
                MyIndex = -1;

            if (!atLeastOneEnemy)
                EnemyIndex = -1;
        }

        private void FindEndTurnCard()
        {
            foreach (var card in Cards)
            {
                if (card.CardId == EndTurnCardId)
                {
                    var endTurnCardData = card;
                    EndTurnCardType = GetCardType(endTurnCardData.CardType);
                }
            }
        }
    }
}
