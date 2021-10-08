using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Solcery.Modules;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class OldGame : Singleton<OldGame>
    {
        public AsyncReactiveProperty<GameContent> GameContent => _gameContent;
        private AsyncReactiveProperty<GameContent> _gameContent = new AsyncReactiveProperty<GameContent>(null);

        [SerializeField] private bool initWithTestGameContent = false;
        [ShowIf("initWithTestGameContent")] [Multiline(20)] [SerializeField] private string testGameContent;

        public void Init()
        {
            if (initWithTestGameContent)
                UpdateWithJson(testGameContent);
        }

        public void DeInit()
        {
            _gameContent = null;
        }

        public void UpdateWithJson(string json)
        {
            var gameContent = JsonConvert.DeserializeObject<GameContent>(json);
            UpdateGameContent(gameContent.Prettify()).Forget();
        }

        public async UniTaskVoid UpdateGameContent(GameContent gameContent)
        {
            _gameContent.Value = gameContent;
            await CardPicturesFromUrl.Instance.BasicLoad(gameContent);
        }
    }
}
