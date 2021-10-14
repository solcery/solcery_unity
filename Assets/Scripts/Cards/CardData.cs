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
        public List<CardAttr> Attrs = new List<CardAttr>();

        public bool TryGetAttrValue(string name, out int value)
        {
            if (Attrs != null || Attrs.Count > 0)
            {
                foreach (var attr in Attrs)
                {
                    if (attr.Name == name)
                    {
                        value = attr.Value;
                        return true;
                    }
                }
            }

            value = 0;
            return false;
        }
    }
}
