using Solcery.UI.Create.BrickEditor;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Solcery.UI.Create
{
    public class UICreate : UpdateableSingleton<UICreate>
    {
        public UIBrickEditor BrickEditor => brickEditor;

        [SerializeField] private UIBrickEditor brickEditor = null;
        [SerializeField] private UICreateCard createCard = null;
        [SerializeField] private Button createButton = null;
        [SerializeField] private TextMeshProUGUI finishCardCreation = null;

        public void Init()
        {
            createCard.Init();

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
            createCard.DeInit();
            createButton.onClick.RemoveAllListeners();
        }

        public override void PerformUpdate()
        {
            var isBrickTreeValid = brickEditor.BrickTree.IsValid();

            createButton.interactable = isBrickTreeValid;
            finishCardCreation.gameObject.SetActive(!isBrickTreeValid);
        }
    }
}
