using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
public static class Loader
{


    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
        LobbyScene,
        CharacterSelectScene
    }


    private static Scene targetScene;



    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        
        SceneManager.LoadScene(targetScene.ToString());
    }
    public static void LoadNetwork(Scene targetScene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene.ToString(), LoadSceneMode.Single);

    }
    public static void LoaderCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }

}