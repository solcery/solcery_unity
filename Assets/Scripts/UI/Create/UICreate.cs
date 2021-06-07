using Grimmz.UI.Create.BrickEditor;
using Grimmz.Utils;
using Grimmz.WebGL;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Grimmz.UI.Create
{
    public class UICreate : Singleton<UICreate>
    {
        public UIBrickEditor BrickEditor => brickEditor;

        [SerializeField] private UIBrickEditor brickEditor = null;
        [SerializeField] private UICreateCard createCard = null;
        [SerializeField] private Button createButton = null;

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
            });
        }

        public void DeInit()
        {
            createCard.DeInit();
            createButton.onClick.RemoveAllListeners();
        }
    }
}
