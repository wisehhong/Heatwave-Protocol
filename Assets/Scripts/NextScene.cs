using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


    public class NextButton: MonoBehaviour
    {
        [SerializeField] private AudioClip buttonClickSound;
        private AudioSource audioSource;

        public Button button;



        private void Start ()
        {
            button.onClick.AddListener (NextScene);

            audioSource = GetComponent<AudioSource>();
        }

        private void onDestroy()
        {
            button.onClick.RemoveListener (NextScene);
        }

        public void NextScene()
        {
            //play sound FX
            audioSource.clip = buttonClickSound;
            audioSource.Play();
            SceneManager.LoadSceneAsync(1);
        }

    }
