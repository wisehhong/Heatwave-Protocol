using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    [SerializeField] private AudioClip ambientWater; // Reference to the ambient water sound
    private AudioSource audioSource; //Reference to AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //Get the AudioSource component attached to this GameObject.


        audioSource.clip = ambientWater; //Set the audio clip to the ambient water sound
        audioSource.Play(); //Play the audio clip
    }

    // Update is called once per frame
}
