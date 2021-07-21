using UnityEngine;

namespace Solcery
{
    public interface IBoardPlace
    {
        Transform GetCardsParent();
        Vector3 GetCardDestination(int cardId);
        void OnCardArrival(int cardId);
    }
}
