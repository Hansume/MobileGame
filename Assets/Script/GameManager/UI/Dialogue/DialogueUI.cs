using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private float displaySpeed;
    [SerializeField] private DialogueObject initialDialogue;
    [SerializeField] private DialogueObject finalDialogue;

    private void OnEnable()
    {
        if (!GemCount.instance.lastBoss)
        {
            StartCoroutine(StepThroughDiablogue(initialDialogue));
        }
        else
        {
            StartCoroutine(StepThroughDiablogue(finalDialogue));
        }
    }

    private IEnumerator StepThroughDiablogue (DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return StartCoroutine(TypeText(dialogue));
            yield return new WaitForSeconds(1f);
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        }
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    #region TypeEffect
    private IEnumerator TypeText(string text)
    {
        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            t += Time.deltaTime * displaySpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            textLabel.text = text.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = text;
    }
    #endregion

}
