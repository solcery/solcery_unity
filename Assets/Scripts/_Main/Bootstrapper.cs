using UnityEngine;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            Game.Instance?.Init();
        }
    }
}
