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
        [NonSerialized] public PlayerData Me;
        [NonSerialized] public PlayerData Enemy;
        [NonSerialized] public int MyIndex;
        [NonSerialized] public int EnemyIndex;

        public BoardData Prettify()
        {
            CreatePlacesDictionary();
            AssignPlayers();
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

        private void AssignPlayers()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].IsMe)
                {
                    // var me = Players[i];
                    // var temp = Players[0];
                    // Players[0] = me;
                    // Players[i] = temp;
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
                    EndTurnCard = card;
            }
        }
    }
}
