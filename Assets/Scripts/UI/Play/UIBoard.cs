using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Play
{
    public class UIBoard : MonoBehaviour
    {
        [SerializeField] private UIPlayer player = null;
        [SerializeField] private UIPlayer enemy = null;
        [SerializeField] private UIShop shop = null;
        [SerializeField] private UIDrawPile deck = null;
        [SerializeField] private Button endTurnButton = null;

        public void Init()
        {

        }

        public void DeInit()
        {
            endTurnButton?.onClick.RemoveAllListeners();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            this.gameObject.SetActive(true);

            player?.OnBoardUpdate(boardData, 0);
            enemy?.OnBoardUpdate(boardData, 1);

            deck.SetCardsCount(boardData.Places.ContainsKey(CardPlace.Deck) ? boardData.Places[CardPlace.Deck].Count : 0);
            shop.UpdateCards(boardData.Places.ContainsKey(CardPlace.Shop) ? boardData.Places[CardPlace.Shop] : null);

            endTurnButton.gameObject.SetActive(boardData.Players[0].IsActive);

            if (boardData.Players[0].IsActive)
                endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked(boardData));
            else
                endTurnButton?.onClick.RemoveAllListeners();
        }

        private void OnEndTurnButtonClicked(BoardData boardData)
        {
            UnityToReact.Instance.CallUseCard(boardData.EndTurnCard.MintAddress, boardData.EndTurnCard.CardId);
        }
    }
}
