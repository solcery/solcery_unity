using System;
// using Cysharp.Threading.Tasks;
using Solcery.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.NodeEditor
{
    public class UINodeEditor : UpdateableSingleton<UINodeEditor>
    {
        // [NonSerialized] public Action OnBrickInputChanged = () => { Debug.Log("boom"); };

        void OnBrickInputChanged()
        {
            _subscribee?.Invoke();
        }

        Action _subscribee;

        public void Subscribe(Action subscribee)
        {
            _subscribee = subscribee;
        }

        public BrickTree BrickTree => _brickTree;

        [SerializeField] private UINodeEditorZoom zoom = null;
        [SerializeField] private float horizontalPadding;
        [SerializeField] private float verticalPadding;
        [SerializeField] private GameObject brickNodePrefab = null;
        [SerializeField] private GameObject selectBrickNodePrefab = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private ScrollRect scrollView = null;
        [SerializeField] private BrickConfigs brickConfigs = null;
        [SerializeField] private UINodeEditorClipboard clipboard = null;
        [SerializeField] private UINodeEditorNodeSelector nodeSelector = null;
        [SerializeField] private RectTransform rect = null;
        [SerializeField] private GameObject contentBlocker = null;
        [SerializeField] private Button contentBlockerButton = null;
        [SerializeField] private UIBrickSubtypePopup subtypePopup = null;
        [SerializeField] private TextMeshProUGUI waitingForDataText = null;
        [SerializeField] private TextMeshProUGUI startHereText = null;

        private BrickTree _brickTree;
        private UINode _genesisNode;
        private bool _isNullGenesisValid;
        private BrickType _genesisBrickType;

        public void SetWaitingForData(bool isWaiting)
        {
            waitingForDataText?.gameObject.SetActive(isWaiting);
        }

        // public void Init()
        // {
        //     // Debug.Log("Init");
        //     brickConfigs.Init();
        //     clipboard?.Init(nodeSelector, RebuildAll);
        //     nodeSelector?.Init();
        //     zoom.SetActive(true);

        //     CreateFirstButton();
        // }

        public void Init(BrickTree brickTree, BrickType genesisBrickType, bool isNullGenesisValid)
        {
            // Debug.Log("Init with brickTree");
            _isNullGenesisValid = isNullGenesisValid;
            _genesisBrickType = genesisBrickType;

            clipboard?.Init(nodeSelector, RebuildAll);
            nodeSelector?.Init();
            zoom.SetActive(true);

            if (brickTree == null)
                CreateFirstButton();
            else
                OpenBrickTree(brickTree);
        }

        private void CreateFirstButton()
        {
            _brickTree = new BrickTree();
            startHereText?.gameObject?.SetActive(true);
            CreateFromBrickData(_brickTree.Genesis, null, null, 0);
            Rebuild();
        }

        public void OpenBrickTree(BrickTree brickTree)
        {
            _brickTree = brickTree;
            RebuildAll();

            if (_brickTree != null && _brickTree.Genesis != null)
            {
                startHereText?.gameObject?.SetActive(false);
                scrollView.enabled = true;
                contentBlocker.gameObject.SetActive(false);
                contentBlockerButton.onClick.RemoveAllListeners();
                subtypePopup.Close();
                zoom.SetActive(true);
                Debug.Log("checkValidity");
                _brickTree.CheckValidity(_isNullGenesisValid);
            }
            else
                startHereText?.gameObject?.SetActive(true);
        }

        public void CreateNewBrickTree()
        {
            _brickTree = new BrickTree();
            RebuildAll();
        }

        public void DeInit()
        {
            clipboard?.DeInit();
            nodeSelector?.DeInit();
            _brickTree = null;
        }

        private float _maxWidth;
        private float _maxHeight;

        public void RebuildAll()
        {
            if (_genesisNode != null)
            {
                _genesisNode.gameObject.SetActive(false);
                Destroy(_genesisNode.gameObject);
            }
            _genesisNode = CreateFromBrickData(_brickTree.Genesis, null, content, 0);

            Rebuild();
        }

        public void Rebuild()
        {
            _brickTree?.CheckValidity(_isNullGenesisValid);

            _maxWidth = _genesisNode.GetMaxWidth();
            _maxHeight = _genesisNode.GetMaxHeight();

            rect.sizeDelta = new Vector2(_maxWidth + horizontalPadding, _maxHeight + verticalPadding);
            _genesisNode.rect.localPosition = new Vector2(-_genesisNode.Width / 2, _genesisNode.rect.localPosition.y - 50);
            _genesisNode.Rebuild();
        }

        public override void PerformUpdate()
        {
            if (rect != null)
                rect.sizeDelta = new Vector2(_maxWidth + horizontalPadding / rect.localScale.x, _maxHeight + verticalPadding / rect.localScale.y);

            if (_genesisNode != null)
                _genesisNode.rect.localPosition = new Vector2(-_genesisNode.Width / 2, _genesisNode.rect.localPosition.y);
        }

        public void OpenSubtypePopup(UISelectBrickNode button)
        {
            startHereText.gameObject.SetActive(false);
            contentBlocker.SetActive(true);
            contentBlocker.transform.SetAsLastSibling();
            contentBlockerButton.onClick.AddListener(() =>
            {
                if (_brickTree.Genesis == null)
                    startHereText.gameObject.SetActive(true);
                subtypePopup.Close();
                zoom.SetActive(true);
                contentBlocker.SetActive(false);
            });
            subtypePopup.gameObject.SetActive(true);
            subtypePopup.Open(button, brickConfigs, OnBrickAdded);
            zoom.SetActive(false);
        }

        private void OnBrickAdded(SubtypeNameConfig subtypeNameConfig, UISelectBrickNode button)
        {
            scrollView.enabled = true;
            contentBlocker.gameObject.SetActive(false);
            contentBlockerButton.onClick.RemoveAllListeners();
            subtypePopup.Close();
            zoom.SetActive(true);
            CreateBrickNode(subtypeNameConfig.Config, button);
        }

        private UINode CreateFromBrickData(BrickData brickData, UIBrickNode parentNode, Transform parentTransform, int indexInParentSlots)
        {
            if (brickData != null)
            {
                BrickConfig config = brickConfigs.GetConfigByTypeAndSubtype((BrickType)brickData.Type, brickData.Subtype);
                UIBrickNode brickNode = Instantiate(brickNodePrefab, parentTransform).GetComponent<UIBrickNode>();

                if (parentNode == null)
                    _genesisNode = brickNode;

                brickNode.Init(config, brickData, parentNode, indexInParentSlots, nodeSelector.OnBrickNodeHighlighted, nodeSelector.OnBrickNodeDeHighlighted, OnBrickInputChanged);

                if (brickData.Slots != null && brickData.Slots.Length > 0)
                {
                    for (int i = 0; i < brickData.Slots.Length; i++)
                    {
                        brickNode.NodeSlots[i] = CreateFromBrickData(brickData.Slots[i], brickNode, brickNode.transform, i);
                    }
                }

                return brickNode;
            }
            else
            {
                UISelectBrickNode selectBrickNode;

                if (parentNode == null)
                {
                    selectBrickNode = Instantiate(selectBrickNodePrefab, content).GetComponent<UISelectBrickNode>();
                    _genesisNode = selectBrickNode;
                    selectBrickNode.Init(_genesisBrickType, content);
                }
                else
                {
                    selectBrickNode = Instantiate(selectBrickNodePrefab, parentTransform).GetComponent<UISelectBrickNode>();
                    selectBrickNode.Init(parentNode.Config.Slots[indexInParentSlots].Type, parentTransform, parentNode, indexInParentSlots, parentNode.Slots.Slots[indexInParentSlots]);
                }

                return selectBrickNode;
            }
        }

        private void CreateBrickNode(BrickConfig config, UISelectBrickNode button)
        {
            button.DeInit();
            DestroyImmediate(button.gameObject);

            var brickData = new BrickData(config);

            if (button.Parent != null)
                button.Parent.Data.Slots[button.IndexInParentSlots] = brickData;
            else
            {
                _brickTree.SetGenesis(brickData);
            }

            RebuildAll();
        }

        public void DeleteBrickNode(UIBrickNode brickNode)
        {
            if (brickNode.Parent == null)
            {
                var selectBrickButton = Instantiate(selectBrickNodePrefab, content).GetComponent<UISelectBrickNode>();
                _genesisNode = selectBrickButton;
                _brickTree.SetGenesis(null);
                selectBrickButton.Init(_genesisBrickType, content);
                startHereText.gameObject.SetActive(true);
                DisableScrollView();
            }
            else
            {
                var selectBrickButton = Instantiate(selectBrickNodePrefab, brickNode.Parent.transform).GetComponent<UISelectBrickNode>();
                brickNode.Parent.Data.Slots[brickNode.IndexInParentSlots] = null;
                brickNode.Parent.Slots.Slots[brickNode.IndexInParentSlots].SetFilled(false);
                selectBrickButton.Init(brickNode.Config.Type, brickNode.Parent.transform, brickNode.Parent, brickNode.IndexInParentSlots, brickNode.Parent.Slots.Slots[brickNode.IndexInParentSlots]);
                brickNode.Parent.NodeSlots[brickNode.IndexInParentSlots] = selectBrickButton;
            }

            DestroyImmediate(brickNode.gameObject);
            Rebuild();
        }

        private void DisableScrollView()
        {
            var currentElasticity = scrollView.elasticity;
            scrollView.elasticity = 0;
            // await UniTask.NextFrame();
            scrollView.enabled = false;
            scrollView.elasticity = currentElasticity;
        }

        public void DeleteGenesisBrickNode()
        {
            if (_genesisNode is UIBrickNode)
                DeleteBrickNode(_genesisNode as UIBrickNode);
        }
    }
}