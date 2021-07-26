using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Solcery.FSM.Dapp
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Dapp/States/Create", fileName = "Create")]
    public class CreateState : DappState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            await Solcery.Create.Instance.Init();
        }

        public override async UniTask Exit()
        {
            Solcery.Create.Instance?.DeInit();
            await base.Exit();
        }
    }
}
