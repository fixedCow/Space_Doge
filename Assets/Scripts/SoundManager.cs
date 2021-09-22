using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;

    public List<AudioSource> ad = new List<AudioSource>();
    public List<AudioClip> blacklist = new List<AudioClip>();

    private void Awake()
    {
        if (inst == null)
            inst = this;
    }
    public void PlaySound(AudioClip adClip)
    {
        if (!CanPlaySound(adClip))
            return;

        AddSoundToList(adClip);
        AudioSource ad = GetAudioSource();
        ad.clip = adClip;
        ad.Play();
    }
    private AudioSource GetAudioSource()
    {
        AudioSource current = null;
        for (int i = 0; i < ad.Count; i++)
        {
            if (!ad[i].isPlaying)
            {
                current = ad[i];
                break;
            }
        }
        if (current == null)
        {
            current = gameObject.AddComponent<AudioSource>() as AudioSource;
            current.volume = 0.7f;
            ad.Add(current);
        }
        return current;
    }
    private bool CanPlaySound(AudioClip adClip)
    {
        List<AudioClip> temp = blacklist;       // 깊은 복사를 해야 함. 굳이 그럴 필요까지는 없나? 일단 염두에 두는 걸로.
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == adClip)
                return false;
        }
        return true;
    }
    private void AddSoundToList(AudioClip adClip)
    {
        StartCoroutine("AddSoundToListCo", adClip);
    }
    private IEnumerator AddSoundToListCo(AudioClip adClip)
    {
        blacklist.Add(adClip);
        yield return new WaitForSeconds(0.03f);
        blacklist.Remove(adClip);
    }
}
