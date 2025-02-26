using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RouletteMiniGame.SceneMenagement
{
    public class NextSceneLoader : MonoBehaviour
    {
        [SerializeField] Button nextSceneButton;

        private void Start()
        {
            nextSceneButton.onClick.AddListener(NextScene);
        }
        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}