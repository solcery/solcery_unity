using UnityEngine;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;

#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.captureAllKeyboardInput = false;
#endif
            Dapp.Instance?.Init();
        }

        void OnDestroy()
        {
            Dapp.Instance?.DeInit();
        }
    }
}
