using System;

namespace Solcery
{
    [Serializable]
    public class GameOverPopupData
    {
        public string Title;
        public string Description;
        public Action Callback;
        public GameOverPopupData(string title, string description, Action callback)
        {
            Title = title;
            Description = description;
            Callback = callback;
        }
    }
}
