using Cysharp.Threading.Tasks;
using Solcery.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINodeEditor : UpdateableSingleton<UINodeEditor>
    {
        public BrickTree BrickTree => _brickTree;

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
        [SerializeField] private TextMeshProUGUI helperText = null;

        private BrickTree _brickTree;
        private UINode _genesisNode;

        public async UniTask Init()
        {
            await brickConfigs.Init();
            clipboard?.Init(nodeSelector, RebuildAll);
            nodeSelector?.Init();

            _brickTree = new BrickTree();

            CreateFirstButton();
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

        public void OpenBrickTree(BrickTree brickTree)
        {
            _brickTree = brickTree;
            RebuildAll();

            if (_brickTree != null && _brickTree.Genesis != null)
            {
                helperText.gameObject.SetActive(false);
                scrollView.enabled = true;
                contentBlocker.gameObject.SetActive(false);
                contentBlockerButton.onClick.RemoveAllListeners();
                subtypePopup.Close();
                _brickTree.CheckValidity();
            }
        }

        public void Rebuild()
        {
            _brickTree?.CheckValidity();

            _maxWidth = _genesisNode.GetMaxWidth();
            _maxHeight = _genesisNode.GetMaxHeight();

            rect.sizeDelta = new Vector2(_maxWidth + horizontalPadding, _maxHeight + verticalPadding);
            _genesisNode.rect.localPosition = new Vector2(-_genesisNode.Width / 2, _genesisNode.rect.localPosition.y);
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
            helperText.gameObject.SetActive(false);
            contentBlocker.SetActive(true);
            contentBlocker.transform.SetAsLastSibling();
            contentBlockerButton.onClick.AddListener(() =>
            {
                if (_brickTree.Genesis == null)
                    helperText.gameObject.SetActive(true);
                subtypePopup.Close();
                contentBlocker.SetActive(false);
            });
            subtypePopup.gameObject.SetActive(true);
            subtypePopup.Open(button, brickConfigs, OnBrickAdded);
        }

        private void OnBrickAdded(SubtypeNameConfig subtypeNameConfig, UISelectBrickNode button)
        {
            scrollView.enabled = true;
            contentBlocker.gameObject.SetActive(false);
            contentBlockerButton.onClick.RemoveAllListeners();
            subtypePopup.Close();
            CreateBrickNode(subtypeNameConfig.Config, button);
        }

        private void CreateFirstButton()
        {
            CreateFromBrickData(_brickTree.Genesis, null, null, 0);
            Rebuild();
        }

        private UINode CreateFromBrickData(BrickData brickData, UIBrickNode parentNode, Transform parentTransform, int indexInParentSlots)
        {
            if (brickData != null)
            {
                BrickConfig config = brickConfigs.GetConfigByTypeAndSubtype((BrickType)brickData.Type, brickData.Subtype);
                UIBrickNode brickNode = Instantiate(brickNodePrefab, parentTransform).GetComponent<UIBrickNode>();

                if (parentNode == null)
                    _genesisNode = brickNode;

                brickNode.Init(config, brickData, parentNode, indexInParentSlots, nodeSelector.OnBrickNodeHighlighted, nodeSelector.OnBrickNodeDeHighlighted);

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
                    selectBrickNode.Init(BrickType.Action, content);
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
                selectBrickButton.Init(BrickType.Action, content);
                helperText.gameObject.SetActive(true);
                DisableScrollView().Forget();
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

        private async UniTaskVoid DisableScrollView()
        {
            var currentElasticity = scrollView.elasticity;
            scrollView.elasticity = 0;
            await UniTask.NextFrame();
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