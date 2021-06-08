using System.Collections.Generic;
using System.Linq;
using System;

namespace Grimmz
{
    public class BrickTree
    {
        public BrickData Genesis { get; private set; }
        public CardMetadata MetaData;

        public void SetGenesis(BrickData data)
        {
            Genesis = data;
            MetaData = new CardMetadata(true);
        }

        public void SerializeToBytes(ref List<byte> buffer)
        {
            List<byte> tmpBuffer = new List<byte>();
            MetaData.SerializeToBytes(ref tmpBuffer);
            buffer.AddRange(BitConverter.GetBytes(tmpBuffer.Count).ToList<byte>());
            buffer.AddRange(tmpBuffer);
            Genesis.SerializeToBytes(ref buffer);
        }

        public bool IsValid()
        {
            if (Genesis == null)
                return false;
                
            return Genesis.IsValid();
        }
    }
}
