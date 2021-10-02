using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Solcery
{
    // [Serializable]
    public class BrickData
    {
        public bool IsValid()
        {
            if (Slots == null || Slots.Length == 0)
                return true;
            else
            {
                var areSlotsValid = true;

                foreach (var slot in Slots)
                {
                    if (slot == null)
                        return false;
                    else
                    {
                        if (!slot.IsValid())
                            areSlotsValid = false;
                    }

                }

                return areSlotsValid;
            }
        }

        public int Type = -1;
        public int Subtype = -1;
        public int Object = 0;

        public bool HasField = false;
        public int IntField = 0;
        public string StringField = null;
        public bool HasObjectSelection = false;

        [HideInInspector]
        // [NonSerialized]
        // [Newtonsoft.Json.JsonIgnore]
        public BrickData[] Slots;

        public BrickData(BrickConfig config)
        {
            Type = (int)config.Type;
            Subtype = config.Subtype;

            HasField = config.HasField;
            HasObjectSelection = config.HasObjectSelection;

            Slots = new BrickData[config.Slots.Count];
        }

        public BrickData()
        {

        }

        public void TurnInto(BrickData brickData)
        {
            this.Type = brickData.Type;
            this.Subtype = brickData.Subtype;
            this.Object = brickData.Object;
            this.HasField = brickData.HasField;
            this.IntField = brickData.IntField;
            this.StringField = brickData.StringField;
            this.HasObjectSelection = brickData.HasObjectSelection;

            this.Slots = new BrickData[brickData.Slots.Length];

            if (brickData.Slots.Length > 0)
            {
                for (int i = 0; i < brickData.Slots.Length; i++)
                {
                    this.Slots[i] = brickData.Slots[i] != null ? brickData.Slots[i].Clone : null;
                }
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public BrickData Clone
        {
            get
            {
                var clone = new BrickData();

                clone.Type = this.Type;
                clone.Subtype = this.Subtype;
                clone.Object = this.Object;
                clone.HasField = this.HasField;
                clone.IntField = this.IntField;
                clone.StringField = this.StringField;
                clone.HasObjectSelection = this.HasObjectSelection;

                clone.Slots = new BrickData[this.Slots.Length];

                if (this.Slots.Length > 0)
                {
                    for (int i = 0; i < this.Slots.Length; i++)
                    {
                        clone.Slots[i] = this.Slots[i] != null ? this.Slots[i].Clone : null;
                    }
                }

                return clone;
            }
        }

        // public void SerializeToBytes(ref List<byte> buffer)
        // {
        //     buffer.AddRange(BitConverter.GetBytes(Type).ToList<byte>());
        //     buffer.AddRange(BitConverter.GetBytes(Subtype).ToList<byte>());
        //     if (HasObjectSelection)
        //         buffer.AddRange(BitConverter.GetBytes(Object).ToList<byte>());
        //     if (HasField)
        //         buffer.AddRange(BitConverter.GetBytes(IntField).ToList<byte>());
        //     foreach (BrickData child in Slots)
        //         child?.SerializeToBytes(ref buffer);
        // }

        public int GetDepth()
        {
            if (Slots == null || Slots.Length == 0)
            {
                return 1;
            }

            var maxDepth = 1;

            foreach (var slot in Slots)
            {
                if (slot == null)
                {
                    continue;
                }

                var slotDepth = slot.GetDepth();

                if ((slotDepth + 1) > maxDepth)
                    maxDepth = 1 + slotDepth;
            }

            return maxDepth;
        }
    }

}
