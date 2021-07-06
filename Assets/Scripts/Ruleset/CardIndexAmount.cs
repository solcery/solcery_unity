using System;

namespace Solcery.Ruleset
{
    [Serializable]
    public struct CardIndexAmount
    {
        public int Index;
        public int Amount;

        public CardIndexAmount(int index, int amount)
        {
            Index = index;
            Amount = amount;
        }
    }
}
