using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardsLineup : MonoBehaviour
    {
        [SerializeField] private GameObject lineupCardPrefab = null;
        [SerializeField] private GameObject droppableAreaPrefab = null;

        [SerializeField] private HorizontalLayoutGroup cardsLG = null;
        [SerializeField] private HorizontalLayoutGroup droppablesLG = null;

        private List<UILineupCard> _cards;

        private void AddCard()
        {
            var lineUpCard = Instantiate(lineupCardPrefab, cardsLG.transform).GetComponent<UILineupCard>();
            var before = Instantiate(droppableAreaPrefab, droppablesLG.transform).GetComponent<UIDroppableArea>();
            var after = Instantiate(droppableAreaPrefab, droppablesLG.transform).GetComponent<UIDroppableArea>();

            before.Init(lineUpCard, UIDroppableAreaOption.Before, OnDroppableAreaPointerEnter, OnDroppableAreaPointerExit);
        }

        private void OnDroppableAreaPointerEnter(UILineupCard card)
        {
            
        }

        private void OnDroppableAreaPointerExit(UILineupCard card)
        {

        }
    }
}
