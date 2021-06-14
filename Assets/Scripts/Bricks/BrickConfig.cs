using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Solcery
{
    [CreateAssetMenu(menuName = "Solcery/Bricks/BrickConfig", fileName = "BrickConfig")]
    public class BrickConfig : SerializedScriptableObject
    {
        public BrickType Type;
        public System.Enum Subtype;
        [Multiline(5)] public string Description;

        public bool HasField;
        [ShowIf("HasField")] public UIBrickFieldType FieldType;
        [ShowIf("HasField")] public string FieldName;

        public bool HasObjectSelection;
        
        public bool DoesAddObject;
        [ShowIf("DoesAddObject")] public string AddedObjectName;

        public bool areSlotsExpandable;
        public List<UIBrickSlotStruct> Slots = new List<UIBrickSlotStruct>();
    }

    public enum UIBrickFieldType
    {
        Int,
        String
    }

    public struct UIBrickSlotStruct
    {
        public BrickType Type;
        public string Name;
    }
}
