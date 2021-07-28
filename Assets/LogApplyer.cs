using Solcery.Utils;
using System;
using UnityEngine;
using Newtonsoft.Json;

namespace Solcery
{

    public class LogApplyer : Singleton<LogApplyer>
    {

        public void ApplyLog(ref BoardData origin, LogData log)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            //return CastCard(ref origin, logStep.playerId, logStep.cardId);
            var currentStep = origin.Step;
            for (int i = origin.Step; i < log.Steps.Count; i++)
            {
                ApplyLogStep(ref origin, log.Steps[i]);
            }
            origin.Step = log.Steps.Count;
            // return origin;
        }

        public void ApplyLogStep(ref BoardData origin, LogStepData logStep)
        {
            // TODO: Proper action type parsing. Casting is just a particular type
            CastCard(ref origin, logStep.playerId, logStep.cardId);
        }

        public void CastCard(ref BoardData origin, int casterId, int cardId)
        {
            var ctx = new BrickRuntime.Context(ref origin, cardId, casterId);
            var cardData = origin.GetCard(cardId);
            var cardTypeData = origin.GetCardTypeById(cardData.CardType);
            var brickTree = cardTypeData.BrickTree;
            BrickRuntime.Action.Run(brickTree.Genesis, ref ctx);
            origin = ctx.boardData;
        }
    }
}
