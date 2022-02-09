using System;
using UnityEngine;

namespace SunnyLand
{
    public class PCInputVertical: IUserInputProxy
    {
        public event Action<float> AxisChange;
        
        public void GetAxis()
        {
           // Debug.Log("InGetAxis Method");
            AxisChange?.Invoke(Input.GetAxis(AxisManager.VERTICAL));
        }
        
    }
}