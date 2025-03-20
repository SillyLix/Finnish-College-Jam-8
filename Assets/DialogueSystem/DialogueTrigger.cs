using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSystem.DialogueLine[] dialogueLines;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueSystem dialogueSystem = FindFirstObjectByType<DialogueSystem>();
            if (dialogueSystem != null)
            {
                dialogueSystem.StartDialogue(dialogueLines);
            }
            else
            {
                Debug.LogError("DialogueSystem not found in the scene!");
            }
        }
    }
}