using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Cards/CardFrames", fileName = "CardFrames")]
    public class CardFrames : SerializedScriptableObject
    {
        public Sprite CardFrontSprite => cardFrontSprite;
        public Sprite CardBackSprite => cardBackSprite;

        [SerializeField] private Sprite cardFrontSprite = null;
        [SerializeField] private Sprite cardBackSprite = null;
    }
}
