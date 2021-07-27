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
                    { 100, IsAtPlace }
                };
            }

            static bool True(BrickData brick, ref Context ctx) {
                return true;
            }

            static bool False(BrickData brick, ref Context ctx) {
                return false;
            }

            static bool Or(BrickData brick, ref Context ctx) {
                return Run(brick.Slots[0], ref ctx) && Run(brick.Slots[1], ref ctx);
            }

            public static bool Run(BrickData brick, ref Context ctx) {
                if (!Funcs.ContainsKey(brick.Subtype))
                    return false;
                return Funcs[brick.Subtype](brick, ref ctx);
            }

            public static bool IsAtPlace(BrickData brick, ref Context ctx) {
                var placeId = Value.Run(brick.Slots[0], ref ctx);
                return (int)ctx.objects[ctx.objects.Count - 1].CardPlace == placeId;
            }

        }

    }

            // 0u32 => Ok(Box::new(True::deserialize(buf)?)),
            // 1u32 => Ok(Box::new(False::deserialize(buf)?)),
            // 2u32 => Ok(Box::new(Or::deserialize(buf)?)),
            // 3u32 => Ok(Box::new(And::deserialize(buf)?)),
            // 4u32 => Ok(Box::new(Not::deserialize(buf)?)),
            // 5u32 => Ok(Box::new(Equal::deserialize(buf)?)),
            // 6u32 => Ok(Box::new(GreaterThan::deserialize(buf)?)),
            // 7u32 => Ok(Box::new(LesserThan::deserialize(buf)?)),
            // 100u32 => Ok(Box::new(IsAtPlace::deserialize(buf)?)),
            // _ => Ok(Box::new(True::deserialize(buf)?)),

}
