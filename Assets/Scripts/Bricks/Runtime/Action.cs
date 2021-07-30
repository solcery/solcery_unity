using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Solcery
{

    namespace BrickRuntime
    {

        public static class Action
        {

            delegate void Func(BrickData brick, ref Context ctx);
            private static Dictionary<int, Func> Funcs;

            static Action()
            {
                Funcs = new Dictionary<int, Func> {
                    { 0, Void },
                    { 1, Set },
                    { 2, Conditional },
                    { 3, Loop },
                    { 4, Card },
                    { 5, ShowMessage },
                    { 6, SetCtxVar },
                    { 100, MoveTo },
                    { 101, SetPlayerAttr },
                    { 102, AddPlayerAttr },
                    { 103, ApplyToPlace },
                    { 104, SubPlayerAttr }
                };
            }

            public static void Run(BrickData brick, ref Context ctx)
            {
                Funcs[brick.Subtype](brick, ref ctx);
            }

            static void Void(BrickData brick, ref Context ctx) { }

            static void Set(BrickData brick, ref Context ctx)
            {
                foreach (BrickData slot in brick.Slots)
                {
                    Action.Run(slot, ref ctx);
                }
            }

            static void Conditional(BrickData brick, ref Context ctx)
            {
                if (Condition.Run(brick.Slots[0], ref ctx))
                    Action.Run(brick.Slots[1], ref ctx);
                else
                    Action.Run(brick.Slots[2], ref ctx);
            }

            static void Loop(BrickData brick, ref Context ctx)
            {
                for (int i = 0; i < Value.Run(brick.Slots[0], ref ctx); i++)
                {
                    Action.Run(brick.Slots[1], ref ctx);
                }
            }

            static void Card(BrickData brick, ref Context ctx)
            {
                int cardId = brick.IntField;
                var cardData = ctx.boardData.GetCard(cardId);
                var cardTypeData = ctx.boardData.GetCardTypeById(cardData.CardType);
                var brickTree = cardTypeData.BrickTree;
                Action.Run(brickTree.Genesis, ref ctx);
            }

            static void ShowMessage(BrickData brick, ref Context ctx)
            {
                //Debug.Log("Ingame message: " + brick.StringField);
            }

            static void SetCtxVar(BrickData brick, ref Context ctx)
            {
                ctx.vars[brick.StringField] = Value.Run(brick.Slots[0], ref ctx);
            }

            public static void MoveTo(BrickData brick, ref Context ctx)
            {
                var place = (CardPlace)Value.Run(brick.Slots[0], ref ctx);
                ctx.obj.CardPlace = place; // TODO: remove enum
            }

            static void SetPlayerAttr(BrickData brick, ref Context ctx)
            {
                var attrIndex = brick.IntField;
                var playerId = Value.Run(brick.Slots[0], ref ctx);
                var value = Value.Run(brick.Slots[1], ref ctx);
                var playerData = ctx.boardData.Players[playerId - 1]; // TODO byId
                if (attrIndex == 0)
                    playerData.IsActive = value > 0;
                else if (attrIndex == 1)
                    playerData.HP = value;
                else if (attrIndex == 2)
                    playerData.Coins = value;
                else
                    playerData.Attrs[attrIndex - 3] = value;
            }

            static void AddPlayerAttr(BrickData brick, ref Context ctx)
            {
                var attrIndex = brick.IntField;
                var playerId = Value.Run(brick.Slots[0], ref ctx);
                var value = Value.Run(brick.Slots[1], ref ctx);
                var playerData = ctx.boardData.Players[playerId - 1]; // TODO byId
                if (attrIndex == 0)
                    playerData.IsActive = value > 0;
                else if (attrIndex == 1)
                    playerData.HP += value;
                else if (attrIndex == 2)
                    playerData.Coins += value;
                else
                    playerData.Attrs[attrIndex - 3] += value;
            }

            static void SubPlayerAttr(BrickData brick, ref Context ctx)
            {
                var attrIndex = brick.IntField;
                var playerId = Value.Run(brick.Slots[0], ref ctx);
                var value = Value.Run(brick.Slots[1], ref ctx);
                var playerData = ctx.boardData.Players[playerId - 1]; // TODO byId

                if (attrIndex == 0)
                    playerData.IsActive = value < 0;
                else if (attrIndex == 1)
                    playerData.HP -= value;
                else if (attrIndex == 2)
                    playerData.Coins -= value;
                else
                    playerData.Attrs[attrIndex - 3] -= value;
            }

            static void ApplyToPlace(BrickData brick, ref Context ctx)
            {
                var place = (CardPlace)Value.Run(brick.Slots[0], ref ctx);
                var cardsList = new List<BoardCardData>();
                foreach (var card in ctx.boardData.Cards)
                {
                    if (card.CardPlace == place)
                        cardsList.Add(card);
                }
                var cards = cardsList.ToArray();
                var limit = Value.Run(brick.Slots[2], ref ctx);
                if (limit == 0)
                    limit = cards.Length;
                limit = Mathf.Min(limit, cards.Length);
                ctx.boardData.Random.Shuffle(ref cards);
                var oldObj = ctx.obj;
                for (int i = 0; i < limit; i++)
                {
                    ctx.obj = cards[i];
                    Action.Run(brick.Slots[1], ref ctx);
                }
                ctx.obj = oldObj;
            }

        }

    }


}
