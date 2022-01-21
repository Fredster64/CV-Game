using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public AudioSource playButtonOnClickSoundEffect;
    public SceneTransitionController sceneTransitionController;

    private bool hasBeenClicked = false;

    private void Update()
    {
        if (Input.GetKeyDown("space") && !hasBeenClicked)
        {
            OnPlayButtonClick();
        }
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(PlayButtonClickCoroutine());
    }

    private IEnumerator PlayButtonClickCoroutine()
    {
        hasBeenClicked = true;
        playButtonOnClickSoundEffect.Play();

        yield return new WaitForSeconds(1);

        sceneTransitionController.LoadSceneWithTransition(1);
    }
}
