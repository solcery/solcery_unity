using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Solcery.Utils;

namespace Solcery
{
    public class GameTracker : Singleton<GameTracker>
    {
        public ReadOnlyAsyncReactiveProperty<GameAll> GameAll;

        public void Init()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            GameAll = Game.Instance?.GameContent.CombineLatest(Game.Instance?.GameDisplay, Game.Instance?.GameState, (x, y, z) => new GameAll(x, y, z)).ToReadOnlyAsyncReactiveProperty(ct);
        }

        public void DeInit()
        {
            GameAll = null;
        }
    }

    public class GameAll
    {
        public bool HasBeenProcessed;

        GameContent GameContent;
        GameDisplay GameDisplay;
        GameState GameState;

        public GameAll(GameContent gameContent, GameDisplay gameDisplay, GameState gameState)
        {
            this.GameContent = gameContent;
            this.GameDisplay = gameDisplay;
            this.GameState = gameState;
        }
    }
}
