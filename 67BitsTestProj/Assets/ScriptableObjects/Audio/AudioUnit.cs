using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsData", menuName = "ScriptableObjects/AudioClipsData", order = 1)]
public class AudioClipsData : ScriptableObject
{
    public List<AudioClip> audioClips;
}
