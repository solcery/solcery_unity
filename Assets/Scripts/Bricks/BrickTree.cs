using System.Collections.Generic;
using System.Linq;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery
{
    public class BrickTree
    {
        [HideInInspector]
        public BrickData Genesis;
        public CardMetadata MetaData;

        [NonSerialized][Newtonsoft.Json.JsonIgnore]
        public AsyncReactiveProperty<bool> IsValid = new AsyncReactiveProperty<bool>(false);

        public void SetGenesis(BrickData data)
        {
            Genesis = data;
            MetaData = new CardMetadata(true);
        }

        // public void SerializeToBytes(ref List<byte> buffer)
        // {
        //     List<byte> tmpBuffer = new List<byte>();
        //     MetaData.SerializeToBytes(ref tmpBuffer);
        //     buffer.AddRange(BitConverter.GetBytes(tmpBuffer.Count).ToList<byte>());
        //     buffer.AddRange(tmpBuffer);
        //     Genesis.SerializeToBytes(ref buffer);
        // }

        public void CheckValidity()
        {
            if (Genesis == null)
                IsValid.Value = false;
            else
                IsValid.Value = Genesis.IsValid();
        }

        public int GetDepth()
        {
            if (Genesis == null)
                return 0;

            return Genesis.GetDepth();
        }
    }
}
