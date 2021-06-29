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
        [SerializeField] private UICreateCard createCard = null;
        [SerializeField] private Button createButton = null;
        [SerializeField] private TextMeshProUGUI finishCardCreation = null;
        [SerializeField] private GameObject lockIcon = null;
        [SerializeField] private GameObject createButtonText = null;

        private CancellationTokenSource _cts;

        public async UniTask Init()
        {
            _cts = new CancellationTokenSource();
            await UINodeEditor.Instance.Init();
            createCard?.Init();

            Reactives.Subscribe(UINodeEditor.Instance.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                var cardName = string.IsNullOrEmpty(createCard.CardNameInput.text) ? "Card" : createCard.CardNameInput.text;
                var cardDescription = string.IsNullOrEmpty(createCard.CardDescriptionInput.text) ? "Description" : createCard.CardDescriptionInput.text;
                var cardPicture = createCard.CurrentPictureIndex;

                UINodeEditor.Instance.BrickTree.MetaData.Name = cardName;
                UINodeEditor.Instance.BrickTree.MetaData.Description = cardDescription;
                UINodeEditor.Instance.BrickTree.MetaData.Picture = cardPicture;

                UICreatingCardPopup.Instance.Open(UINodeEditor.Instance.BrickTree.MetaData);
                UnityToReact.Instance?.CallCreateCard();
                UINodeEditor.Instance?.DeleteGenesisBrickNode();
            });
        }

        public void DeInit()
        {
            _cts.Cancel();
            _cts.Dispose();

            UINodeEditor.Instance?.DeInit();
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
