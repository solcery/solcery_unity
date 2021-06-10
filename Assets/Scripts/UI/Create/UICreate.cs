using Solcery.UI.Create.BrickEditor;
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
        public UIBrickEditor BrickEditor => brickEditor;

        [SerializeField] private UIBrickEditor brickEditor = null;
        [SerializeField] private UICreateCard createCard = null;
        [SerializeField] private Button createButton = null;
        [SerializeField] private TextMeshProUGUI finishCardCreation = null;
        [SerializeField] private GameObject lockIcon = null;

        private CancellationTokenSource _cts;

        public void Init()
        {
            _cts = new CancellationTokenSource();
            brickEditor?.Init();
            createCard?.Init();

            Reactives.SubscribeWithoutCurrent(brickEditor.BrickTree.IsValid, OnBrickTreeValidityChange, _cts.Token);

            createButton.onClick.AddListener(() =>
            {
                brickEditor.BrickTree.MetaData.Name = string.IsNullOrEmpty(createCard.CardNameInput.text) ? "Card" : createCard.CardNameInput.text;
                brickEditor.BrickTree.MetaData.Description = string.IsNullOrEmpty(createCard.CardDescriptionInput.text) ? "Description" : createCard.CardDescriptionInput.text;
                brickEditor.BrickTree.MetaData.Picture = createCard.CurrentPictureIndex;

                List<byte> buffer = new List<byte>();
                brickEditor.BrickTree.SerializeToBytes(ref buffer);
                UnityToReact.Instance?.CallCreateCard(buffer.ToArray());

                brickEditor.DeleteGenesisBrick();
            });
        }

        public void DeInit()
        {
            _cts.Cancel();
            _cts.Dispose();

            brickEditor?.DeInit();
            createCard?.DeInit();
            createButton.onClick.RemoveAllListeners();
        }

        private void OnBrickTreeValidityChange(bool isValid)
        {
            createButton.interactable = isValid;
            finishCardCreation.gameObject.SetActive(!isValid);
            lockIcon.gameObject.SetActive(!isValid);
        }
    }
}
