using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


    public class NextButton: MonoBehaviour
    {
        public Button button;

        private void Start ()
        {
            button.onClick.AddListener (NextScene);
        }

        private void onDestroy()
        {
            button.onClick.RemoveListener (NextScene);
        }

        public void NextScene()
        {
            SceneManager.LoadSceneAsync(1);
        }

    }
