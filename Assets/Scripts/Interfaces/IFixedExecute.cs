using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    internal interface IFixedExecute : IController
    {
        void FixedExecute(float deltatime);
    }
}
