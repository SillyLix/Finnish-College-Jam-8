using UnityEngine;
using System.Collections;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] cringeSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public readonly float timeToWaitMin = 2f;
    [SerializeField] public readonly float timeToWaitMax = 15f;
    float waitTime;
    int randomIndex;

    void Start()
    {
        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        while (true)
        {
            waitTime = Random.Range(timeToWaitMin, timeToWaitMax);
            waitTime += cringeSounds[randomIndex].length;
            yield return new WaitForSeconds(waitTime);
            randomIndex = Random.Range(0, cringeSounds.Length);
            audioSource.PlayOneShot(cringeSounds[randomIndex]);
        }
    }
}
