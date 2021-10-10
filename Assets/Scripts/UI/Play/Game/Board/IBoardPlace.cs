using UnityEngine;

namespace Solcery.UI
{
    public interface IBoardPlace
    {
        PlaceDisplayData DisplayData { get; set; }
        bool AreCardsFaceDown { get; }
        Transform GetCardsParent();
        Vector3 GetCardDestination(int cardId);
        Vector3 GetCardRotation(int cardId);
        Vector2 GetCardSize(int cardId);
        void OnCardArrival(int cardId);
        void UpdateGameContent(GameContent gameContent);
    }
}
