using System.Runtime.InteropServices;
using Solcery.Utils;

namespace Solcery.React
{
    public class UnityToReact : Singleton<UnityToReact>
    {
        [DllImport("__Internal")] private static extern void OnUnityLoaded();
        [DllImport("__Internal")] private static extern void OnGameOverPopupButtonClicked();
        [DllImport("__Internal")] private static extern void OpenLinkInNewTab(string link);
        [DllImport("__Internal")] private static extern void CastCard(int cardId);

        public void CallOnUnityLoaded()
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            OnUnityLoaded ();
#endif
        }

        public void CallOnGameOverPopupButtonClicked()
        {
            UnityEngine.Debug.Log("GameOver");

#if (UNITY_WEBGL && !UNITY_EDITOR)
            OnGameOverPopupButtonClicked ();
#endif
        }

        public void CallOpenLinkInNewTab(string link)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            OpenLinkInNewTab (link);
#endif
        }

        public void CallCastCard(int cardId)
        {
            UnityEngine.Debug.Log($"CastCard: {cardId}");

#if (UNITY_WEBGL && !UNITY_EDITOR)
            CastCard(cardId);
#endif
        }
    }
}
