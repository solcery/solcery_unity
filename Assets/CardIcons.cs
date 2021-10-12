using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Cards/CardIcons", fileName = "CardIcons")]
    public class CardIcons : SerializedScriptableObject
    {
        [ListDrawerSettings(NumberOfItemsPerPage = 10)]
        [SerializeField]
        private Dictionary<CardIcon, Sprite> AllSprites = new Dictionary<CardIcon, Sprite>();

        public Sprite GetSpriteByCardIcon(CardIcon cardIcon)
        {
            if (cardIcon != CardIcon.None && AllSprites.TryGetValue(cardIcon, out var sprite))
                return sprite;

            return null;
        }
    }
}
