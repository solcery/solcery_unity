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
            var customEventResult = AnalyticsEvent.Custom("secret_found", new Dictionary<string, object>
            {
                { "secret_id", 123 },
                { "time_elapsed", Time.timeSinceLevelLoad }
            });
            Debug.Log(customEventResult);
            var testEventResult = Analytics.CustomEvent("Test");
            Debug.Log(testEventResult);

            Application.targetFrameRate = 60;
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = true;
#endif
            Game.Instance?.Init();
        }
    }
}
