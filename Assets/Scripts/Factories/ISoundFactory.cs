using UnityEngine;
using UnityEngine.Audio;

namespace SunnyLand
{
    internal interface ISoundFactory
    {
        AudioClip GetSound(SoundsType type);
    }
}
