using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class GameState
    {
        public List<CardData> Cards;
        public BrickRuntime.Random Random;

        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, CardData> CardsById;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public Dictionary<int, List<CardData>> CardsByPlace;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public GameStateDiff Diff;
        [NonSerialized] [Newtonsoft.Json.JsonIgnore] public bool HasBeenProcessed;

        public GameState Prettify()
        {
            CreateCardsDictionary();
            CreatePlacesDictionary();

            return this;
        }

        public CardData GetCard(int cardId)
        {
            if (CardsById.TryGetValue(cardId, out var card))
            {
                return card;
            }

            return null;
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
    }
}
