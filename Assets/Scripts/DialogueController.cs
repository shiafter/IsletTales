using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance {  get; private set; }
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image npcImage;
    public Transform choicePanel;
    public GameObject choiceButton;
    public bool isDialogueActive { get; private set; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowDialogueUI(bool show)
    {
        dialoguePanel.SetActive(show);
        isDialogueActive = show;
    }
    public void SetNPCInfo(string name, Sprite image)
    {
        nameText.text = name;
        npcImage.sprite = image;
    }
    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
    public void ClearChoices()
    {
        foreach (Transform child in choicePanel)
            Destroy(child.gameObject);
    }
    public void CreatChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick)
    {
        GameObject button = Instantiate(choiceButton, choicePanel);
        button.GetComponentInChildren<TMP_Text>().text = choiceText;
        button.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
