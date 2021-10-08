using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class BoardData
    {
        public int Step;
        public List<CardData> Cards;
        public List<PlayerData> Players;
        public BrickRuntime.Random Random;
        public int EndTurnCardId;

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public GameStateDiff Diff;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, CardData> CardsById;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, List<CardData>> CardsByPlace;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public CardType EndTurnCardType;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Me;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public PlayerData Enemy;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int MyIndex = -1;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public int EnemyIndex = -1;
        public int MyId => MyIndex + 1;
        public int EnemyId => EnemyIndex + 1;

        public CardData GetCard(int cardId)
        {
            if (CardsById.TryGetValue(cardId, out var card))
            {
                return card;
            }

            return null;
        }

        public BoardData Prettify()
        {
            CreateCardsDictionary();
            CreatePlacesDictionary();
            AssignPlayers();
            FindEndTurnCard();

            return this;
        }

        private void CreateCardsDictionary()
        {
            CardsById = new Dictionary<int, CardData>();

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
            CardsByPlace = new Dictionary<int, List<CardData>>();

            foreach (var card in Cards)
            {
                if (CardsByPlace.ContainsKey(card.CardPlace))
                {
                    CardsByPlace[card.CardPlace].Add(card);
                }
                else
                {
                    CardsByPlace.Add(card.CardPlace, new List<CardData>() { card });
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
                    EndTurnCardType = OldGame.Instance?.GameContent?.Value?.GetCardTypeById(endTurnCardData.CardType);
                }
            }
        }
    }
}
