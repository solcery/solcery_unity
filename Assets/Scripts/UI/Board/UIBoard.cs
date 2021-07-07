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

            player?.OnBoardUpdate(boardData, boardData.MyIndex);
            enemy?.OnBoardUpdate(boardData, boardData.EnemyIndex);

            deck.SetCardsCount(boardData.Places.ContainsKey(CardPlace.Deck) ? boardData.Places[CardPlace.Deck].Count : 0);
            shop.UpdateCards(boardData.Places.ContainsKey(CardPlace.Shop) ? boardData.Places[CardPlace.Shop] : null);

            endTurnButton.gameObject.SetActive(boardData.Me.IsActive);

            if (boardData.Me.IsActive)
                endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked(boardData));
            else
                endTurnButton?.onClick.RemoveAllListeners();
        }

        private void OnEndTurnButtonClicked(BoardData boardData)
        {
            UnityToReact.Instance.CallUseCard(boardData.EndTurnCardId);
        }
    }
}
