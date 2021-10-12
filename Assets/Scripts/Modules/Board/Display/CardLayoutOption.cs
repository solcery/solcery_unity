using System;

namespace Solcery
{
    [Serializable]
    public enum CardLayoutOption
    {
        Stacked,
        LayedOut,
        Widget, //Picture and Coins
        Title, //description of the top card
        Button //title of the top card and cast on click
    }
}
