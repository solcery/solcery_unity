using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Solcery
{
    [Serializable]
    public class BrickConfigData
    {
        public string Name;
        public BrickType Type;
        public int Subtype;
        public string Description;

        public BrickConfigData(string name, BrickType type, int subtype, string description)
        {
            Name = name;
            Type = type;
            Subtype = subtype;
            Description = description;
        }
    }

    [CreateAssetMenu(menuName = "Solcery/Bricks/BrickConfig", fileName = "BrickConfig")]
    public class BrickConfig : SerializedScriptableObject, ISerializationCallbackReceiver
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
        public List<UIBrickSlotStruct> Slots;

        public BrickConfigData ToData()
        {
            return new BrickConfigData(this.name, Type, Convert.ToInt32(Subtype), Description);
        }

        public void FromData(BrickConfigData data)
        {
            Type = data.Type;
            Subtype = Type switch
            {
                BrickType.Action => (BrickSubtypeAction)data.Subtype,
                BrickType.Condition => (BrickSubtypeCondition)data.Subtype,
                BrickType.Value => (BrickSubtypeValue)data.Subtype,
                _ => (BrickSubtypeValue)data.Subtype
            };
            Description = data.Description;
        }
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
