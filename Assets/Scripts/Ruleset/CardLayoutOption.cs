using System;

namespace Solcery.Ruleset
{
    [Serializable]
    public enum CardLayoutOption
    {
        Stacked,
        LayedOut,
        Map, //card name : amount
        Title //name of the top card
    }
}
