using System.Collections.Generic;
using UnityEngine;

namespace Solcery.UI.Create.BrickEditor
{
    public class UIBrickSlots : MonoBehaviour
    {
        public List<UIBrickSlot> Slots => _slots;

        [SerializeField] private GameObject brickSlotPrefab = null;

        private List<UIBrickSlot> _slots = new List<UIBrickSlot>();

        public void Init(List<UIBrickSlotStruct> slotStructs)
        {
            foreach (var slotStruct in slotStructs)
            {
                var slot = Instantiate(brickSlotPrefab, this.transform).GetComponent<UIBrickSlot>();
                slot.Init(slotStruct.Name);
                _slots.Add(slot);
            }
        }
    }
}
