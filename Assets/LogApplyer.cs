using Solcery.Utils;
using Newtonsoft.Json;
using System.Threading;
using Solcery.Utils.Reactives;
using Solcery.Modules.Log;
using Solcery.Modules.Board;
using UnityEngine;

namespace Solcery
{
    public class LogApplyer : Singleton<LogApplyer>
    {
        private CancellationTokenSource _cts;

        public void Init()
        {
            Debug.Log("LogApplyer.Init");

            _cts = new CancellationTokenSource();

            Reactives.Subscribe(Log.Instance.LogData, OnLogUpdate, _cts.Token);
        }

        public void DeInit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private void OnLogUpdate(LogData logData)
        {
            Debug.Log("LogApplyer.OnLogUpdate");
            if (logData == null)
                return;

            var currentBoardData = Board.Instance.BoardData.Value;
            var newBoardData = JsonConvert.DeserializeObject<BoardData>(JsonConvert.SerializeObject(currentBoardData)).Prettify(); //Cloning via JSON

            ApplyLog(newBoardData, logData);
            Board.Instance?.UpdateBoard(newBoardData.Prettify());
        }

        private void ApplyLog(BoardData origin, LogData log)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            //return CastCard(ref origin, logStep.playerId, logStep.cardId);
            var currentStep = origin.Step;
            for (int i = origin.Step; i < log.Steps.Count; i++)
            {
                ApplyLogStep(origin, log.Steps[i]);
            }
            origin.Step = log.Steps.Count;
            // return origin;
        }

        private void ApplyLogStep(BoardData origin, LogStepData logStep)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            CastCard(origin, logStep.playerId, logStep.cardId);
        }

        private void CastCard(BoardData origin, int casterId, int cardId)
        {
            var ctx = new Solcery.BrickRuntime.Context(origin, cardId, casterId);
            var cardData = origin.GetCard(cardId);
            var cardTypeData = origin.GetCardTypeById(cardData.CardType);
            var brickTree = cardTypeData.BrickTree;
            BrickRuntime.Action.Run(brickTree.Genesis, ref ctx);
            origin = ctx.boardData;
        }
    }
}
