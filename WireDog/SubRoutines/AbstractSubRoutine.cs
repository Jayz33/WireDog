using WireDog.Common;

namespace WireDog.SubRoutines
{
    public abstract class AbstractSubRoutine : AssemblerContainer
    {
        public override string ToString()
        {
            return CaveAddress.ToString();
        }
    }
}
