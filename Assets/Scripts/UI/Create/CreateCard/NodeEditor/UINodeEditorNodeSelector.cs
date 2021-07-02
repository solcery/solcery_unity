using UnityEngine;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINodeEditorNodeSelector : MonoBehaviour
    {
        public UIBrickNode BrickNodeHighlighted { get; private set; }

        public void Init()
        {

        }

        public void DeInit()
        {

        }

        public void OnBrickNodeHighlighted(UIBrickNode brickNode)
        {
            BrickNodeHighlighted = brickNode;
        }

        public void OnBrickNodeDeHighlighted(UIBrickNode brickNode)
        {
            BrickNodeHighlighted = null;
        }
    }
}
