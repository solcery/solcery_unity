using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Solcery.FSM
{
    public abstract class State : SerializedScriptableObject
    {
#pragma warning disable 1998
        public abstract UniTask Enter();
        public abstract UniTask Exit();
#pragma warning restore 1998

        public virtual void PerformUpdate() { }
    }
}