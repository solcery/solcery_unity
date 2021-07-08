using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Solcery.Modules.Collection;
using Solcery.Ruleset;
using Solcery.Utils;
using Solcery.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create
{
    public class UICreateRuleset : Singleton<UICreateRuleset>
    {
        public UIPlace PlaceUnderPointer { get; private set; }

        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasGroup canvasGroup = null;
        [SerializeField] private RectTransform content = null;
        [SerializeField] private UIPlace initialsPlace = null;
        [SerializeField] private ScrollRect scrollRect = null;
        [SerializeField] private CanvasGroup scrollCG = null;
        [SerializeField] private RectTransform placesRect = null;
        [SerializeField] private Button addPlaceButton = null;
        [SerializeField] private GameObject placePrefab = null;
        [SerializeField] private Button createRulesetButton = null;

        private List<UIPlace> _places;

        public void Init()
        {
            UICreate.Instance.OnGlobalRebuild += () =>
             {
                 RebuildScroll();
                 LayoutRebuilder.ForceRebuildLayoutImmediate(content);
                 LayoutRebuilder.MarkLayoutForRebuild(content);
             };

            _places = new List<UIPlace>();
            _places.Add(initialsPlace);

            addPlaceButton?.onClick.AddListener(CreatePlaceOnButton);

            if (Collection.Instance.CollectionData != null && Collection.Instance.CollectionData.Value != null && Collection.Instance.CollectionData.Value.RulesetData != null)
                CreateFromRulesetData(Collection.Instance.CollectionData.Value.RulesetData);
        }

        private void CreateFromRulesetData(RulesetData rulesetData)
        {
            initialsPlace?.InitFromRulesetData(rulesetData, rulesetData.Deck[0], 0, () => RebuildScroll(), OnPointerEnterPlace, OnPointerExitPlace, null);

            for (int p = 1; p < rulesetData.Deck.Count; p++)
            {
                var placeData = rulesetData.Deck[p];

                var cardPlace = Instantiate(placePrefab, placesRect).GetComponent<UIPlace>();
                cardPlace.transform.SetSiblingIndex(_places.Count - 1);
                var initialPlaceId = _places[_places.Count - 1].PlaceId + 1;
                cardPlace.InitFromRulesetData(rulesetData, placeData, initialPlaceId, () => RebuildScroll(), OnPointerEnterPlace, OnPointerExitPlace, DeletePlace);
                _places.Add(cardPlace);
            }

            RebuildScroll();

            createRulesetButton?.onClick.AddListener(UpdateRuleset);
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

        private void OnPointerEnterPlace(UIPlace place)
        {
            PlaceUnderPointer = place;
        }

        private void OnPointerExitPlace(UIPlace place)
        {
            PlaceUnderPointer = null;
        }

        private void CreatePlaceOnButton()
        {
            var cardPlace = Instantiate(placePrefab, placesRect).GetComponent<UIPlace>();
            cardPlace.transform.SetSiblingIndex(_places.Count - 1);
            var initialPlaceId = _places[_places.Count - 1].PlaceId + 1;
            cardPlace.Init(initialPlaceId, () => RebuildScroll(), OnPointerEnterPlace, OnPointerExitPlace, DeletePlace);
            _places.Add(cardPlace);

            RebuildScroll();
        }

        private void DeletePlace(UIPlace place)
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
            scrollCG.alpha = 0;
            var vnp = scrollRect.verticalNormalizedPosition;
            var hnp = scrollRect.horizontalNormalizedPosition;

            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
            await UniTask.WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(vnp, 0f, 1f);
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp(hnp, 0f, 1f);
            scrollCG.alpha = 1;
        }

        private void UpdateRuleset()
        {
            var rulesetData = new RulesetData();

            var cardMintAddresses = new List<string>();
            var deck = new List<PlaceData>();
            var rulesetDisplayData = new RulesetDisplayData();
            var playerDisplayDatasDict = new Dictionary<int, PlayerDisplayData>();

            for (int placeId = 0; placeId < _places.Count; placeId++)
            {
                var place = _places[placeId];
                var placeData = new PlaceData();
                placeData.PlaceId = place.PlaceId;

                for (int c = 0; c < place.Cards.Count; c++)
                {
                    var card = place.Cards[c];
                    var cardData = card.Data;
                    var cardMintAddress = cardData.CardType.MintAddress;
                    int cardIndex;

                    if (cardMintAddresses.Contains(cardMintAddress))
                    {
                        cardIndex = cardMintAddresses.IndexOf(cardMintAddress);
                    }
                    else
                    {
                        cardIndex = cardMintAddresses.Count;
                        cardMintAddresses.Add(cardMintAddress);
                    }

                    placeData.IndexAmount.Add(new CardIndexAmount(cardIndex, cardData.Amount));
                }

                deck.Add(placeData);

                var placeDisplayDatas = place.DisplayDatas;
                if (place.DisplayDatas != null) {
                    foreach (var idDisplayData in placeDisplayDatas)
                    {
                        var playerId = idDisplayData.Key;
                        var placeDisplayData = idDisplayData.Value;
                        placeDisplayData.PlaceId = placeId;

                        if (!playerDisplayDatasDict.ContainsKey(playerId))
                        {
                            playerDisplayDatasDict.Add(playerId, new PlayerDisplayData()
                            {
                                PlayerId = playerId,
                                PlaceDisplayData = new List<PlaceDisplayDataForPlayer>()
                                {
                                    placeDisplayData
                                }
                            });
                        }
                        else
                        {
                            var playerDisplayData = playerDisplayDatasDict[playerId];
                            playerDisplayData.PlaceDisplayData.Add(placeDisplayData);
                        }
                    }
                }
            }

            rulesetDisplayData.PlayerDisplayDatas = playerDisplayDatasDict.Values.ToList();

            rulesetData.CardMintAddresses = cardMintAddresses;
            rulesetData.Deck = deck;
            rulesetData.DisplayData = rulesetDisplayData;

            var rulesetJson = JsonUtility.ToJson(rulesetData);
            Debug.Log(rulesetJson);
            UnityToReact.Instance.CallUpdateRuleset(rulesetJson);
        }
    }
}
