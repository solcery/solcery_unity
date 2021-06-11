using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickEditor : MonoBehaviour
    {
        public BrickTree BrickTree => _brickTree;

        [SerializeField] private UIBrickSubtypePopup subtypePopup = null;
        [SerializeField] private GameObject contentBlocker = null;
        [SerializeField] private GameObject horPrefab = null;
        [SerializeField] private GameObject vertPrefab = null;
        [SerializeField] private GameObject selectBrickButtonPrefab = null;
        [SerializeField] private RectTransform content = null;
        [SerializeField] private GameObject brickPrefab = null;
        [SerializeField] private TextMeshProUGUI helperText = null;
        [SerializeField] private Canvas canvas = null;

        [SerializeField] private RawImage screenshot = null;
        [SerializeField] private RectTransform scrollView = null;

        private BrickTree _brickTree;
        private UIBrick _genesisBrick;

        public void Init()
        {
            _brickTree = new BrickTree();
            CreateFirstButton();
        }

        public void DeInit()
        {

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
            CreateBrick(subtypeNameConfig.Config, button).Forget();
        }

        private async UniTaskVoid CreateBrick(BrickConfig config, UISelectBrickButton button)
        {
            DestroyImmediate(button.gameObject);


            // #if UNITY_WEBGL && !UNITY_EDITOR
            //             canvas.enabled = false;
            //             await UniTask.WaitForEndOfFrame();
            //             var texture = ScreenCapture.CaptureScreenshotAsTexture();
            //             screenshot.texture = ScreenCapture.CaptureScreenshotAsTexture();
            //             screenshot.gameObject.SetActive(true);
            // #endif

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
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)hor.transform);
                var selectBrickButton = Instantiate(selectBrickButtonPrefab, vert.transform).GetComponent<UISelectBrickButton>();
                selectBrickButton.Init(config.Slots[i].Type, vert.transform, brick, i);
            }

            _brickTree?.CheckValidity();

            // var depth = BrickTree.GetDepth();

            Application.targetFrameRate = 120;
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView);
            // canvas.enabled = false;

            // for (int i = 0; i < depth; i++)
            // {
            //     await UniTask.Delay(1);
            // }
            // canvas.enabled = true;
            // await UniTask.DelayFrame(1);

            // #if UNITY_WEBGL && !UNITY_EDITOR
            //             await UniTask.DelayFrame(5);
            //             screenshot.gameObject.SetActive(false);
            //             Object.Destroy(texture);
            //             canvas.enabled = true;
            // #endif

            Debug.Log(BrickTree.Genesis.GetDepth());
        }

        public async UniTask DeleteBrick(UIBrick brick)
        {
            //             canvas.enabled = false;
            // #if UNITY_WEBGL && !UNITY_EDITOR
            //             await UniTask.WaitForEndOfFrame();
            //             var texture = ScreenCapture.CaptureScreenshotAsTexture();
            //             screenshot.texture = ScreenCapture.CaptureScreenshotAsTexture();
            //             screenshot.gameObject.SetActive(true);
            // #endif

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

            if (_genesisBrick == null)
                helperText.gameObject.SetActive(true);

            DestroyImmediate(brick.Hor);
            DestroyImmediate(brick.gameObject);

            _brickTree?.CheckValidity();

            Application.targetFrameRate = 120;
            LayoutRebuilder.ForceRebuildLayoutImmediate(scrollView);
            // await UniTask.DelayFrame(1);


            // #if UNITY_WEBGL && !UNITY_EDITOR
            //             await UniTask.DelayFrame(5);
            //             screenshot.gameObject.SetActive(false);
            //             Object.Destroy(texture);
            // #endif

            //             canvas.enabled = true;
        }

        public void DeleteGenesisBrick()
        {
            DeleteBrick(_genesisBrick).Forget();
        }

        private void CreateFirstButton()
        {
            var selectBrickButton = Instantiate(selectBrickButtonPrefab, content).GetComponent<UISelectBrickButton>();
            selectBrickButton.Init(BrickType.Action, content);
        }
    }
}

