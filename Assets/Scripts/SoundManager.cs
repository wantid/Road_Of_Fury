using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject sfx;

    public AudioClip[] audioClips;
    public List<AudioClip> music = new List<AudioClip>();

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        PlayRandomMusic();
    }
    private void PlayRandomMusic()
    {
        if (!source.isPlaying)
        {
            int i = Random.Range(0, music.Count);

            source.clip = music[i];

            source.Play();
        }
    }
    public void PlaySound(int soundNum)
    {
        GameObject s = Instantiate(sfx, Vector2.zero, Quaternion.identity) as GameObject;
        AudioSource AS = s.GetComponent<AudioSource>();

        AS.clip = audioClips[soundNum];
        AS.Play();
        Destroy(s, audioClips[soundNum].length);
    }
}
