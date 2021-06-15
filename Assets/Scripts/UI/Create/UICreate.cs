using Solcery.UI.Create.NodeEditor;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Solcery.Utils.Reactives;
using System.Threading;

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

        public void Init()
        {
            _cts = new CancellationTokenSource();
            nodeEditor?.Init();
            createCard?.Init();

            Reactives.Subscribe(nodeEditor.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                nodeEditor.BrickTree.MetaData.Name = string.IsNullOrEmpty(createCard.CardNameInput.text) ? "Card" : createCard.CardNameInput.text;
                nodeEditor.BrickTree.MetaData.Description = string.IsNullOrEmpty(createCard.CardDescriptionInput.text) ? "Description" : createCard.CardDescriptionInput.text;
                nodeEditor.BrickTree.MetaData.Picture = createCard.CurrentPictureIndex;

                List<byte> buffer = new List<byte>();
                nodeEditor.BrickTree.SerializeToBytes(ref buffer);
                UnityToReact.Instance?.CallCreateCard(buffer.ToArray());

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
