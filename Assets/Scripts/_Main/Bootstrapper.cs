using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            AnalyticsEvent.GameStart();
            var result1 = AnalyticsEvent.Custom("secret_found", new Dictionary<string, object>
            {
                { "secret_id", 123 },
                { "time_elapsed", Time.timeSinceLevelLoad }
            });
            AnalyticsResult result = Analytics.CustomEvent("Test");
            
            Application.targetFrameRate = 60;
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = true;
#endif
            Game.Instance?.Init();
        }
    }
}
