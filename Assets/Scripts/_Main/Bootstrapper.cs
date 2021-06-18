using UnityEngine;
using UnityEngine.Analytics;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
            AnalyticsEvent.GameStart();
            Analytics.FlushEvents();
#endif

            Application.targetFrameRate = 60;
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = true;
#endif
            Game.Instance?.Init();
        }
    }
}
