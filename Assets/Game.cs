using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Solcery.Modules;
using Solcery.Utils;

namespace Solcery
{
    public class Game : Singleton<Game>
    {
        public AsyncReactiveProperty<GameContent> GameContent = new AsyncReactiveProperty<GameContent>(null);
        public AsyncReactiveProperty<GameDisplay> GameDisplay = new AsyncReactiveProperty<GameDisplay>(null);
        public AsyncReactiveProperty<GameState> GameState = new AsyncReactiveProperty<GameState>(null);

        public void UpdateGameContent(string gameContentJson)
        {
            var gameContent = JsonConvert.DeserializeObject<GameContent>(gameContentJson).Prettify();
            GameContent.Value = gameContent;
            CardPicturesFromUrl.Instance.BasicLoad(gameContent).Forget();
        }

        public void UpdateGameDisplay(string gameDisplayJson)
        {
            var gameDisplay = JsonConvert.DeserializeObject<GameDisplay>(gameDisplayJson).Prettify();
            GameDisplay.Value = gameDisplay;
        }

        public void UpdateGameState(string gameStateJson)
        {
            var gameState = JsonConvert.DeserializeObject<GameState>(gameStateJson).Prettify();
            GameState.Value = gameState;
        }
    }
}
