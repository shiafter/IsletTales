using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu]
public class NPCDialogue : ScriptableObject
{
    public string NPCName;
    public Sprite NPCImage;
    public bool endGameNPC;
    public string[] dialogueLines;
    public bool[] autoProgessLines;//đánh dấu dòng nào tự chạy
    public bool[] endDialogueLines;//đánh dấu dòng nào là kết thúc
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public float autoProgressDelay = 1.5f;
    public DialogueChoice[] choices;
}
[System.Serializable]
public class DialogueChoice
{
    public int dialogueIndex; //lựa chọn xuất hiện ở dòng nào
    public string[] choices; // các lựa chọn
    public int[] nextDialogueIndex; // chọn xong đến câu thoại nào
}