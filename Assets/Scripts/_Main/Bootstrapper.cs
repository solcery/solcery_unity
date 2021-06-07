using UnityEngine;

namespace Grimmz
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            Game.Instance?.Init();
        }
    }
}
