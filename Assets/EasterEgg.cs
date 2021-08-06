using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class EasterEgg : UpdateableBehaviour
    {
        [SerializeField] private UIEasterEgg ui = null;
        [SerializeField] private string code;
        [SerializeField] private float timeToEnterCode;

        private float _timer = 0f;
        private string _currentInput = null;

        public override void PerformUpdate()
        {
            _timer += Time.deltaTime;

            if (_timer > timeToEnterCode)
            {
                Reset();
                return;
            }

            var inputThisFrame = Input.inputString;

            if (inputThisFrame.Length > 0)
            {
                _currentInput += inputThisFrame;
            }

            if (_currentInput != null && _currentInput.Contains("sofi"))
            {
                Reset();
                ui?.Show().Forget();
            }
        }

        private void Reset()
        {
            _timer = 0f;
            _currentInput = null;
        }
    }
}
