using System;

namespace Solcery
{
    [Serializable]
    public class GameOverData
    {
        public string Title;
        public string Description;
        public Action Callback;
        public GameOverData(string title, string description, Action callback)
        {
            Title = title;
            Description = description;
            Callback = callback;
        }
    }
}
