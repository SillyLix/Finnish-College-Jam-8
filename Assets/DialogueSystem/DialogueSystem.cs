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
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Z))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        dialogueLines = lines;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
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
    }
}