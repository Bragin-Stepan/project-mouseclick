using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioDatabase
{
    private Dictionary<string, AudioClipData> _clipCollection;

    public AudioDatabase(List<AudioClipData> gameMusic, List<AudioClipData> sfx) 
    {  
        _clipCollection = new Dictionary<string, AudioClipData>();
        
        AddToCollection(gameMusic);
        AddToCollection(sfx);
    }
    
    public AudioClipData Get(string name) => _clipCollection.TryGetValue(name, out AudioClipData data) ? data : null;
    
    private void AddToCollection(List<AudioClipData> listToAdd)
    {
        foreach (AudioClipData data in listToAdd)
            if (data != null && _clipCollection.ContainsKey(data.AudioName) == false)
                _clipCollection.Add(data.AudioName, data);
    }
}

[Serializable]
public class AudioClipData
{
    public string AudioName;
    public List<AudioClip> Clips = new();
    [Range(0f, 1f)] public float Volume = 0.5f;

    public AudioClip GetRandomClip()
    {
        if (Clips == null || Clips.Count == 0)
            return null;

        return Clips[UnityEngine.Random.Range(0, Clips.Count)];
    }
}
