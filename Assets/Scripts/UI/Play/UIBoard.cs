using UnityEngine;

namespace Solcery.UI.Play
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField] private UIPlayer player = null;
        [SerializeField] private UIPlayer enemy = null;
        [SerializeField] private UIShop shop = null;
        [SerializeField] private UIDrawPile deck = null;

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
            player?.OnBoardUpdate(boardData, 0);
            enemy?.OnBoardUpdate(boardData, 1);

            deck.SetCardsCount(boardData.Places.ContainsKey(CardPlace.Deck) ? boardData.Places[CardPlace.Deck].Count : 0);
            shop.UpdateCards(boardData.Places.ContainsKey(CardPlace.Shop) ? boardData.Places[CardPlace.Shop] : null);
        }
    }
}
