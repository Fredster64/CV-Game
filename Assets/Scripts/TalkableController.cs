using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkableController : MonoBehaviour
{
    public PlayerController player;
    public float talkingRange = 1;
    public GameObject talkPrompt;
    public Color dialogueBoxColor;
    public string characterName;
    public string[] characterLines;

    private bool talkable;

    private void Update()
    {
        talkable = IsWithinTalkingRange() && SpaceInputShouldTriggerConversation();

        talkPrompt.SetActive(talkable);

        if (Input.GetKeyDown("space") && talkable)
        {
            DialogueManager.Instance.StartDialogue(characterName, characterLines, dialogueBoxColor);
        }
    }

    private bool IsWithinTalkingRange()
    {
        var distanceFromPlayer = player.GetPlayerPosition() - gameObject.transform.position;
        return distanceFromPlayer.magnitude <= talkingRange;
    }

    private bool SpaceInputShouldTriggerConversation()
    {
        return !ProgressTracker.Instance.IsDisplayingWinMessage()
            && !DialogueManager.Instance.IsTalking();
    }
}
