using Solcery.Modules;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class Hotkeys : UpdateableSingleton<Hotkeys>
    {
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                Board.Instance?.SaveBoardData1();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Board.Instance?.SaveBoardData2();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                Game.Instance?.UpdateWithGameContent1();
            }
#endif
        }
    }
}
