using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    internal interface ILateExecute : IController
    {
        void LateExecute(float deltatime);
    }
}
