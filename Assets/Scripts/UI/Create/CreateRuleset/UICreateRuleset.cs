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
                 RebuildScroll();
                 LayoutRebuilder.ForceRebuildLayoutImmediate(content);
                 LayoutRebuilder.MarkLayoutForRebuild(content);
             };

            initialCardsLineup?.Init(() => RebuildScroll(), OnPointerEnterLineup, OnPointerExitLineup, null);
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
            cardPlace.Init(() => RebuildScroll(), OnPointerEnterLineup, OnPointerExitLineup, DeletePlace);
            _places.Add(cardPlace);

            RebuildScroll();
        }

        private void DeletePlace(UICardsLineup place)
        {
            place.DeInit();
            _places.Remove(place);
            DestroyImmediate(place.gameObject);

            RebuildScroll();
        }

        private void RebuildScroll()
        {
            RebuildScrollRect().Forget();
        }

        private async UniTaskVoid RebuildScrollRect()
        {
            var vnp = scrollRect.verticalNormalizedPosition;
            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            await UniTask.WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(vnp, 0f, 1f);
        }
    }
}
