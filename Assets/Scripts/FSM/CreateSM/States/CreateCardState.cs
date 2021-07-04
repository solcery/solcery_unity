using Cysharp.Threading.Tasks;
using Solcery.UI;
using Solcery.UI.Create;
using UnityEngine;

namespace Solcery.FSM.Create
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Create/States/CreateCard", fileName = "CreateCard")]
    public class CreateCardState : CreateState
    {
        public override async UniTask Enter()
        {
            await base.Enter();
            UICreateCard.Instance?.Open();
            UICollection.Instance?.SetMode(UICollectionMode.CreateCard);
        }

        public override async UniTask Exit()
        {
            UICreateCard.Instance?.Close();
            await base.Exit();
        }
    }
}
