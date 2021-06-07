using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Grimmz.FSM.Game
{
    [CreateAssetMenu(menuName = "Grimmz/FSM/Game/States/Menu", fileName = "Menu")]
    public class MenuState : GameState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            Menu.Instance?.Init();
        }

        public override async UniTask Exit()
        {
            Menu.Instance?.DeInit();
            await base.Exit();
        }
    }
}
