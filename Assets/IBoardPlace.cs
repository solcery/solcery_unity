using UnityEngine;

namespace Solcery
{
    public interface IBoardPlace
    {
        Transform GetCardsParent();
        Transform GetCardDestination(int cardId);
        void OnCardArrival(int cardId);
    }
}
