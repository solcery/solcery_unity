using System.Collections.Generic;

namespace Solcery
{

    namespace BrickRuntime {

        public static class Action
        {

            delegate void Func(BrickData brick, ref Context ctx);
            private static Dictionary<int, Func> Funcs;
            
            static Action() {
                Funcs = new Dictionary<int, Func> {
                    { 0, Void },
                    { 1, Set },
                    { 2, Conditional },
                    { 100, MoveTo }
                };
            }

            static void Void(BrickData brick, ref Context ctx) {}

            static void Set(BrickData brick, ref Context ctx) {
                foreach (BrickData slot in brick.Slots) {
                    Action.Run(slot, ref ctx);
                }
            }

            static void Conditional(BrickData brick, ref Context ctx) {
                if (Condition.Run(brick.Slots[0], ref ctx))
                    Action.Run(brick.Slots[1], ref ctx);
                else 
                    Action.Run(brick.Slots[2], ref ctx);
            }

            public static void Run(BrickData brick, ref Context ctx) {
                Funcs[brick.Subtype](brick, ref ctx);
            }

            public static void MoveTo(BrickData brick, ref Context ctx) {
                var placeId = Value.Run(brick.Slots[0], ref ctx);
                ctx.objects[ctx.objects.Count - 1].CardPlace = (CardPlace)placeId; // TODO: remove enum
            }

        }

    }

    //     0u32 => Ok(Box::new(Void::deserialize(buf)?)),
    //     1u32 => Ok(Box::new(Set::deserialize(buf)?)),
    //     2u32 => Ok(Box::new(Conditional::deserialize(buf)?)),
    //     3u32 => Ok(Box::new(Loop::deserialize(buf)?)),
    //     4u32 => Ok(Box::new(Card::deserialize(buf)?)),
    //     5u32 => Ok(Box::new(ShowMessage::deserialize(buf)?)),
    //     6u32 => Ok(Box::new(SetCtxVar::deserialize(buf)?)),
    //     100u32 => Ok(Box::new(MoveTo::deserialize(buf)?)),
    //     101u32 => Ok(Box::new(SetPlayerAttr::deserialize(buf)?)),
    //     102u32 => Ok(Box::new(AddPlayerAttr::deserialize(buf)?)),
    //     103u32 => Ok(Box::new(ApplyToPlace::deserialize(buf)?)),
    //     104u32 => Ok(Box::new(SubPlayerAttr::deserialize(buf)?)),

}
