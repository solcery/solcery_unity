using System.Collections.Generic;

namespace Solcery
{

    namespace BrickRuntime {

        public static class Value
        {

            delegate int Func(BrickData brick, ref Context ctx);
            private static Dictionary<int, Func> Funcs;
           
            static Value() {
                Funcs = new Dictionary<int, Func> {
                    { 0, Const },
                    { 1, Conditional },
                    { 2, Add },
                    { 3, Sub },
                    { 4, GetCtxVar },
                    { 5, RandRange },
                    { 6, Mul },
                    { 7, Div },
                    { 8, Mod },
                    { 100, GetPlayerAttr },
                    { 101, GetPlayerIndex },
                    { 102, GetCardsAmount },
                    { 103, CurrentPlace },
                    { 105, CasterPlayerIndex },
                };
            }

            public static int Run(BrickData brick, ref Context ctx) {
                return Funcs[brick.Subtype](brick, ref ctx);
            }

            static int Const(BrickData brick, ref Context ctx) {
                return brick.IntField;
            }

            static int Conditional(BrickData brick, ref Context ctx) {
                if (Condition.Run(brick.Slots[0], ref ctx))
                    return Run(brick.Slots[1], ref ctx);
                else 
                    return Run(brick.Slots[2], ref ctx);
            }

            static int Add(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) + Run(brick.Slots[1], ref ctx);
            }

            static int Sub(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) - Run(brick.Slots[1], ref ctx);
            }

            static int Mul(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) * Run(brick.Slots[1], ref ctx);
            }

            static int Div(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) / Run(brick.Slots[1], ref ctx);
            }

            static int Mod(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) % Run(brick.Slots[1], ref ctx);
            }

            static int GetCtxVar(BrickData brick, ref Context ctx) {
                var varName = brick.StringField;
                if (!ctx.vars.ContainsKey(varName))
                    return 0;
                return ctx.vars[brick.StringField];
            }

            static int RandRange(BrickData brick, ref Context ctx) {
                var from = Run(brick.Slots[0], ref ctx);
                var to = Run(brick.Slots[1], ref ctx);
                return ctx.boardData.Random.Range(from, to);
            }

            static int GetPlayerAttr(BrickData brick, ref Context ctx) {
                var attrIndex = brick.IntField;
                var playerId = Value.Run(brick.Slots[0], ref ctx);
                var playerData = ctx.boardData.Players[playerId - 1]; // TODO byId
                if (attrIndex == 0)
                    return playerData.IsActive ? 1 : 0;
                else if (attrIndex == 1)
                    return playerData.HP;
                else if (attrIndex == 2)
                    return playerData.Coins;
                else
                    return playerData.Attrs[attrIndex - 3];
            }

            static int GetPlayerIndex(BrickData brick, ref Context ctx) { // DEPRECATED
                return 0;
            }

            static int GetCardsAmount(BrickData brick, ref Context ctx) {
                int result = 0;
                var place = (CardPlace)Value.Run(brick.Slots[0], ref ctx);
                foreach (var card in ctx.boardData.Cards) {
                    if (card.CardPlace == place)
                        result++;
                }
                return result;
            }

            static int CurrentPlace(BrickData brick, ref Context ctx) {
                return (int)ctx.obj.CardPlace;
            }

            static int CasterPlayerIndex(BrickData brick, ref Context ctx) {
                return ctx.casterId;
            }

        }

    }
}
