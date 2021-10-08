using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Solcery.Modules;
using Solcery.Ruleset;
using Solcery.Utils;
using Solcery.Utils.Reactives;
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

        private bool hasBeenOpenedAtLeastOnce = false;

        private CancellationTokenSource _cts;
        private List<UIPlace> _places;

        public void Init()
        {
            _cts = new CancellationTokenSource();

            UICreate.Instance.OnGlobalRebuild += () =>
             {
                 RebuildScroll();
                 LayoutRebuilder.ForceRebuildLayoutImmediate(content);
                 LayoutRebuilder.MarkLayoutForRebuild(content);
             };

            _places = new List<UIPlace>();
            _places.Add(initialsPlace);

            addPlaceButton?.onClick.AddListener(CreatePlaceOnButton);

            Reactives.Subscribe(Collection.Instance?.CollectionData, UpdateCollection, _cts.Token);
        }

        private void UpdateCollection(CollectionData collectionData)
        {

            if (collectionData == null)
            {
                return;
            }

            if (collectionData.RulesetData == null)
            {
                return;
            }

            CreateFromRulesetData(collectionData, collectionData.RulesetData);
        }

        private void CreateFromRulesetData(CollectionData collectionData, RulesetData rulesetData)
        {
            initialsPlace?.InitFromRulesetData(collectionData, rulesetData, rulesetData.Deck[0], 0, () => RebuildScroll(), OnPointerEnterPlace, OnPointerExitPlace, null);

            for (int p = 1; p < rulesetData.Deck.Count; p++)
            {
                var placeData = rulesetData.Deck[p];

                var cardPlace = Instantiate(placePrefab, placesRect).GetComponent<UIPlace>();
                cardPlace.transform.SetSiblingIndex(_places.Count - 1);
                var initialPlaceId = _places[_places.Count - 1].PlaceId + 1;
                cardPlace.InitFromRulesetData(collectionData, rulesetData, placeData, initialPlaceId, () => RebuildScroll(), OnPointerEnterPlace, OnPointerExitPlace, DeletePlace);
                _places.Add(cardPlace);
            }

            RebuildScroll();

            createRulesetButton?.onClick.AddListener(UpdateRuleset);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void Open()
        {
            canvas.enabled = true;
            canvasGroup.blocksRaycasts = true;

            if (!hasBeenOpenedAtLeastOnce)
            {
                hasBeenOpenedAtLeastOnce = true;
                UICollection.Instance?.Open();
            }
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

                var placeDisplayDatas = place.Display.Data.DisplayDataByPlayer;
                if (placeDisplayDatas != null)
                {
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
            OldUnityToReact.Instance.CallUpdateRuleset(rulesetJson);
        }
    }
}
