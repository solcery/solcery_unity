using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grimmz.UI.Create.BrickEditor
{
    public class UIBrickEditor : MonoBehaviour
    {
        public BrickTree BrickTree => _brickTree;

        [SerializeField] private UIBrickSubtypePopup subtypePopup = null;
        [SerializeField] private GameObject contentBlocker = null;
        [SerializeField] private GameObject horPrefab = null;
        [SerializeField] private GameObject vertPrefab = null;
        [SerializeField] private GameObject selectBrickButtonPrefab = null;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject brickPrefab = null;
        [SerializeField] private TextMeshProUGUI helperText = null;

        private BrickTree _brickTree;
        private UIBrick _genesisBrick;

        private void Start()
        {
            _brickTree = new BrickTree();
            CreateFirstButton();
        }

        public void OpenSubtypePopup(UISelectBrickButton button)
        {
            contentBlocker.gameObject.SetActive(true);
            contentBlocker.transform.SetAsLastSibling();
            subtypePopup.gameObject.SetActive(true);
            subtypePopup.Open(button, OnBrickAdded);

            helperText.gameObject.SetActive(false);
        }

        public void CloseSubtypePopup()
        {
            contentBlocker.gameObject.SetActive(false);
            subtypePopup.Close();
        }

        private void OnBrickAdded(SubtypeNameConfig subtypeNameConfig, UISelectBrickButton button)
        {
            CloseSubtypePopup();
            CreateBrick(subtypeNameConfig.Config, button);
        }

        private void CreateBrick(BrickConfig config, UISelectBrickButton button)
        {
            DestroyImmediate(button.gameObject);

            var brickData = new BrickData(config);

            var brick = Instantiate(brickPrefab, button.Vert).GetComponent<UIBrick>();
            var hor = Instantiate(horPrefab, button.Vert);

            brick.Init(config, brickData, button.Parent, button.IndexInParentSlots, button.Vert, hor);

            if (button.Parent == null)
            {
                _brickTree.SetGenesis(brickData);
                _genesisBrick = brick;
            }
            else
            {
                button.Parent.Data.Slots[button.IndexInParentSlots] = brickData;
                button.Parent.Slots.Slots[button.IndexInParentSlots].SetFilled(true);
            }

            for (int i = 0; i < config.Slots.Count; i++)
            {
                var vert = Instantiate(vertPrefab, hor.transform);
                var selectBrickButton = Instantiate(selectBrickButtonPrefab, vert.transform).GetComponent<UISelectBrickButton>();
                selectBrickButton.Init(config.Slots[i].Type, vert.transform, brick, i);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }

        public void DeleteBrick(UIBrick brick)
        {
            var selectBrickButton = Instantiate(selectBrickButtonPrefab, brick.Vert.transform).GetComponent<UISelectBrickButton>();

            if (brick.Parent == null)
            {
                _brickTree.SetGenesis(null);
                _genesisBrick = null;
                selectBrickButton.Init(BrickType.Action, content);
            }
            else
            {
                brick.Parent.Data.Slots[brick.IndexInParentSlots] = null;
                brick.Parent.Slots.Slots[brick.IndexInParentSlots].SetFilled(false);
                selectBrickButton.Init(brick.Config.Type, brick.Vert.transform, brick.Parent, brick.IndexInParentSlots);
            }

            if(_genesisBrick == null)
                helperText.gameObject.SetActive(true);

            DestroyImmediate(brick.Hor);
            DestroyImmediate(brick.gameObject);

            LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        }

        public void DeleteGenesisBrick()
        {
            DeleteBrick(_genesisBrick);
        }

        private void CreateFirstButton()
        {
            var selectBrickButton = Instantiate(selectBrickButtonPrefab, content).GetComponent<UISelectBrickButton>();
            selectBrickButton.Init(BrickType.Action, content);
        }
    }
}

