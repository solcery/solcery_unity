using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardData
    {
        // public BoardDisplayData DisplayData;
        public int Step;
        public List<BoardCardType> CardTypes;
        public List<BoardCardData> Cards;
        public List<PlayerData> Players; // TODO: to Dictionary
        public BrickRuntime.Random Random;
        public int EndTurnCardId;

        // [NonSerialized] [Newtonsoft.Json.JsonIgnore] public bool IsVirgin;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public BoardDataDiff Diff;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, BoardCardType> CardTypesById;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, BoardCardData> CardsById;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, List<BoardCardData>> CardsByPlace;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public BoardCardType EndTurnCardType;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Me;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Enemy;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int MyIndex = -1;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int EnemyIndex = -1;
        public int MyId => MyIndex + 1;
        public int EnemyId => EnemyIndex + 1;

        public BoardCardType GetCardTypeById(int cardTypeId)
        {
            if (CardTypesById.TryGetValue(cardTypeId, out var cardType))
            {
                return cardType;
            }

            return null;
        }

        public BoardCardData GetCard(int cardId)
        {
            if (CardsById.TryGetValue(cardId, out var card))
            {
                return card;
            }

            return null;
        }

        public BoardData Prettify()
        {
            // DisplayData?.Prettify();

            CreateTypesDictionary();
            CreateCardsDictionary();
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

        private void CreateCardsDictionary()
        {
            CardsById = new Dictionary<int, BoardCardData>();

            foreach (var card in Cards)
            {
                var cardId = card.CardId;

                if (CardsById.ContainsKey(cardId))
                    CardsById[cardId] = card;
                else
                    CardsById.Add(cardId, card);
            }
        }

        private void CreatePlacesDictionary()
        {
            CardsByPlace = new Dictionary<int, List<BoardCardData>>();

            foreach (var card in Cards)
            {
                if (CardsByPlace.ContainsKey(card.CardPlace))
                {
                    CardsByPlace[card.CardPlace].Add(card);
                }
                else
                {
                    CardsByPlace.Add(card.CardPlace, new List<BoardCardData>() { card });
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
                MyIndex = 0;

            if (!atLeastOneEnemy)
                EnemyIndex = 1 - MyIndex;
        }

        private void FindEndTurnCard()
        {
            foreach (var card in Cards)
            {
                if (card.CardId == EndTurnCardId)
                {
                    var endTurnCardData = card;
                    EndTurnCardType = GetCardTypeById(endTurnCardData.CardType);
                }
            }
        }
    }
}
