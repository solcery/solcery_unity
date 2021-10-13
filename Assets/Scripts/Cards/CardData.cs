using System;
using System.Collections.Generic;

namespace Solcery
{
    [Serializable]
    public class CardData
    {
        public int CardId;
        public int CardType;
        public int CardPlace;
        public List<CardAttr> Attrs;

        public bool TryGetAttrValue(string name, out int value)
        {
            foreach (var attr in Attrs)
            {
                if (attr.Name == name)
                {
                    value = attr.Value;
                    return true;
                }
            }

            value = 0;
            return false;
        }
    }
}
