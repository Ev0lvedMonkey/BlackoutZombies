using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    MenuScene =0,
    GameScene =1
}

public static class SceneLoader 
{
    public static void LoadScene(Scenes targetScene) =>
        SceneManager.LoadScene((int)targetScene);
}
