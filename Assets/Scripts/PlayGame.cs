using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


    public class PlayGame: MonoBehaviour
    {
        public Button button;

        private void Start ()
        {
            button.onClick.AddListener(GameStart);
        }

        private void onDestroy()
        {
            button.onClick.RemoveListener (GameStart);
        }

        public void GameStart()
        {
            SceneManager.LoadSceneAsync(2);
        }

    }
