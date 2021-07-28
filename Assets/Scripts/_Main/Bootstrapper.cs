using UnityEngine;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
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
