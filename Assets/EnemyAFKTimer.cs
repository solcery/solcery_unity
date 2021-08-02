using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using Solcery.Utils.Reactives;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery
{
    public class EnemyAFKTimer : UpdateableBehaviour
    {
        [SerializeField] private float afkTime;
        [SerializeField] private Image image = null;

        private CancellationTokenSource _cts;
        private Action _onTimerFinished;
        private bool _isActive;
        private float _timeSinceBecameActive;

        public void Init(AsyncReactiveProperty<bool> isEnemyActive, Action onTimerFinished)
        {
            _cts = new CancellationTokenSource();
            _onTimerFinished = onTimerFinished;

            Reactives.Subscribe(isEnemyActive, OnEnemyActiveChanged, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void OnEnemyActiveChanged(bool isEnemyActive)
        {
            _isActive = isEnemyActive;
        }

        public override void PerformUpdate()
        {
            if (!_isActive)
                return;

            _timeSinceBecameActive += Time.deltaTime;

            if (_timeSinceBecameActive >= afkTime)
            {
                _onTimerFinished?.Invoke();
                _timeSinceBecameActive = 0f;
                _isActive = false;
            }

            if (image != null)
                image.fillAmount = (float)(_timeSinceBecameActive / afkTime);
        }
    }
}
