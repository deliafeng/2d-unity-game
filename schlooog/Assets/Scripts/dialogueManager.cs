using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueCanvas;

    public Animator animator;
    private bool isActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!isActivated)
        {
            animator.SetBool("isActivated", true);
            nameText.text = dialogue.name;
            isActivated = true;
            

            sentences.Clear();
            dialogueCanvas.SetActive(true);
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        FindObjectOfType<playerController>().inDialogue = true;
        if (isActivated)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isActivated", false);
        isActivated = false;
        FindObjectOfType<playerController>().inDialogue = false;
        dialogueCanvas.SetActive(false);
    }

}
