using UnityEngine;

namespace SunnyLand
{
    public interface ICollectable
    {
        ObjectType Type { get; }
        float AmountAdd { get; }
    }
}