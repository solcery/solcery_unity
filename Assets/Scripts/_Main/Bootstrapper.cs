using UnityEngine;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 30;
            Game.Instance?.Init();
        }
    }
}
