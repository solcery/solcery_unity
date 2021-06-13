using UnityEngine;

namespace Solcery
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
            Game.Instance?.Init();
        }
    }
}
