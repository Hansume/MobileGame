using UnityEngine;

public class NPCInteract : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject dialogueBox;

    public void Interact()
    {
        dialogueBox.SetActive(true);
    }

    public void ResetInteract()
    {
        dialogueBox.GetComponent<DialogueUI>().CloseDialogue();
    }
}
