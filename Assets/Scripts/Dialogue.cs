using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    public TextMeshProUGUI textComponent;
    public List<Npc> queueToPlay;

    //добавить объект класса NPC чтобы ссылаться на него
    //inDialog перенести в NPC

    [TextAreaAttribute]
    public string[] lines;

    public float textSpeed;

    private int index;
    public int tempIndex;//////////////////////////////////// проверить на правильность

    public bool inDialogue;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {

        StartDialogue();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {////////////////////////////////////////remove in future 
                StopAllCoroutines();////////////////////////////////////////remove in future 
                textComponent.text = lines[index];////////////////////////////////////////remove in future 
            }   ////////////////////////////////////////remove in future       

        }

    }

    private void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = tempIndex;
        inDialogue = true;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        //type each character 1 by 1 
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {           
            tempIndex = 0;
            queueToPlay.RemoveAt(0);

            if(queueToPlay.Count != 0)
            {
                SetDialogue(this, queueToPlay[0]);
                StopAllCoroutines();
                StartDialogue();
                return;
            }

            inDialogue = false;
            gameObject.SetActive(false);
        }
    }

    public void InterruptDialogue(Npc npc)
    {
        if (index != 0 && index != lines.Length - 1)
        {
            npc.trustLevel--;
            npc.tempIndex = index;
            npc.isRememberLine = true;
        }

        inDialogue = false;
        gameObject.SetActive(false);
    }

    public void SetDialogue(Dialogue dialogueWindow, Npc npcParameters)
    {
        DialogueData npcDataOfDialogues;

        npcDataOfDialogues = npcParameters.GetComponent<Npc>().listOfDialogues[0];// 0 replace with correct index in future

        dialogueWindow.GetComponent<Dialogue>().lines = new string[npcDataOfDialogues.dialogueLines.Length];

        for (int i = 0; i < npcDataOfDialogues.dialogueLines.Length; i++)
        {
            dialogueWindow.GetComponent<Dialogue>().lines[i] = npcDataOfDialogues.dialogueLines[i];
        }

        if (npcParameters.GetComponent<Npc>().tempIndex != 0)
        {
            dialogueWindow.GetComponent<Dialogue>().tempIndex = npcParameters.GetComponent<Npc>().tempIndex;
        }


        npcParameters.GetComponent<Npc>().dialogueBubble.SetActive(true);
        npcParameters.GetComponent<Npc>().dialogueInteractionIcon.SetActive(false);
    }


    private void ProceedDialogue()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {////////////////////////////////////////remove in future 
                StopAllCoroutines();////////////////////////////////////////remove in future 
                textComponent.text = lines[index];////////////////////////////////////////remove in future 
            }   ////////////////////////////////////////remove in future       

        }
    }

}
