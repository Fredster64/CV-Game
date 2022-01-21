using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public AudioSource textScrollSoundEffect;
    public PlayerController player;
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueBody;
    public float dialogueSpeed;

    private string[] currentLines;
    private bool isTalking;
    private string currentLine {get { return currentLines[currentLineIndex]; }}
    private int currentLineIndex;
    private bool hasJustPressedSpace;
    private bool readyToMoveToNextLine = false;
    private int framesSinceLastCharacterAppended = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        dialogueBox.SetActive(false);
        isTalking = false;
        hasJustPressedSpace = false;
    }

    private void LateUpdate()
    {
        if (isTalking && !hasJustPressedSpace)
        {
            if (dialogueBody.text.Length < currentLine.Length)
            {
                ScrollText();
            }
            else
            {
                StopTextScroll();
            }

            if (Input.GetKeyDown("space") && !hasJustPressedSpace)
            {
                if (readyToMoveToNextLine)
                {
                    MoveToNextLine();
                }
                else
                {
                    StopTextScroll();
                }

                hasJustPressedSpace = true;
                return;
            }
        }

        hasJustPressedSpace = false;
    }

    public void StartDialogue(string name, string[] lines, Color dialogueBoxColor)
    {
        player.TogglePlayerMovement(false);

        dialogueName.text = name;
        dialogueBody.text = "";
        dialogueBox.GetComponent<Image>().color = dialogueBoxColor;        
        dialogueBox.SetActive(true);
        textScrollSoundEffect.Play();

        isTalking = true;
        hasJustPressedSpace = true;
        currentLines = lines;
        currentLineIndex = 0;
        framesSinceLastCharacterAppended = 0;
    }

    private void ScrollText()
    {
        if (framesSinceLastCharacterAppended > 1 / dialogueSpeed)
        {
            dialogueBody.text += currentLine[dialogueBody.text.Length];
            readyToMoveToNextLine = false;
        }

        framesSinceLastCharacterAppended += 1;
    }

    private void StopTextScroll()
    {
        dialogueBody.text = currentLine;
        readyToMoveToNextLine = true;
        textScrollSoundEffect.Stop();
    }

    private void MoveToNextLine()
    {
        if (currentLineIndex >= currentLines.Length - 1)
        {
            EndDialogue();
        }
        else
        {
            currentLineIndex += 1;
            dialogueBody.text = "";
            framesSinceLastCharacterAppended = 0;
            readyToMoveToNextLine = false;
            textScrollSoundEffect.Play();
        }
    }

    private void EndDialogue()
    {
        currentLineIndex = 0;
        currentLines = new string[0];
        isTalking = false;
        framesSinceLastCharacterAppended = 0;

        dialogueBox.SetActive(false);
        player.TogglePlayerMovement(true);

        ProgressTracker.Instance.RegisterNPC(dialogueName.text);
    }

    public bool IsTalking()
    {
        return isTalking;
    }
}
