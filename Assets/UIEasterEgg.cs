using System;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class UIEasterEgg : MonoBehaviour
    {
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private GameObject heart = null;
        [SerializeField] private Animator animator = null;

        private bool _isShowing;

        public async UniTaskVoid Show()
        {
            if (_isShowing)
                return;

            _isShowing = true;

            canvas.enabled = true;
            heart?.SetActive(true);
            animator.SetBool("Easter", true);

            await UniTask.Delay(TimeSpan.FromSeconds(3.5f));

            canvas.enabled = false;
            heart?.SetActive(false);

            _isShowing = false;
        }
    }
}
