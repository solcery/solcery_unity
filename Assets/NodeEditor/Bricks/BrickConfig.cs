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
        public bool HasField;
        public UIBrickFieldType FieldType;
        public string FieldName;
        public bool HasObjectSelection;
        public List<UIBrickSlotStruct> Slots;

        public BrickConfigData Create(BrickConfig config)
        {
            Name = config.Name;
            Type = config.Type;
            Subtype = Convert.ToInt32(config.Subtype);
            Description = config.Description;
            HasField = config.HasField;
            FieldType = config.FieldType;
            FieldName = config.FieldName;
            HasObjectSelection = config.HasObjectSelection;
            Slots = config.Slots;

            return this;
        }
    }

    [CreateAssetMenu(menuName = "Solcery/Bricks/BrickConfig", fileName = "BrickConfig")]
    public class BrickConfig : SerializedScriptableObject, ISerializationCallbackReceiver
    {
        public string Name;
        public BrickType Type;
        public int Subtype;
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
            return new BrickConfigData().Create(this);
        }

        public void FromData(BrickConfigData data)
        {
            Name = data.Name;
            Type = data.Type;
            Subtype = data.Subtype;
            Description = data.Description;
            HasField = data.HasField;
            FieldType = data.FieldType;
            FieldName = data.FieldName;
            HasObjectSelection = data.HasObjectSelection;
            Slots = data.Slots;
        }
    }

    public enum UIBrickFieldType
    {
        Int,
        String
    }

    [Serializable]
    public struct UIBrickSlotStruct
    {
        public BrickType Type;
        public string Name;
    }
}
