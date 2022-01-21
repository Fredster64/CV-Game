using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    public static ProgressTracker Instance;
    
    public AudioSource npcRegisteredSoundEffect;
    public WinMessage winMessage;
    public TextMeshProUGUI NPCsTalkedToCounter;
    public TextMeshProUGUI numberOfNPCsText;
    public int numberOfNPCs;

    private int NPCsTalkedToNumber = 0;
    private List<string> namesOfNPCsTalkedTo = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        NPCsTalkedToCounter.text = "0";
        numberOfNPCsText.text = numberOfNPCs.ToString();
    }

    public void RegisterNPC(string NPCName)
    {
        if (!namesOfNPCsTalkedTo.Contains(NPCName))
        {
            namesOfNPCsTalkedTo.Add(NPCName);
            NPCsTalkedToNumber += 1;
            NPCsTalkedToCounter.text = NPCsTalkedToNumber.ToString();

            if (NPCsTalkedToNumber == numberOfNPCs)
            {
                winMessage.Open();
            }
            else
            {
                npcRegisteredSoundEffect.Play();
            }
        }
    }

    public bool IsDisplayingWinMessage()
    {
        return winMessage.gameObject.activeSelf;
    }
}
