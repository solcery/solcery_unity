using System.Runtime.InteropServices;
using Solcery.Utils;
using System;
using System.Collections.Generic;
using Solcery.UI.Create.NodeEditor;

namespace Solcery.WebGL
{
    public class UnityToReact : Singleton<UnityToReact>
    {
        [DllImport("__Internal")] private static extern void OnUnityLoaded(string message);
        [DllImport("__Internal")] private static extern void OpenLinkInNewTab(string link);
        [DllImport("__Internal")] private static extern void UpdateCard(string card);
        [DllImport("__Internal")] private static extern void UpdateRuleset(string ruleset);
        [DllImport("__Internal")] private static extern void CreateBoard();
        [DllImport("__Internal")] private static extern void JoinBoard(string gameKey);
        [DllImport("__Internal")] private static extern void UseCard(int card);

        public void CallOnUnityLoaded()
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
    OnUnityLoaded ("message");
#endif
        }

        public void CallOpenLinkInNewTab(string link)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
    OpenLinkInNewTab (link);
#endif
        }

        public void CallUpdateCard()
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            List<byte> buffer = new List<byte>();
            UINodeEditor.Instance?.BrickTree?.SerializeToBytes(ref buffer);
            string buf = String.Join("|", buffer.ToArray());
            UpdateCard(buf);
#endif
        }

        public void CallUpdateRuleset(string ruleset)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            UpdateRuleset(ruleset);
#endif
        }

        public void CallCreateBoard()
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
    CreateBoard();
#endif
        }

        public void CallJoinBoard(string gameKey)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
    JoinBoard(gameKey);
#endif
        }

        public void CallUseCard(int cardIndex)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
    UseCard(cardIndex);
#endif
        }
    }
}
