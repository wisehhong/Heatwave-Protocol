using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


    public class PlayGame: MonoBehaviour
    {
        [SerializeField] private AudioClip buttonClickSound;
        private AudioSource audioSource;

        public Button button;

        private void Start ()
        {
            button.onClick.AddListener(GameStart);

            audioSource = GetComponent<AudioSource>();

        }

        private void onDestroy()
        {
            button.onClick.RemoveListener (GameStart);
        }

        public void GameStart()
        {
            SceneManager.LoadSceneAsync(2);
            audioSource.clip = buttonClickSound;
            audioSource.Play();

        }

    }
