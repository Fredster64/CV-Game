using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMessage : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource winFanfareSoundEffect;
    public GameObject spaceToClosePrompt;
    public PlayerController player;

    private bool isCloseable;

    private void Awake()
    {
        isCloseable = false;
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Close();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        StartCoroutine(OpenWinMessageCoroutine());
    }

    public bool IsOpen()
    {
        return gameObject.activeSelf;
    }

    private IEnumerator OpenWinMessageCoroutine()
    {
        player.TogglePlayerMovement(false);
        isCloseable = false;
        backgroundMusic.Stop();
        winFanfareSoundEffect.Play();
        spaceToClosePrompt.SetActive(false);

        yield return new WaitForSeconds(3);

        spaceToClosePrompt.SetActive(true);
        isCloseable = true;
    }
    
    private void Close()
    {
        if (isCloseable)
        {
            backgroundMusic.Play();
            player.TogglePlayerMovement(true);
            gameObject.SetActive(false);
        }
    }
}
