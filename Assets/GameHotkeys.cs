using Sirenix.OdinInspector;
using Solcery.Utils;
using UnityEngine;

namespace Solcery
{
    public class GameHotkeys : UpdateableSingleton<GameHotkeys>
    {
        [BoxGroup("GameContent")] [InfoBox("Q", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameContent1;
        [BoxGroup("GameContent")] [InfoBox("W", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameContent2;
        [BoxGroup("GameContent")] [InfoBox("E", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameContent3;
        [BoxGroup("GameContent")] [InfoBox("R", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameContent4;

        [BoxGroup("GameDisplay")] [InfoBox("A", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameDisplay1;
        [BoxGroup("GameDisplay")] [InfoBox("S", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameDisplay2;
        [BoxGroup("GameDisplay")] [InfoBox("D", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameDisplay3;
        [BoxGroup("GameDisplay")] [InfoBox("F", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameDisplay4;

        [BoxGroup("GameState")] [InfoBox("Z", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState1;
        [BoxGroup("GameState")] [InfoBox("X", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState2;
        [BoxGroup("GameState")] [InfoBox("C", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState3;
        [BoxGroup("GameState")] [InfoBox("V", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState4;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.Q))
                Game.Instance?.UpdateGameContent(gameContent1);
            if (Input.GetKeyDown(KeyCode.W))
                Game.Instance?.UpdateGameContent(gameContent2);
            if (Input.GetKeyDown(KeyCode.E))
                Game.Instance?.UpdateGameContent(gameContent3);
            if (Input.GetKeyDown(KeyCode.R))
                Game.Instance?.UpdateGameContent(gameContent4);

            if (Input.GetKeyDown(KeyCode.A))
                Game.Instance?.UpdateGameDisplay(gameDisplay1);
            if (Input.GetKeyDown(KeyCode.S))
                Game.Instance?.UpdateGameDisplay(gameDisplay2);
            if (Input.GetKeyDown(KeyCode.D))
                Game.Instance?.UpdateGameDisplay(gameDisplay3);
            if (Input.GetKeyDown(KeyCode.F))
                Game.Instance?.UpdateGameDisplay(gameDisplay4);

            if (Input.GetKeyDown(KeyCode.Z))
                Game.Instance?.UpdateGameState(gameState1);
            if (Input.GetKeyDown(KeyCode.X))
                Game.Instance?.UpdateGameState(gameState2);
            if (Input.GetKeyDown(KeyCode.C))
                Game.Instance?.UpdateGameState(gameState3);
            if (Input.GetKeyDown(KeyCode.V))
                Game.Instance?.UpdateGameState(gameState4);
#endif
        }
    }
}
