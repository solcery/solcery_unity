using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Solcery.UI.Create.NodeEditor
{
    public class UINodeEditorScrollView : ScrollRect
    {
        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
        }
    }
}
