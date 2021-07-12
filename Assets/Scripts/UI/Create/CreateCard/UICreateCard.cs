using Solcery.UI.Create.NodeEditor;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Solcery.Utils.Reactives;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace Solcery.UI.Create
{
    public class UICreateCard : Singleton<UICreateCard>
    {
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private UICardDisplay cardDisplay = null;
        [SerializeField] private Button createButton = null;
        [SerializeField] private TextMeshProUGUI finishCardCreation = null;
        [SerializeField] private GameObject lockIcon = null;
        [SerializeField] private GameObject createButtonText = null;

        private CancellationTokenSource _cts;

        private CollectionCardType _currentCard;

        public async UniTask Init()
        {
            _cts = new CancellationTokenSource();
            _currentCard = new CollectionCardType();

            await UINodeEditor.Instance.Init();
            cardDisplay?.Init();

            Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                var cardName = string.IsNullOrEmpty(cardDisplay.CardNameInput.text) ? "Card" : cardDisplay.CardNameInput.text;
                var cardDescription = string.IsNullOrEmpty(cardDisplay.CardDescriptionInput.text) ? "Description" : cardDisplay.CardDescriptionInput.text;

                int cardCoins = 0;
                if (!string.IsNullOrEmpty(cardDisplay.CardCoinsInput.text))
                    int.TryParse(cardDisplay.CardCoinsInput.text, out cardCoins);

                var cardPicture = cardDisplay.CurrentPictureIndex;

                _currentCard.Metadata = new CardMetadata();
                _currentCard.Metadata.Name = cardName;
                _currentCard.Metadata.Description = cardDescription;
                _currentCard.Metadata.Coins = cardCoins;
                _currentCard.Metadata.Picture = cardPicture;

                _currentCard.BrickTree = UINodeEditor.Instance.BrickTree;

                UICreatingCardPopup.Instance.Open(_currentCard.Metadata);
                UnityToReact.Instance?.CallUpdateCard(_currentCard);
                UINodeEditor.Instance?.DeleteGenesisBrickNode();
            });
        }

        public void CreateNewCard()
        {
            _currentCard = new CollectionCardType();
            UINodeEditor.Instance.CreateNewBrickTree();
            cardDisplay.CreateNewCard();
            Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);
        }

        public void DeInit()
        {
            _cts.Cancel();
            _cts.Dispose();

            UINodeEditor.Instance?.DeInit();
            cardDisplay?.DeInit();
            createButton.onClick.RemoveAllListeners();
        }

        public void Open()
        {
            canvas.enabled = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Close()
        {
            canvas.enabled = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void OpenCard(CollectionCardType cardType)
        {
            _currentCard = cardType;

            UINodeEditor.Instance.OpenBrickTree(_currentCard.BrickTree);
            Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);
            cardDisplay?.Init(_currentCard.Metadata);
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {
            createButton.interactable = isValid;
            finishCardCreation.gameObject.SetActive(!isValid);
            lockIcon.gameObject.SetActive(!isValid);
            createButtonText.gameObject.SetActive(isValid);
        }
    }
}
