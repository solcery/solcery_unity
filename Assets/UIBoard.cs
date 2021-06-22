using UnityEngine;

namespace Solcery
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField] private UIPlayer player = null;
        [SerializeField] private UIPlayer enemy = null;
        [SerializeField] private UIDrawPile playerDrawPile = null;
        [SerializeField] private UIDrawPile enemyDrawPile = null;
        [SerializeField] private UIDrawPile deck = null;
        [SerializeField] private UIPlayerHand playerHand = null;
        [SerializeField] private UIPlayerHand enemyHand = null;
        [SerializeField] private UIPlayerHand shop = null;

        public void Init()
        {
            Board.Instance.OnBoardUpdate += OnBoardUpdate;
        }

        public void DeInit()
        {
            Board.Instance.OnBoardUpdate -= OnBoardUpdate;
        }


        private void OnBoardUpdate(BoardData boardData)
        {
            player?.UpdatePlayerData(boardData.Players[0]);
            enemy?.UpdatePlayerData(boardData.Players[1]);

            playerDrawPile.SetCardsCount(boardData.Places.ContainsKey(CardPlace.DrawPile1) ? boardData.Places[CardPlace.DrawPile1].Count : 0);
            enemyDrawPile.SetCardsCount(boardData.Places.ContainsKey(CardPlace.DrawPile2) ? boardData.Places[CardPlace.DrawPile2].Count : 0);
            deck.SetCardsCount(boardData.Places.ContainsKey(CardPlace.Deck) ? boardData.Places[CardPlace.Deck].Count : 0);

            playerHand.UpdateCards(boardData.Places.ContainsKey(CardPlace.Hand1) ? boardData.Places[CardPlace.Hand1] : null);
            enemyHand.UpdateCards(boardData.Places.ContainsKey(CardPlace.Hand2) ? boardData.Places[CardPlace.Hand2] : null);
            shop.UpdateCards(boardData.Places.ContainsKey(CardPlace.Shop) ? boardData.Places[CardPlace.Shop] : null);
        }
    }
}
