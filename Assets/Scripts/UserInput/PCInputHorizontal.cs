using System;
using UnityEngine;

namespace SunnyLand
{
    internal sealed class PCInputHorizontal : IUserInputProxy
    {
        public event Action<float> AxisChange;
        
        public void GetAxis()
        {
           // Debug.Log("InGetAxis Method");
            AxisChange?.Invoke(Input.GetAxis(AxisManager.HORIZONTAL));
        }
    }
}