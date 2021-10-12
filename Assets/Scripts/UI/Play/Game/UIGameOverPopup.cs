using System;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI
{
    public class UIGameOverPopup : Singleton<UIGameOverPopup>
    {
        [SerializeField] Canvas canvas = null;
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI descriptionText = null;

        [SerializeField] private Button exitButton = null;

        private GameOverPopupData _data;

        public async UniTaskVoid OpenWithDelay(float delay, GameOverPopupData data)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            Open(data);
        }

        public void Open(GameOverPopupData data)
        {
            if (data == null)
                return;

            _data = data;

            if (canvas != null) canvas.enabled = true;

            if (titleText != null) titleText.text = _data.Title;
            if (descriptionText != null) descriptionText.text = _data.Description;

            exitButton?.onClick?.RemoveAllListeners();
            exitButton?.onClick?.AddListener(Exit);
        }

        private void Exit()
        {
            if (canvas != null) canvas.enabled = false;
            // callback
        }
    }
}
