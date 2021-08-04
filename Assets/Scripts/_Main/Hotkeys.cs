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
                if (Board.Instance.BoardData.Value.Enemy.IsActive)
                    LogActionCreator.Instance?.EnemyCastCard(Board.Instance.BoardData.Value.EndTurnCardId);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Board.Instance?.UpdateWithTestJson();
                Log.Instance?.UpdateWithTestJson();
            }
#endif
        }
    }
}
