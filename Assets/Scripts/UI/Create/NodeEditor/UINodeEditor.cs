using Cysharp.Threading.Tasks;
using Solcery.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINodeEditor : Singleton<UINodeEditor>
    {
        public BrickTree BrickTree => _brickTree;
        public GameObject BrickNodePrefab;
        public GameObject SelectBrickNodePrefab;
        public UINode Genesis;
        [SerializeField] private BrickConfigs brickConfigs = null;
        [SerializeField] private RectTransform rect = null;
        [SerializeField] private GameObject contentBlocker = null;
        [SerializeField] private Button contentBlockerButton = null;
        [SerializeField] private UIBrickSubtypePopup subtypePopup = null;
        [SerializeField] private TextMeshProUGUI helperText = null;

        private BrickTree _brickTree;
        private UINode _genesisNode;

        public BrickConfig genesisConfig;
        public BrickConfig config1;
        public BrickConfig config2;
        public BrickConfig config3;
        public BrickConfig config4;

        public async UniTask Init()
        {
            await brickConfigs.Init();

            _brickTree = new BrickTree();

            CreateFirstButton();
        }

        public void DeInit()
        {
            _brickTree = null;
        }

        public void Rebuild()
        {
            _brickTree?.CheckValidity();

            var maxWidth = Genesis.GetMaxWidth();
            var maxHeight = Genesis.GetMaxHeight();

            rect.sizeDelta = new Vector2(maxWidth + 400, maxHeight + 400);
            Genesis.rect.localPosition = new Vector2(-Genesis.Width / 2, Genesis.rect.localPosition.y);
            Genesis.Rebuild();
        }

        public void OpenSubtypePopup(UISelectBrickNode button)
        {
            helperText.gameObject.SetActive(false);
            contentBlocker.SetActive(true);
            contentBlocker.transform.SetAsLastSibling();
            contentBlockerButton.onClick.AddListener(() =>
            {
                subtypePopup.Close();
                contentBlocker.SetActive(false);
            });
            subtypePopup.gameObject.SetActive(true);
            subtypePopup.Open(button, brickConfigs, OnBrickAdded);
        }

        private void OnBrickAdded(SubtypeNameConfig subtypeNameConfig, UISelectBrickNode button)
        {
            contentBlocker.gameObject.SetActive(false);
            contentBlockerButton.onClick.RemoveAllListeners();
            subtypePopup.Close();
            CreateBrickNode(subtypeNameConfig.Config, button);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Rebuild();
        }

        private void CreateFirstButton()
        {
            // var selectBrickNode = Instantiate(SelectBrickNodePrefab, rect).GetComponent<UISelectBrickNode>();
            // selectBrickNode.Init(BrickType.Action, transform);
            // Genesis = selectBrickNode;
            CreateFromBrickData(_brickTree.Genesis, null, null, 0);
            Rebuild();
        }

        private UINode CreateFromBrickData(BrickData brickData, UIBrickNode parentNode, Transform parentTransform, int indexInParentSlots)
        {
            if (brickData != null)
            {
                BrickConfig config = brickConfigs.GetConfigByTypeAndSubtype((BrickType)brickData.Type, brickData.Subtype);
                UIBrickNode brickNode = Instantiate(BrickNodePrefab, parentTransform).GetComponent<UIBrickNode>();

                if (parentNode == null)
                    Genesis = brickNode;

                brickNode.Init(config, brickData, parentNode, indexInParentSlots);

                for (int i = 0; i < brickData.Slots.Length; i++)
                {
                    brickNode.NodeSlots[i] = CreateFromBrickData(brickData.Slots[i], brickNode, brickNode.transform, i);
                }

                return brickNode;
            }
            else
            {
                UISelectBrickNode selectBrickNode;

                if (parentNode == null)
                {
                    selectBrickNode = Instantiate(SelectBrickNodePrefab, transform).GetComponent<UISelectBrickNode>();
                    Genesis = selectBrickNode;
                    selectBrickNode.Init(BrickType.Action, transform);
                }
                else
                {
                    selectBrickNode = Instantiate(SelectBrickNodePrefab, parentTransform).GetComponent<UISelectBrickNode>();
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

            if (_genesisNode != null)
            {
                _genesisNode.gameObject.SetActive(false);
                Destroy(_genesisNode.gameObject);
            }
            _genesisNode = CreateFromBrickData(_brickTree.Genesis, null, transform, 0);

            Rebuild();
        }

        public void DeleteBrickNode(UIBrickNode brickNode)
        {
            if (brickNode.Parent == null)
            {
                var selectBrickButton = Instantiate(SelectBrickNodePrefab, transform).GetComponent<UISelectBrickNode>();
                Genesis = selectBrickButton;
                _brickTree.SetGenesis(null);
                selectBrickButton.Init(BrickType.Action, transform);
                helperText.gameObject.SetActive(true);
            }
            else
            {
                var selectBrickButton = Instantiate(SelectBrickNodePrefab, brickNode.Parent.transform).GetComponent<UISelectBrickNode>();
                brickNode.Parent.Data.Slots[brickNode.IndexInParentSlots] = null;
                brickNode.Parent.Slots.Slots[brickNode.IndexInParentSlots].SetFilled(false);
                selectBrickButton.Init(brickNode.Config.Type, brickNode.Parent.transform, brickNode.Parent, brickNode.IndexInParentSlots, brickNode.Parent.Slots.Slots[brickNode.IndexInParentSlots]);
                brickNode.Parent.NodeSlots[brickNode.IndexInParentSlots] = selectBrickButton;
            }

            DestroyImmediate(brickNode.gameObject);
            Rebuild();
        }

        public void DeleteGenesisBrickNode()
        {
            if (Genesis is UIBrickNode)
                DeleteBrickNode(Genesis as UIBrickNode);
        }
    }
}