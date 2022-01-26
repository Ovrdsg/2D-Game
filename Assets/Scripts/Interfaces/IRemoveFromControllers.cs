using System;

namespace SunnyLand
{
    internal interface IRemoveFromControllers : IController
    {
        event Action<IController> PlayerRemoved;
    }
}
