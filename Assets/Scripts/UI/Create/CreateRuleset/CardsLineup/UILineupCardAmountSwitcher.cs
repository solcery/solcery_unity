using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UILineupCardAmountSwitcher : MonoBehaviour
    {
        [SerializeField] private int min;
        [SerializeField] private int max;
        [SerializeField] private TextMeshProUGUI amountText = null;
        [SerializeField] private Button prevButton = null;
        [SerializeField] private Button nextButton = null;

        private Action<int> _onAmountChange;
        private int _currentAmount;

        public void Init(int initialAmount, Action<int> onAmountChange)
        {
            _onAmountChange = onAmountChange;

            SetAmount(initialAmount);

            prevButton.onClick.AddListener(Prev);
            nextButton.onClick.AddListener(Next);
        }

        private void Prev()
        {
            SetAmount(Mathf.Max(min, _currentAmount - 1));
        }

        private void Next()
        {
            SetAmount(Mathf.Min(max, _currentAmount + 1));
        }

        private void SetAmount(int newAmount)
        {
            if (newAmount != _currentAmount)
            {
                _currentAmount = newAmount;
                _onAmountChange?.Invoke(newAmount);
                SetAmountText();
                CheckButtons();
            }
        }

        private void SetAmountText()
        {
            if (amountText != null)
                amountText.text = _currentAmount.ToString();
        }

        private void CheckButtons()
        {
            prevButton.gameObject.SetActive(_currentAmount != min);
            nextButton.gameObject.SetActive(_currentAmount != max);
        }
    }
}
