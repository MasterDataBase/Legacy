using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.LSS;

public class SceneLoader : MonoBehaviour
{
    public LoadingScreenManager lsm;

    private void Start()
    {
        lsm = FindObjectOfType<LoadingScreenManager>();
    }

    public void EndLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        //SceneManager.LoadScene(nextSceneIndex);
        lsm.LoadScene(SceneUtility.GetScenePathByBuildIndex(nextSceneIndex));
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) { nextSceneIndex = 0; }
        //SceneManager.LoadScene(nextSceneIndex);
        lsm.LoadScene(SceneUtility.GetScenePathByBuildIndex(nextSceneIndex));
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMidLevelForDemo()
    {
        lsm.LoadScene(SceneUtility.GetScenePathByBuildIndex(0));
    }
}
