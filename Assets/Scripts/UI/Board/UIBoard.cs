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
        [SerializeField] private UIPlayedThisTurn playedThisTurn = null;
        [SerializeField] private Button endTurnButton = null;

        private BoardData _boardData;

        public void Init()
        {
            endTurnButton?.onClick.AddListener(() => OnEndTurnButtonClicked());
        }

        public void DeInit()
        {
            endTurnButton?.onClick.RemoveAllListeners();
        }

        public void OnBoardUpdate(BoardData boardData)
        {
            _boardData = boardData;

            this.gameObject.SetActive(true);

            player?.OnBoardUpdate(boardData, boardData.MyIndex);
            enemy?.OnBoardUpdate(boardData, boardData.EnemyIndex);

            deck?.SetCardsCount(boardData.Places.ContainsKey(CardPlace.Deck) ? boardData.Places[CardPlace.Deck].Count : 0);
            shop?.UpdateCards(boardData.Places.ContainsKey(CardPlace.Shop) ? boardData.Places[CardPlace.Shop] : null);
            playedThisTurn?.UpdateCards(boardData.Places.ContainsKey(CardPlace.PlayedThisTurn) ? boardData.Places[CardPlace.PlayedThisTurn] : null);

            endTurnButton?.gameObject.SetActive(boardData.Me.IsActive);
            endTurnButton.interactable = boardData.Me.IsActive;
        }

        private void OnEndTurnButtonClicked()
        {
            if (_boardData != null && _boardData.Me != null && _boardData.Me.IsActive)
            {
                UnityToReact.Instance.CallUseCard(_boardData.EndTurnCardId);
                endTurnButton.interactable = false;
            }
        }
    }
}
