using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery
{
    public class PlayerAFKTimer : UpdateableBehaviour
    {
        [SerializeField] private float afkTime;
        [SerializeField] private Image image = null;

        private CancellationTokenSource _cts;
        private Action _onTimerFinished;
        private bool _isActive;
        private float _timeSinceBecameActive;

        public void Init(AsyncReactiveProperty<bool> isPlayerActive, Action onTimerFinished)
        {
            _cts = new CancellationTokenSource();
            _onTimerFinished = onTimerFinished;

            Reactives.Subscribe(isPlayerActive, OnPlayerActiveChanged, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void OnPlayerActiveChanged(bool isPlayerActive)
        {
            _isActive = isPlayerActive;
        }

        public override void PerformUpdate()
        {
            if (!_isActive)
            {
                _timeSinceBecameActive = 0f;
                _isActive = false;
                SetFillAmount(0f);
                return;
            }

            _timeSinceBecameActive += Time.deltaTime;

            if (_timeSinceBecameActive >= afkTime)
            {
                if (_onTimerFinished != null)
                    _onTimerFinished?.Invoke();
            }

            SetFillAmount((float)(_timeSinceBecameActive / afkTime));
        }

        private void SetFillAmount(float fillAmount)
        {
            if (image != null)
                image.fillAmount = fillAmount;
        }
    }
}
