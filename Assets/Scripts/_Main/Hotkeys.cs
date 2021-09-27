using Solcery.Modules;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class Hotkeys : UpdateableSingleton<Hotkeys>
    {
        [Multiline(20)] [SerializeField] private string gameContent1;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Board.Instance?.BoardData?.Value?.Enemy?.IsActive == true)
                    LogActionCreator.Instance?.EnemyCastCard(Board.Instance.BoardData.Value.EndTurnCardId);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Board.Instance?.UpdateWithTestJson();
                Log.Instance?.UpdateWithTestJson();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Board.Instance?.UpdateWithJson1();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Board.Instance?.UpdateWithJson2();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Log.Instance?.UpdateWithJson1();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Game.Instance?.UpdateWithJson(gameContent1);
            }
#endif
        }
    }
}
