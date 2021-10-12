using System;

namespace Solcery
{
    [Serializable]
    public class GameOverPopupData
    {
        public string Title;
        public string Description;

        public GameOverPopupData(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
