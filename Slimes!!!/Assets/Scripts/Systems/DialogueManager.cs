using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Systems;
using System;

public class DialogueManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    TMP_Text nameText;
    [SerializeField]
    TMP_Text dialogueText;

    [SerializeField]
    Image characterImage;

    [SerializeField]
    Animator animator;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void DoInteraction()
    {
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        FindObjectOfType<PlayerInteractions>().SetInteractor(this);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.CharacterName;

        characterImage.sprite = dialogue.CharacterImage;

        sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
