using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Solcery.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICreateRuleset : Singleton<UICreateRuleset>
    {
        public UICardsLineup LineupUnderPointer { get; private set; }

        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private RectTransform content = null;
        [SerializeField] private UICardsLineup initialCardsLineup = null;
        [SerializeField] private ScrollRect scrollRect = null;
        [SerializeField] private RectTransform placesRect = null;
        [SerializeField] private Button addPlaceButton = null;
        [SerializeField] private GameObject cardsLineupPrefab = null;

        private List<UICardsLineup> _places;

        public void Init(Action onRebuild)
        {
            _places = new List<UICardsLineup>();

            UICreate.Instance.OnGlobalRebuild += () =>
             {
                RebuildScrollRect().Forget();
                 LayoutRebuilder.ForceRebuildLayoutImmediate(content);
                 LayoutRebuilder.MarkLayoutForRebuild(content);
             };

            initialCardsLineup?.Init(() => RebuildScrollRect().Forget(), OnPointerEnterLineup, OnPointerExitLineup);
            addPlaceButton?.onClick.AddListener(CreatePlace);
        }

        public void DeInit()
        {

        }

        public void Open()
        {
            canvas.enabled = true;
            canvasGroup.blocksRaycasts = true;
        }

        public void Close()
        {
            canvas.enabled = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OnPointerEnterLineup(UICardsLineup lineup)
        {
            LineupUnderPointer = lineup;
        }

        private void OnPointerExitLineup(UICardsLineup lineup)
        {
            LineupUnderPointer = null;
        }

        private void CreatePlace()
        {
            var cardPlace = Instantiate(cardsLineupPrefab, placesRect).GetComponent<UICardsLineup>();
            cardPlace.transform.SetSiblingIndex(_places.Count);
            cardPlace.Init(() => RebuildScrollRect().Forget(), OnPointerEnterLineup, OnPointerExitLineup);
            _places.Add(cardPlace);

            RebuildScrollRect().Forget();
        }

        private async UniTaskVoid RebuildScrollRect()
        {
            Debug.Log("rebuilding");
            var vnp = scrollRect.verticalNormalizedPosition;
            Debug.Log(vnp);
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            await UniTask.WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = vnp;
        }
    }
}
