using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class GameHotkeys : UpdateableSingleton<GameHotkeys>
    {
        [Multiline(5)] [SerializeField] private string gameContent1;

        [Multiline(5)] [SerializeField] private string gameDisplay1;
        [Multiline(5)] [SerializeField] private string gameDisplay2;
        [Multiline(5)] [SerializeField] private string gameDisplay3;

        [Multiline(5)] [SerializeField] private string gameState1;
        [Multiline(5)] [SerializeField] private string gameState2;
        [Multiline(5)] [SerializeField] private string gameState3;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.G))
            {
                
            }

            if (Input.GetKeyDown(KeyCode.S))
            {

            }

            if (Input.GetKeyDown(KeyCode.D))
            {

            }

            if (Input.GetKeyDown(KeyCode.Z))
            {

            }

            if (Input.GetKeyDown(KeyCode.X))
            {

            }

            if (Input.GetKeyDown(KeyCode.C))
            {

            }
#endif
        }
    }
}
