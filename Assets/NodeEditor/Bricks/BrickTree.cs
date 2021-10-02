using System;
// using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery
{
    public class BrickTree
    {
        [HideInInspector]
        public BrickData Genesis;

        [NonSerialized]
        [Newtonsoft.Json.JsonIgnore]
        // public AsyncReactiveProperty<bool> IsValid = new AsyncReactiveProperty<bool>(false);

        public Action<bool> OnValidityChanged;
        public bool IsValid { get { return _isValid; } set { _isValid = value; OnValidityChanged?.Invoke(_isValid); } }
        private bool _isValid;

        public void SetGenesis(BrickData data)
        {
            Genesis = data;
        }

        public void CheckValidity(bool isNullGenesisValid = false)
        {
            if (Genesis == null)
                IsValid = isNullGenesisValid;
            else
                IsValid = Genesis.IsValid();
        }

        public int GetDepth()
        {
            if (Genesis == null)
                return 0;

            return Genesis.GetDepth();
        }
    }
}
