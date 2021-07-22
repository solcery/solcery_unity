using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Cards/CardFrames", fileName = "CardFrames")]
    public class CardFrames : SerializedScriptableObject
    {
#pragma warning disable 0414
        [SerializeField] private Sprite cardBackSprite = null;
        [SerializeField] private Sprite cardFrontSprite = null;
#pragma warning restore 0414
    }
}
