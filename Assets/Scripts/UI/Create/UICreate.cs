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
    public class UICreate : Singleton<UICreate>
    {
        public UINodeEditor NodeEditor => nodeEditor;

        [SerializeField] private UINodeEditor nodeEditor = null;
        [SerializeField] private UICreateCard createCard = null;
        [SerializeField] private Button createButton = null;
        [SerializeField] private TextMeshProUGUI finishCardCreation = null;
        [SerializeField] private GameObject lockIcon = null;
        [SerializeField] private GameObject createButtonText = null;

        private CancellationTokenSource _cts;

        public async UniTask Init()
        {
            _cts = new CancellationTokenSource();
            await nodeEditor.Init();
            createCard?.Init();

            Reactives.Subscribe(nodeEditor.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                var cardName = string.IsNullOrEmpty(createCard.CardNameInput.text) ? "Card" : createCard.CardNameInput.text;
                var cardDescription = string.IsNullOrEmpty(createCard.CardDescriptionInput.text) ? "Description" : createCard.CardDescriptionInput.text;
                var cardPicture = createCard.CurrentPictureIndex;

                nodeEditor.BrickTree.MetaData.Name = cardName;
                nodeEditor.BrickTree.MetaData.Description = cardDescription;
                nodeEditor.BrickTree.MetaData.Picture = cardPicture;

                UICreatingCardPopup.Instance.Open(nodeEditor.BrickTree.MetaData);
                UnityToReact.Instance?.CallCreateCard();
                UINodeEditor.Instance?.DeleteGenesisBrickNode();
            });
        }

        public void DeInit()
        {
            _cts.Cancel();
            _cts.Dispose();

            nodeEditor?.DeInit();
            createCard?.DeInit();
            createButton.onClick.RemoveAllListeners();
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
