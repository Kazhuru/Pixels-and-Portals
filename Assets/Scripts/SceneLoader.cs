using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delaySceneChange = 1f;

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        CleanScenePersistenceObjects();
        StartCoroutine(delayAfterSceneChange(nextSceneIndex, delaySceneChange));
    }

    public void RestartScene()
    {
        StartCoroutine(delayAfterSceneChange(SceneManager.GetActiveScene().buildIndex, delaySceneChange));
    }

    public void LoadMainMenuScene()
    {
        StartCoroutine(delayAfterSceneChange("Main Menu Scene", 0f));
    }
    
    public void LoadSuccessScene()
    {
        CleanScenePersistenceObjects();
        StartCoroutine(delayAfterSceneChange("Success Scene", delaySceneChange));
    }

    public void LoadGameoverScene()
    {
        CleanScenePersistenceObjects();
        StartCoroutine(delayAfterSceneChange("GameOver Scene", delaySceneChange));
    }

    public void LoadFirstLevelScene()
    {
        CleanScenePersistenceObjects();
        StartCoroutine(delayAfterSceneChange(1, 0f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator delayAfterSceneChange(int indexScene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(indexScene);
    }

    private IEnumerator delayAfterSceneChange(string nameScene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nameScene);
    }

    private void CleanScenePersistenceObjects()
    {
        var scenePersist = FindObjectOfType<ScenePersist>();
        if (scenePersist != null)
        {
            Destroy(scenePersist.gameObject);
        }
    }
}
