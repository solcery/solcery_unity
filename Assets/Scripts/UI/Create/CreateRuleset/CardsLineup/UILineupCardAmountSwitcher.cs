using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UILineupCardAmountSwitcher : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountText = null;
        [SerializeField] private Button prevButton = null;
        [SerializeField] private Button nextButton = null;

        private int _currentAmount;
        private Action<int> _onAmountChange;

        public void Init(int initialAmount, Action<int> onAmountChange)
        {
            _currentAmount = initialAmount;
            _onAmountChange = onAmountChange;

            SetAmount(_currentAmount);

            prevButton.onClick.AddListener(Prev);
            nextButton.onClick.AddListener(Next);
        }

        private void Prev()
        {
            SetAmount(Mathf.Max(1, _currentAmount - 1));
        }

        private void Next()
        {
            SetAmount(Mathf.Min(99, _currentAmount + 1));
        }

        private void SetAmount(int newAmount)
        {
            if (newAmount != _currentAmount)
            {
                _currentAmount = newAmount;
                _onAmountChange?.Invoke(newAmount);
                SetAmountText();
            }
        }

        private void SetAmountText()
        {
            if (amountText != null)
                amountText.text = _currentAmount.ToString();
        }
    }
}
