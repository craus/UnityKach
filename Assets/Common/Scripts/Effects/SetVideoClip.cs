using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.Video;

namespace Common
{
    public class SetVideoClip : Effect
    {
        [SerializeField] private VideoPlayer player;
        [SerializeField] private VideoClipProvider provider;

        public override void Run()
        {
            player.clip = provider.Value;
        }
    }
}
