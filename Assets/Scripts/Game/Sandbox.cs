using Grimmz.UI.Sandbox;
using Grimmz.Utils;
using UnityEngine;

namespace Grimmz
{
    public class Sandbox : Singleton<Sandbox>
    {
        public void Init()
        {
            // Debug.Log("Sandbox Init");
            UISandbox.Instance?.Init();
        }

        public void DeInit()
        {
            // Debug.Log("Sandbox DeInit");
            UISandbox.Instance?.DeInit();
        }
    }
}
