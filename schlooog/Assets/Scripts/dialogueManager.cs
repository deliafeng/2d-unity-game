using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<Sprite> sprites;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueCanvas;
    public Image canvasSprite;

    public Animator animator;
    private bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (!isActivated)
        {
            dialogueCanvas.SetActive(true);
            animator.SetBool("isActivated", true);
            nameText.text = dialogue.name;
            isActivated = true;

            sprites.Clear();
            sentences.Clear();
            
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            foreach (Sprite sprite in dialogue.sprites)
            {
                sprites.Enqueue(sprite);
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
            Sprite sprite = sprites.Dequeue();
            canvasSprite.sprite = sprite;
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
        StopAllCoroutines();
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        dialogueCanvas.SetActive(false);
    }

}
