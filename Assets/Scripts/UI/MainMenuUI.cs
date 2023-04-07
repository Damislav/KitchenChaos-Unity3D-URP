using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] private Button singleplayerButton;
    [SerializeField] private Button multiplayerButton;
    [SerializeField] private Button quitButton;




    private void Awake()
    {
        singleplayerButton.onClick.AddListener(() =>
        {

            KitchenGameMultiplayer.playMultiplayer = false;

            Loader.Load(Loader.Scene.LobbyScene);

        });

        multiplayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.playMultiplayer = true;


            Loader.Load(Loader.Scene.LobbyScene);

        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}