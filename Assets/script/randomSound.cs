using UnityEngine;
using System.Collections;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] cringeSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private readonly float timeToWaitMin = 0.5f;
    [SerializeField] private readonly float timeToWaitMax = 1.5f;

    void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, cringeSounds.Length);
            audioSource.PlayOneShot(cringeSounds[randomIndex]);
            float waitTime = Random.Range(timeToWaitMin, timeToWaitMax) + cringeSounds[randomIndex].length;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
