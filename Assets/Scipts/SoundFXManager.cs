using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip,Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
    }

    public void PlayRandomSoundFXClip(AudioClip [] audioClip, Transform spawnTransform, float volume)
    {
        int randomIndex = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip[randomIndex];

        audioSource.volume = volume;

        audioSource.Play();
        float clipLength = audioSource.clip.length;
    }
}
