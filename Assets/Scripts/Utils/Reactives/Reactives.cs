using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Solcery.Utils.Reactives
{
    public static class Reactives
    {
        public static void SubscribeTo<T>(AsyncReactiveProperty<T> property, Action<T> callback, CancellationToken cancellationToken)
        {
            property?.ForEachAsync(p => callback?.Invoke(p), cancellationToken);
        }
    }
}
