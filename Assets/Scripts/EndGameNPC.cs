using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameNPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogue;
    private DialogueController dialogueUI;

    private int dialogueIndex;
    private bool isTyping,isDialogueActive;
    private bool spawnPortal;
    private void Start()
    {
        dialogueUI = DialogueController.instance;
    }
    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (dialogue == null) return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerInteract>()?.SetInteract(this);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerInteract>()?.ClearInteract(this);
        }
    }
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialogueUI.SetNPCInfo(dialogue.NPCName, dialogue.NPCImage);
        dialogueUI.ShowDialogueUI(true);

        DisplayCurrentLine();
    }
    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueUI.SetDialogueText(dialogue.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        dialogueUI.ClearChoices();
        if(dialogue.endDialogueLines.Length > dialogueIndex && dialogue.endDialogueLines[dialogueIndex])
        {
            EndDialogue();
            return;
        }
        foreach(DialogueChoice dialogueChoice in dialogue.choices)
        {
            if (dialogueChoice.dialogueIndex == dialogueIndex)
            {
                DisplayChoices(dialogueChoice);
                return;
            }
        }
        if(++dialogueIndex < dialogue.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueUI.SetDialogueText("");

        foreach(char letter in dialogue.dialogueLines[dialogueIndex])
        {
            dialogueUI.SetDialogueText(dialogueUI.dialogueText.text + letter);
            yield return new WaitForSeconds(dialogue.typingSpeed);
        }

        isTyping = false;
        if(dialogue.autoProgessLines.Length > dialogueIndex && dialogue.autoProgessLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogue.autoProgressDelay);
            NextLine();

        }
    }
    void DisplayChoices(DialogueChoice choice)
    {
        for(int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndex[i];
            int choiceIndex = i;
            dialogueUI.CreatChoiceButton(choice.choices[i], ()=>ChooseOption(choiceIndex, nextIndex));
        }
    }
    void ChooseOption(int choiceIndex, int nextIndex)
    {
        dialogueUI.ClearChoices();

        if(dialogueIndex == 0 && choiceIndex == 0)
        {
            if (WinConditionController.instance.CheckWinCondition())
            {
                spawnPortal = true;
                dialogueIndex = nextIndex;
                DisplayCurrentLine();
            }
            else
            {
                dialogueUI.SetDialogueText("You don't have enough artifact, check again");
                StartCoroutine(EndDialogueAfterDelay(1.5f));
            }
            return;
        }
        dialogueIndex = nextIndex;
        DisplayCurrentLine();
    }
    void DisplayCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }
    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueUI.SetDialogueText("");
        dialogueUI.ShowDialogueUI(false);

        if (spawnPortal)
        {
            WinConditionController.instance.TrySpawnPortal();
            spawnPortal = false;
        }
    }
    IEnumerator EndDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndDialogue();
    }
}
