using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Solcery.Modules;
using Solcery.Utils;

namespace Solcery
{
    public class Game : Singleton<Game>
    {
        public AsyncReactiveProperty<GameContent> GameContent => _gameContent;
        public AsyncReactiveProperty<GameDisplay> GameDisplay => _gameDisplay;
        public AsyncReactiveProperty<GameState> GameState => _gameState;

        private AsyncReactiveProperty<GameContent> _gameContent = new AsyncReactiveProperty<GameContent>(null);
        private AsyncReactiveProperty<GameDisplay> _gameDisplay = new AsyncReactiveProperty<GameDisplay>(null);
        private AsyncReactiveProperty<GameState> _gameState = new AsyncReactiveProperty<GameState>(null);

        public void UpdateGameContent(string gameContentJson)
        {
            var gameContent = JsonConvert.DeserializeObject<GameContent>(gameContentJson)?.Prettify();
            _gameContent.Value = gameContent;
            CardPicturesFromUrl.Instance.BasicLoad(gameContent).Forget();
        }

        public void UpdateGameDisplay(string gameDisplayJson)
        {
            var gameDisplay = JsonConvert.DeserializeObject<GameDisplay>(gameDisplayJson)?.Prettify();
            _gameDisplay.Value = gameDisplay;
        }

        public void UpdateGameState(string gameStateJson)
        {
            var gameState = JsonConvert.DeserializeObject<GameState>(gameStateJson)?.Prettify();
            _gameState.Value = gameState;
        }
    }
}
