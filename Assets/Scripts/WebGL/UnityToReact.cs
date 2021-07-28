using System.Runtime.InteropServices;
using Solcery.Utils;
using System;
using System.Collections.Generic;
using Solcery.Modules.Log;
using Solcery.UI.Create.NodeEditor;
using UnityEngine;
using Newtonsoft.Json;

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
        [DllImport("__Internal")] private static extern void UseCard(int cardId);
        [DllImport("__Internal")] private static extern void GameOverCallback(string callback);

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

        public void CallUpdateCard(CollectionCardType card)
        {
            var cardJson = JsonConvert.SerializeObject(card);

#if (UNITY_WEBGL && !UNITY_EDITOR)
            UpdateCard(cardJson);
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

        public void CallUseCard(int cardId)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            UseCard(cardId);
#else
            Log.Instance?.FakeCastCard(1, cardId);
#endif
        }

        public void CallGameOverCallback(string callback)
        {
#if (UNITY_WEBGL && !UNITY_EDITOR)
            GameOverCallback(callback);
#endif
        }
    }
}
