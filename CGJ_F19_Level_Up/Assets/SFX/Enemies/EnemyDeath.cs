using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private AudioClip[] deathSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        EnemyDeathSound();
    }

    private AudioClip GetRandomSound()
    {
        return deathSounds[UnityEngine.Random.Range(0, deathSounds.Length)];
    }

    private void EnemyDeathSound()
    {
        AudioClip clip = GetRandomSound();
        audioSource.PlayOneShot(clip);
    }
}
