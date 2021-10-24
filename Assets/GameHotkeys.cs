using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Solcery.React;
using Solcery.Utils;
using UnityEngine;
using DG.Tweening;

namespace Solcery
{
    public class GameHotkeys : UpdateableSingleton<GameHotkeys>
    {
        [SerializeField] private Material burnMaterial = null;

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
        [BoxGroup("GameState")] [InfoBox("B", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState5;
        [BoxGroup("GameState")] [InfoBox("N", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState6;
        [BoxGroup("GameState")] [InfoBox("M", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameState7;

        [BoxGroup("Other")] [InfoBox("O", InfoMessageType.None)] [Multiline(2)] [SerializeField] private string gameOverPopup1;

        public override void PerformUpdate()
        {
#if (UNITY_EDITOR)
            if (Input.GetKeyDown(KeyCode.Q))
                ReactToUnity.Instance?.UpdateGameContent(gameContent1);
            if (Input.GetKeyDown(KeyCode.W))
                ReactToUnity.Instance?.UpdateGameContent(gameContent2);
            if (Input.GetKeyDown(KeyCode.E))
                ReactToUnity.Instance?.UpdateGameContent(gameContent3);
            if (Input.GetKeyDown(KeyCode.R))
                ReactToUnity.Instance?.UpdateGameContent(gameContent4);

            if (Input.GetKeyDown(KeyCode.A))
                ReactToUnity.Instance?.UpdateGameDisplay(gameDisplay1);
            if (Input.GetKeyDown(KeyCode.S))
                ReactToUnity.Instance?.UpdateGameDisplay(gameDisplay2);
            if (Input.GetKeyDown(KeyCode.D))
                ReactToUnity.Instance?.UpdateGameDisplay(gameDisplay3);
            if (Input.GetKeyDown(KeyCode.F))
                ReactToUnity.Instance?.UpdateGameDisplay(gameDisplay4);

            if (Input.GetKeyDown(KeyCode.Z))
                ReactToUnity.Instance?.UpdateGameState(gameState1);
            if (Input.GetKeyDown(KeyCode.X))
                ReactToUnity.Instance?.UpdateGameState(gameState2);
            if (Input.GetKeyDown(KeyCode.C))
                ReactToUnity.Instance?.UpdateGameState(gameState3);
            if (Input.GetKeyDown(KeyCode.V))
                ReactToUnity.Instance?.UpdateGameState(gameState4);
            if (Input.GetKeyDown(KeyCode.B))
                ReactToUnity.Instance?.UpdateGameState(gameState5);
            if (Input.GetKeyDown(KeyCode.N))
                ReactToUnity.Instance?.UpdateGameState(gameState6);
            if (Input.GetKeyDown(KeyCode.M))
                ReactToUnity.Instance?.UpdateGameState(gameState7);

            if (Input.GetKeyDown(KeyCode.O))
                ReactToUnity.Instance?.OpenGameOverPopup(gameOverPopup1);

            if (Input.GetKeyDown(KeyCode.P))
            {
                Burn().Forget();
            }
#endif
        }

        private async UniTaskVoid Burn()
        {
            if (burnMaterial == null)
                return;

            // await burnMaterial.DOFloat(13f, "_FadeBurnGlow", 0.2f);
            await burnMaterial.DOFloat(1f, "_FadeAmount", 1f);
            // burnMaterial.DOFloat(1f, "_FadeBurnGlow", 0.8f);
        }
    }
}
