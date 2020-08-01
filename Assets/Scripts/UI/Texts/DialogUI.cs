using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogUI : MonoBehaviour
{
    DialogSystem dialogSystem;
    Text text;
    public GameObject MenssagePanelUI;
    Animator animator;
    private int index;
    private float typpingspeed = 0.03f;
    private void Awake()
    {
        dialogSystem = DialogSystem.instance;
        animator = gameObject.GetComponent<Animator>();
        text = gameObject.GetComponentInChildren<Text>();
    }
    private void Start()
    {
    }
    void Update()
    {

    }
    public void ShowMessage(string[] linestext)
    {
        MenssagePanelUI.SetActive(true);
        text.text = "";
        StartCoroutine(TypeText(linestext));
        animator.SetTrigger("Show");
    }
    public void HideMessage()
    {
        animator.SetTrigger("Hide");
        StartCoroutine(TimetoWait());
    }
    IEnumerator TimetoWait()
    {
        yield return new WaitForSeconds(0.3f);
        MenssagePanelUI.SetActive(false);
    }
    IEnumerator TypeText(string[] linestext)
    {
        for (int i = 0; i < linestext.Length; i++)
        {
            foreach (char letter in linestext[i].ToCharArray())
            {
                text.text += letter;
                yield return new WaitForSeconds(typpingspeed);
            }
            text.text += "\n";
        }
    }
}
