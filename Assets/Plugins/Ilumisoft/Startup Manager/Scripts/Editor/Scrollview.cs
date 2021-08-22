namespace Ilumisoft.Plugins.StartupManager.Editor
{
    using UnityEngine;
    using UnityEngine.Events;

    public class Scrollview
    {
        private Vector2 scrollPosition;

        public void OnGUILayout(UnityAction action)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, options: null);
            {
                action?.Invoke();
            }
            GUILayout.EndScrollView();
        }
    }
}