using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICardDisplay : MonoBehaviour
    {
        public int CurrentPictureIndex => _currentPictureIndex;
        public TMP_InputField CardNameInput => cardNameInput;
        public TMP_InputField CardDescriptionInput => cardDescriptionInput;
        public TMP_InputField CardCoinsInput => cardCoinsInput;

        [SerializeField] private CardPictures cardPictures = null;
        [SerializeField] private Image cardPicture = null;
        [SerializeField] private Button prevPictureButton = null;
        [SerializeField] private Button nextPictureButton = null;
        [SerializeField] private TMP_InputField cardNameInput = null;
        [SerializeField] private TMP_InputField cardDescriptionInput = null;
        [SerializeField] private TMP_InputField cardCoinsInput = null;

        private int _currentPictureIndex = 0;

        public void Init()
        {
            RandomizePicture();

            prevPictureButton.onClick.AddListener(PrevPicture);
            nextPictureButton.onClick.AddListener(NextPicture);

            var caret = transform.GetComponentInChildren<TMP_SelectionCaret>();
            caret.maskable = false;
        }

        public void Init(CardMetadata metadata)
        {
            cardNameInput.text = metadata.Name;
            cardDescriptionInput.text = metadata.Description;
            cardCoinsInput.text = metadata.Coins.ToString();
            _currentPictureIndex = metadata.Picture;
            cardPicture.sprite = cardPictures.GetSpriteByIndex(metadata.Picture);

            var caret = transform.GetComponentInChildren<TMP_SelectionCaret>();
            caret.maskable = false;
        }

        public void DeInit()
        {
            prevPictureButton.onClick.RemoveAllListeners();
            nextPictureButton.onClick.RemoveAllListeners();
        }

        public void CreateNewCard()
        {
            RandomizePicture();
            cardNameInput.text = null;
            cardDescriptionInput.text = null;
            cardCoinsInput.text = 0.ToString();
        }

        private void RandomizePicture()
        {
            var random = cardPictures.GetRandomSpriteIndex();
            _currentPictureIndex = random.Index;
            cardPicture.sprite = random.Sprite;
        }

        private void PrevPicture()
        {
            var prev = cardPictures.GetPrevSpriteIndex(_currentPictureIndex);
            _currentPictureIndex = prev.Index;
            cardPicture.sprite = prev.Sprite;
        }

        private void NextPicture()
        {
            var next = cardPictures.GetNextSpriteIndex(_currentPictureIndex);
            _currentPictureIndex = next.Index;
            cardPicture.sprite = next.Sprite;
        }
    }
}
