using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    public Animator sceneTransitionAnimator;
    public float sceneExitTransitionTime = 0.5f;
    
    public void LoadSceneWithTransition(int newSceneBuildIndex)
    {
        StartCoroutine(LoadScene(newSceneBuildIndex));
    }

    public void LoadSceneWithTransition(string newSceneName)
    {
        StartCoroutine(LoadScene(newSceneName));
    }

    private IEnumerator LoadScene(int buildIndex)
    {
        sceneTransitionAnimator.SetTrigger("ExitScene");
        yield return new WaitForSeconds(sceneExitTransitionTime);
        SceneManager.LoadScene(buildIndex);
    }

    private IEnumerator LoadScene(string sceneName)
    {
        sceneTransitionAnimator.SetTrigger("ExitScene");
        yield return new WaitForSeconds(sceneExitTransitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
