using UnityEngine;

namespace Solcery.UI.Play.Game.Board
{
    public interface IBoardPlace
    {
        bool AreCardsFaceDown { get; }
        Transform GetCardsParent();
        Vector3 GetCardDestination(int cardId);
        Vector3 GetCardRotation(int cardId);
        void OnCardArrival(int cardId);
    }
}
