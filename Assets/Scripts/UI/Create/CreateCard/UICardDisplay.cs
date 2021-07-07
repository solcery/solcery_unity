using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardDisplay : MonoBehaviour
    {
        public int CurrentPictureIndex => _currentIndex;
        public TMP_InputField CardNameInput => cardNameInput;
        public TMP_InputField CardDescriptionInput => cardDescriptionInput;

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private Button prevPictureButton = null;
        [SerializeField] private Button nextPictureButton = null;
        [SerializeField] private TMP_InputField cardNameInput = null;
        [SerializeField] private TMP_InputField cardDescriptionInput = null;

        private int _currentIndex = 0;

        public void Init()
        {
            RandomizePicture();

            prevPictureButton.onClick.AddListener(PrevPicture);
            nextPictureButton.onClick.AddListener(NextPicture);
        }

        public void Init(CardMetadata metadata)
        {
            cardNameInput.text = metadata.Name;
            cardDescriptionInput.text = metadata.Description;
            cardPicture.sprite = cardPictures.GetSpriteByIndex(metadata.Picture);
        }

        public void DeInit()
        {
            prevPictureButton.onClick.RemoveAllListeners();
            nextPictureButton.onClick.RemoveAllListeners();
        }

        private void RandomizePicture()
        {
            var random = cardPictures.GetRandomSpriteIndex();
            _currentIndex = random.Index;
            cardPicture.sprite = random.Sprite;
        }

        private void PrevPicture()
        {
            var prev = cardPictures.GetPrevSpriteIndex(_currentIndex);
            _currentIndex = prev.Index;
            cardPicture.sprite = prev.Sprite;
        }

        private void NextPicture()
        {
            var next = cardPictures.GetNextSpriteIndex(_currentIndex);
            _currentIndex = next.Index;
            cardPicture.sprite = next.Sprite;
        }
    }
}
