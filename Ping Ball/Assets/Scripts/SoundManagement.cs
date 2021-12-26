using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    private AudioSource musicSrc;
    private AudioSource sfxSrc;
    public AudioClip ballLaunchingSound;
    public AudioClip ballGettingHitSound;

    private void Awake()
    {
        musicSrc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        sfxSrc = GetComponent<AudioSource>();
    }

    public void PlayLaunchingSound()
    {
        sfxSrc.pitch = GetRandomPitchValue();
        sfxSrc.PlayOneShot(ballLaunchingSound);
    }

    public void PlayGettingHitSound()
    {
        sfxSrc.pitch = GetRandomPitchValue();
        sfxSrc.PlayOneShot(ballGettingHitSound);
    }

    public void SetMusicVolume(float value)
    {
        musicSrc.volume = value;
    }

    public void SetSoundEffectsVolume(float value)
    {
        sfxSrc.volume = value;
    }

    private float GetRandomPitchValue()
    {
        return Random.Range(0.8f, 1.2f);
    }
}
