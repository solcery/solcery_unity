using System.Collections.Generic;

namespace Solcery
{

    namespace BrickRuntime {

        public static class Condition
        {

            delegate bool Func(BrickData brick, ref Context ctx);
            private static Dictionary<int, Func> Funcs;
            
            static Condition() {
                Funcs = new Dictionary<int, Func> {
                    { 0, True },
                    { 1, False },
                    { 2, Or },
                    { 3, And },
                    { 4, Not },
                    { 5, Equal },
                    { 6, GreaterThan },
                    { 7, LesserThan },
                    { 100, IsAtPlace }
                };
            }

            public static bool Run(BrickData brick, ref Context ctx) {
                return Funcs[brick.Subtype](brick, ref ctx);
            }

            static bool True(BrickData brick, ref Context ctx) {
                return true;
            }

            static bool False(BrickData brick, ref Context ctx) {
                return false;
            }

            static bool Or(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) || Run(brick.Slots[1], ref ctx);
            }

            static bool And(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) && Run(brick.Slots[1], ref ctx);
            }

            static bool Not(BrickData brick, ref Context ctx) {
                return !Run(brick.Slots[0], ref ctx);
            }

            public static bool Equal(BrickData brick, ref Context ctx) {
                return Value.Run(brick.Slots[0], ref ctx) == Value.Run(brick.Slots[1], ref ctx);
            }

            public static bool GreaterThan(BrickData brick, ref Context ctx) {
                return Value.Run(brick.Slots[0], ref ctx) > Value.Run(brick.Slots[1], ref ctx);
            }

            public static bool LesserThan(BrickData brick, ref Context ctx) {
                return Value.Run(brick.Slots[0], ref ctx) < Value.Run(brick.Slots[1], ref ctx);
            }

            public static bool IsAtPlace(BrickData brick, ref Context ctx) {
                var place = Value.Run(brick.Slots[0], ref ctx);
                return (int)ctx.obj.CardPlace == place;
            }

        }

    }

}
