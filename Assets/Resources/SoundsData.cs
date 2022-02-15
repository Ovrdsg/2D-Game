using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SunnyLand
{

    [CreateAssetMenu(fileName = "SoundsData", menuName = "Data/SoundsData", order = 51)]
    internal class SoundsData : ScriptableObject
    {
        [Serializable]
        private struct SoundsInfo
        {
            public SoundsType Type;
            public AudioClip audioclip;
        }

        [SerializeField] private List<SoundsInfo> _soundsInfos;


        public AudioClip GetSound(SoundsType Type)
        {
            var soundsInfo = _soundsInfos.FirstOrDefault(info => info.Type == Type);
            if(soundsInfo.audioclip == null)
            {
                throw new InvalidOperationException($"audioclip {Type} type not found");
            }

            return soundsInfo.audioclip;
        }

    }
}

