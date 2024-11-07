using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig", order = 1)]
public class AudioUnit : ScriptableObject
{
    public List<SoundClip> soundClips;

    public AudioClip GetClipByName(string clipName)
    {
        SoundClip clip = soundClips.Find(c => c.clipName == clipName);
        return clip?.audioClip;
    }
}

[System.Serializable]
public class SoundClip
{
    public string clipName;
    public AudioClip audioClip;
}
