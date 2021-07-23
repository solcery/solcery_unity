using UnityEngine;

namespace Solcery
{
    public interface IBoardPlace
    {
        bool AreCardsFaceDown { get; }
        Transform GetCardsParent();
        Vector3 GetCardDestination(int cardId);
        void OnCardArrival(int cardId);
    }
}
