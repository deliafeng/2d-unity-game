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
    //Tracks the number of dialogue sentences that have appeared
    public int sentenceCount = 0;

    public Interactable interactable;

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
        //Makes player stop moving when dialogue is triggered
        FindObjectOfType<playerController>().inDialogue = true;

        if (isActivated)
        {
            if (sentences.Count == 0 && interactable == null)
            {
                EndDialogue();
                return;
                
            }
            if (sentences.Count == 0 && interactable != null)
            {
                interactable.EndScene();
                EndDialogue();
                return;
            }

            string sentence = null;

            //Either displays sentences or triggers a scene if it is an interactable, or if it is not an interactable, just displays dialogue

            if (interactable != null)
            {
                if (interactable.sceneActivationNumber == sentenceCount)
                {
                    interactable.TriggerScene();
                    sentence = sentences.Dequeue();
                    sentenceCount += 1;

                } else if (interactable.sceneDeactivationNumber + 1 == sentenceCount)
                {
                    
                    interactable.EndScene();
                    sentence = sentences.Dequeue();
                    sentenceCount += 1;
                }
                else
                {
                    sentence = sentences.Dequeue();
                    sentenceCount += 1;
                }
            }
            else
            {
                sentence = sentences.Dequeue();
                sentenceCount += 1;

            }

            Sprite sprite = sprites.Dequeue();
            if (sprite != null)
            {
                canvasSprite.sprite = sprite;
            }
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
        //Set back to null
        sentenceCount = 0;
        interactable = null;

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
