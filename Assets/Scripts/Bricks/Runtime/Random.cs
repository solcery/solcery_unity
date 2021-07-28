using System;

namespace Solcery
{
    namespace BrickRuntime {
        
        [Serializable]
        public class Random {
            const uint KX = 123456789;
            const uint KY = 362436069;
            const uint KZ = 521288629;
            const uint KW = 88675123;

            public uint x;
            public uint y;
            public uint z;
            public uint w;

            public uint Number() {
                var t = x ^ (x << 11);
                x = y; 
                y = z; 
                z = w;
                w ^= (w >> 19) ^ t ^ (t >> 8);
                return w;
            }

            public void Shuffle<T>(ref T[] array) {
                if (array.Length == 0) {
                    return;
                }
                for (int i = array.Length - 1; i > 0; i--) {
                    var j = Number() % (i + 1);
                    T tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                }
            }

            public int Range(int from, int to) {
                uint d = (uint)(to - from + 1);
                return from + (int)(Number() % d);
            }
        }
    }
}
