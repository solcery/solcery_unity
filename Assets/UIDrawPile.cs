using TMPro;
using UnityEngine;

public class UIDrawPile : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardsCountText = null;

    public void SetCardsCount(int cardsCount)
    {
        if (cardsCountText != null) cardsCountText.text = cardsCount.ToString();
    }
}
