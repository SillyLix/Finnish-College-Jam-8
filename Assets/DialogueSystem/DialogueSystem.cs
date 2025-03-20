using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private float typingSpeed = 0.05f;

    private DialogueLine[] dialogueLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isDialogueActive = false;

    [System.Serializable]
    public struct DialogueLine
    {
        public string speakerName;
        public string text;
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Z))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("No dialogue lines provided!");
            return;
        }

        dialogueLines = lines;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        isDialogueActive = true;
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        nameText.text = dialogueLines[currentLineIndex].speakerName;
        typingCoroutine = StartCoroutine(TypeSentence(dialogueLines[currentLineIndex].text));
        currentLineIndex++;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }
}