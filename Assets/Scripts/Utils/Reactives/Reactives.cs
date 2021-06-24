using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;

namespace Solcery.Utils.Reactives
{
    public static class Reactives
    {
        public static void Subscribe<T>(AsyncReactiveProperty<T> property, Action<T> callback, CancellationToken cancellationToken)
        {
            property?.ForEachAsync(p => callback?.Invoke(p), cancellationToken);
        }

        public static void SubscribeWithoutCurrent<T>(AsyncReactiveProperty<T> property, Action<T> callback, CancellationToken cancellationToken)
        {
            property?.WithoutCurrent().ForEachAsync(p => callback?.Invoke(p), cancellationToken);
        }

        public static void SubscribeToReadonly<T>(ReadOnlyAsyncReactiveProperty<T> property, Action<T> callback, CancellationToken cancellationToken)
        {
            property?.ForEachAsync(p => callback?.Invoke(p), cancellationToken);
        }

        public static void SubscribeToReadonlyWithoutCurrent<T>(ReadOnlyAsyncReactiveProperty<T> property, Action<T> callback, CancellationToken cancellationToken)
        {
            property?.WithoutCurrent().ForEachAsync(p => callback?.Invoke(p), cancellationToken);
        }
    }
}
