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
                };
            }

            static int Const(BrickData brick, ref Context ctx) {
                return brick.IntField;
            }

            public static int Run(BrickData brick, ref Context ctx) {
                return Funcs[brick.Subtype](brick, ref ctx);
            }


        }

    }

            // 0u32 => Ok(Box::new(Const::deserialize(buf)?)),
            // 1u32 => Ok(Box::new(Conditional::deserialize(buf)?)),
            // 2u32 => Ok(Box::new(Add::deserialize(buf)?)),
            // 3u32 => Ok(Box::new(Sub::deserialize(buf)?)),
            // 4u32 => Ok(Box::new(GetCtxVar::deserialize(buf)?)),
            // 5u32 => Ok(Box::new(RandRange::deserialize(buf)?)),
            // 6u32 => Ok(Box::new(Mul::deserialize(buf)?)),
            // 7u32 => Ok(Box::new(Div::deserialize(buf)?)),
            // 8u32 => Ok(Box::new(Mod::deserialize(buf)?)),

            // 100u32 => Ok(Box::new(GetPlayerAttr::deserialize(buf)?)),
            // 101u32 => Ok(Box::new(GetPlayerIndex::deserialize(buf)?)),
            // 102u32 => Ok(Box::new(GetCardsAmount::deserialize(buf)?)),
            // 103u32 => Ok(Box::new(CurrentPlace::deserialize(buf)?)),
            // 105u32 => Ok(Box::new(CasterPlayerIndex::deserialize(buf)?)),
            // _ => Ok(Box::new(Const { value: 0 })), 
}
