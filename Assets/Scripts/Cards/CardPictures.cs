using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Cards/CardPictures", fileName = "CardPictures")]
    public class CardPictures : SerializedScriptableObject
    {
        [ListDrawerSettings(NumberOfItemsPerPage = 100)]
        [SerializeField]
        private List<Sprite> AllSprites = new List<Sprite>();

        public Sprite GetSpriteByIndex(int index)
        {
            if (index >= 0 && index < AllSprites.Count)
                return AllSprites[index];

            return null;
        }

        public SpriteIndex GetRandomSpriteIndex()
        {
            var randomIndex = Random.Range(0, AllSprites.Count);
            return new SpriteIndex(AllSprites[randomIndex], randomIndex);
        }

        public SpriteIndex GetNextSpriteIndex(int currentIndex)
        {
            var newIndex = (currentIndex + 1 < AllSprites.Count) ? currentIndex + 1 : 0;
            return new SpriteIndex(AllSprites[newIndex], newIndex);
        }

        public SpriteIndex GetPrevSpriteIndex(int currentIndex)
        {
            var newIndex = (currentIndex - 1 >= 0) ? currentIndex - 1 : AllSprites.Count - 1;
            return new SpriteIndex(AllSprites[newIndex], newIndex);
        }
    }

    public struct SpriteIndex
    {
        public Sprite Sprite;
        public int Index;

        public SpriteIndex(Sprite sprite, int index)
        {
            Sprite = sprite;
            Index = index;
        }
    }
}