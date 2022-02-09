using System;

namespace SunnyLand
{
    public interface IUserInputProxy
    {
        event Action<float> AxisChange;
        void GetAxis();
    }
}