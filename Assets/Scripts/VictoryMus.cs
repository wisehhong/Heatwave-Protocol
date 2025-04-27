using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryMus : MonoBehaviour
{

    [SerializeField] private AudioClip victoryMusic;

    private AudioSource audioSource; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = victoryMusic;
        audioSource.Play();
    }

}
