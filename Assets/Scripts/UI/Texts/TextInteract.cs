using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteract : Interactable
{
    public DialogUI dialogUI;
    public TutorialText tutorialText;
    public override void Interact()
    {
        base.Interact();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
            string[] tutoriallines = tutorialText.lines; 
            dialogUI.ShowMessage(tutoriallines);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
            dialogUI.HideMessage();
        }
    }
}
