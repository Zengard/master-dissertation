using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ThangChibaGPT;

public class Dialogue : MonoBehaviour
{
   public enum UseAi {HasAI,  HasNotAI};
   public UseAi useAi;

    public static Dialogue instance;

    public TextMeshProUGUI npcName;
    public TextMeshProUGUI textComponent;
    [SerializeField] private Npc currentlyActiveNPC;
    public List<Npc> queueToPlay;
    public List<Npc> afterCommentQueue;


    [TextArea(8, 8)]
    public string npcComments;

    //�������� ������ ������ NPC ����� ��������� �� ����
    //inDialog ��������� � NPC

    [TextAreaAttribute]
    public string[] lines;

    public float textSpeed;
    private float _defaultTextSpeed;

    [SerializeField]private int index;
    public int tempIndex;

    public bool inDialogue;


    Image image;


    private void Awake()
    {
        instance = this;
    }

    

    private void OnEnable()
    {

        if(queueToPlay[0].hasComment == true)
        {
            StartComment();
            queueToPlay[0].hasComment = false;

        }
        else
        {
            StartDialogue();
        }
    
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultTextSpeed = textSpeed;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (textComponent.text == npcComments || textComponent.text == lines[index])
            {
                textSpeed = _defaultTextSpeed;
                NextLine();
            }
            else
            {////////////////////////////////////////remove in future 
                //StopAllCoroutines();////////////////////////////////////////remove in future 
                //textComponent.text = lines[index];////////////////////////////////////////remove in future 
                textSpeed /= 2;
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

    private void StartComment()
    {
        if(useAi == UseAi.HasAI)
        {
            npcComments = queueToPlay[0].GetComponent<AITestController>().finalGeneratedPhrase;
        }
        else
        {
            npcComments = queueToPlay[0].listOfDialogues[queueToPlay[0].triggeredDialogue].commentPhrase; //0 replace with correct index in future
        }
        textComponent.text = string.Empty;
        index = tempIndex - 1;
        inDialogue = true;

        StartCoroutine(TypeComment(npcComments));
    }

    private void StartGap()
    {

        if(queueToPlay[0].triggeredDialogue != -1)
        {
            npcComments = queueToPlay[0].listOfDialogues[queueToPlay[0].triggeredDialogue].gapPhrase;
        }
        else
        {
            npcComments = queueToPlay[0].listOfDialogues[0].gapPhrase;
        }

        textComponent.text = string.Empty;
        index = tempIndex - 1;
        inDialogue = true;
        StartCoroutine(TypeComment(npcComments));
    }


    private IEnumerator TypeLine()
    {
        //type each character 1 by 1 
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        foreach (Npc npc in queueToPlay)
        {
            if (npc.hasComment == true)
            {
                if (npc.choosenTrait == 1)
                {
                    npc.hasComment = false;
                    OtherNPCInterruptDialogue(npc, currentlyActiveNPC);
                    break;
                }
               
            }
        }

    }

    private IEnumerator TypeComment(string comment)
    {
        foreach (char c in comment.ToCharArray())
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
            queueToPlay[0].dialogueBubble.SetActive(false);
            queueToPlay[0].voiceSpeech.SetActive(false);
            queueToPlay[0].triggeredDialogue = -1;
            queueToPlay[0].RemovePopUp();
            afterCommentQueue.Add(queueToPlay[0]);
            queueToPlay.RemoveAt(0);

            if(queueToPlay.Count != 0)
            {
                queueToPlay[0].goToOtherNPC = false;

                SetDialogue(this, queueToPlay[0]);
                StopAllCoroutines();

                if (queueToPlay[0].hasComment)
                {
                    StartComment();
                    queueToPlay[0].hasComment = false;
                }
                else if (queueToPlay[0].tempIndex != 0)
                {
                    StartGap();
                }
                else
                {
                    StartDialogue();
                }

                return;
            }
            
                foreach(var character in afterCommentQueue)
            {
                    character.triggeredDialogue = -1;
                    character.isFoundInterestingWord = false;
                    character.timeToRemember = 0;
                    character.isRememberLine = true;
                    character.agent.ResetPath();
                    character.RemovePopUp();
                //character.choosenTrait = 0;

                if (character.GetComponent<MoveNpc>() != null)
                {
                    character.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
                   character.GetComponent<MoveNpc>().enabled = true;                    
                }

                if (character.GetComponent<ResetPos>() != null)
                {
                    character.GetComponent<ResetPos>().speed = character.GetComponent<ResetPos>().defaultSpeed;
                }

            }

                afterCommentQueue.Clear();
            

            inDialogue = false;
            currentlyActiveNPC = null;
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
        npc.voiceSpeech.SetActive(false);

        afterCommentQueue.Clear();

        foreach (var character in queueToPlay)
        {
            character.dialogueBubble.SetActive(false);
            character.isFoundInterestingWord = false;

            if (character.GetComponent<ResetPos>() != null)
            {
                character.GetComponent<ResetPos>().speed = character.GetComponent<ResetPos>().defaultSpeed;
            }
        }
        queueToPlay.Clear();

        gameObject.SetActive(false);
    }

    public void OtherNPCInterruptDialogue(Npc interruping, Npc interruped)
    {
        if (index != 0)
        {
            interruped.tempIndex = index;
            interruped.isRememberLine = true;
        }

        inDialogue = false;

        foreach (var character in queueToPlay)
        {
            character.dialogueBubble.SetActive(false);
            character.voiceSpeech.SetActive(false);
        }

        interruping.goToOtherNPC = false;
        interruping.isHearOtherNpc = false; // maybe delete after tests
        //interruping.isFoundInterestingWord = false;
        SetDialogue(this, interruping);
        StartComment();
    }

    public void SetDialogue(Dialogue dialogueWindow, Npc npcParameters)
    {
        DialogueData npcDataOfDialogues;

        if(npcParameters.GetComponent<Npc>().triggeredDialogue > -1)
        {
            npcDataOfDialogues = npcParameters.GetComponent<Npc>().listOfDialogues[npcParameters.GetComponent<Npc>().triggeredDialogue];
        }
        else
        {
            npcDataOfDialogues = npcParameters.GetComponent<Npc>().listOfDialogues[0];// 0 replace with correct index in future
        }

        dialogueWindow.GetComponent<Dialogue>().lines = new string[npcDataOfDialogues.dialogueLines.Length];

        for (int i = 0; i < npcDataOfDialogues.dialogueLines.Length; i++)
        {
            dialogueWindow.GetComponent<Dialogue>().lines[i] = npcDataOfDialogues.dialogueLines[i];
        }

        if (npcParameters.GetComponent<Npc>().tempIndex != 0)
        {
            dialogueWindow.GetComponent<Dialogue>().tempIndex = npcParameters.GetComponent<Npc>().tempIndex;
        }

        if (npcParameters.gameObject.GetComponent<ResetPos>() != null)
        {
            npcParameters.gameObject.GetComponent<ResetPos>().speed = 0;
        }

        dialogueWindow.npcName.text = npcParameters.GetComponent<Npc>().npcName;
        npcParameters.GetComponent<Npc>().dialogueBubble.SetActive(true);
        npcParameters.voiceSpeech.SetActive(true);
        npcParameters.GetComponent<Npc>().dialogueInteractionIcon.SetActive(false);
        currentlyActiveNPC = npcParameters;

        Color c = npcParameters.dialogueColor.color;
        c.a = 0.5f;
        GetComponent<Image>().color = c;
        //GetComponent<Image>().color = new Color(npcParameters.dialogueColor.color.r, npcParameters.dialogueColor.color.b, npcParameters.dialogueColor.color.b, 180);

    }

}
