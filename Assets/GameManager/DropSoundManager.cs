using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class DropSoundManager : Singleton<DropSoundManager>
{
    public GameObject dropSoundPrefab; // A drop sound is an object which is temporarily spawned into the game in runtime which makes one sound and then dissappears.

    //public static Dictionary<string, AudioMixerGroup> MixerGroups = new Dictionary<string, AudioMixerGroup>();

    private List<GameObject> instances = new List<GameObject>();
    private List<GameObject> instancesToDestroy = new List<GameObject>();
    [SerializeField] private int soundLimit = 20;

    public void PlayDropSound(AudioClip soundClip, Vector3 position = new Vector3(), float volume = 1, float spatialBlend = 0, float pitch = 1, bool varyPitch = false)
    {
        if (instances.Count >= soundLimit)
            return;

        float pitchVar = Random.Range(-0.08f, 0.08f);
        var o = Instantiate(dropSoundPrefab);
        var a = o.GetComponent<AudioSource>();
        a.transform.position = position;
        a.spatialBlend = spatialBlend;
        a.volume = volume;
        a.pitch = pitch + (varyPitch ? pitchVar : 0);
        a.clip = soundClip;
        if (!a.isPlaying)
            a.Play();
        instances.Add(o);
    }

    private void FixedUpdate()
    {
        foreach (GameObject instance in instances)
        {
            var a = instance.GetComponent<AudioSource>();
            if (!a.isPlaying)
            {
                instancesToDestroy.Add(instance);
            }
        }

        foreach (GameObject instance in instancesToDestroy)
        {
            instances.Remove(instance);
            Destroy(instance);
        }

        instancesToDestroy.Clear();
    }
}
