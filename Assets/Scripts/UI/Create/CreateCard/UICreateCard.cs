using Solcery.UI.Create.NodeEditor;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Solcery.Utils.Reactives;
using System.Threading;
using Cysharp.Threading.Tasks;

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

        public async UniTask Init()
        {
            _cts = new CancellationTokenSource();
            await UINodeEditor.Instance.Init();
            cardDisplay?.Init();

            Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                var cardName = string.IsNullOrEmpty(cardDisplay.CardNameInput.text) ? "Card" : cardDisplay.CardNameInput.text;
                var cardDescription = string.IsNullOrEmpty(cardDisplay.CardDescriptionInput.text) ? "Description" : cardDisplay.CardDescriptionInput.text;
                var cardPicture = cardDisplay.CurrentPictureIndex;

                UINodeEditor.Instance.BrickTree.MetaData.Name = cardName;
                UINodeEditor.Instance.BrickTree.MetaData.Description = cardDescription;
                UINodeEditor.Instance.BrickTree.MetaData.Picture = cardPicture;

                UICreatingCardPopup.Instance.Open(UINodeEditor.Instance.BrickTree.MetaData);
                UnityToReact.Instance?.CallUpdateCard();
                UINodeEditor.Instance?.DeleteGenesisBrickNode();
            });
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
            Debug.Log("Open Card");
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
