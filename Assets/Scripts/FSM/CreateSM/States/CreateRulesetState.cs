using System;
using Cysharp.Threading.Tasks;
using Solcery.UI;
using Solcery.UI.Create;
using UnityEngine;

namespace Solcery.FSM.Create
{
    [CreateAssetMenu(menuName = "Solcery/FSM/Create/States/CreateRuleset", fileName = "CreateRuleset")]
    public class CreateRulesetState : CreateState
    {
        public override async UniTask Enter(Action<CreateTransition> performTransition)
        {
            await base.Enter(performTransition);
            UICreateRuleset.Instance?.Open();
            UICollection.Instance?.SetMode(UICollectionMode.CreateRuleset);
        }

        public override async UniTask Exit()
        {
            UICreateRuleset.Instance?.Close();
            await base.Exit();
        }
    }
}
