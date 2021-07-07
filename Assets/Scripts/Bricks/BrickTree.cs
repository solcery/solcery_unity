using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery
{
    public class BrickTree
    {
        [HideInInspector]
        public BrickData Genesis;

        [NonSerialized]
        [Newtonsoft.Json.JsonIgnore]
        public AsyncReactiveProperty<bool> IsValid = new AsyncReactiveProperty<bool>(false);

        public void SetGenesis(BrickData data)
        {
            Genesis = data;
        }

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
