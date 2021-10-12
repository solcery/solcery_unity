using System;

namespace Solcery
{
    [Serializable]
    public enum CardLayoutOption
    {
        Stacked,
        LayedOut,
        Map, //card name : amount
        Title, //description of the top card
        Button //title of the top card and cast on click
    }
}
