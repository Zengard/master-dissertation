using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ThangChibaGPT;
using TMPro;

public class Npc : MonoBehaviour
{
    [Header("Required parametes")]
    public float radius = 3f;
    private float distance;
    public GameObject dialogueInteractionIcon;
    public GameObject dialogueBubble;
    public GameObject voiceSpeech;
    public TextMeshProUGUI interestingWordPopUp;
    public string npcName;
    public NavMeshAgent agent;

    private Dialogue dialogue;
    [Space]
    [Header("Active dialogue data")]
    [TextArea(8, 8)]
    public string dialogueText;

    [Space]
    [Header("Personality traits")]
    [Range(0, 1)] public float opennesToExperience;
    [Range(0, 1)] public float conscientiousness;
    [Range(0, 1)] public float extraversion;
    [Range(0, 1)] public float agreeableness;
    [Range(0, 1)] public float neuroticism;
    public float choosenTrait;

    [Space]
    [Header("Trust level")]
    public int trustLevel;
    public bool isRememberLine;
    public float timeToRemember = 10f;
    private float _timeToRemember = 10; // default remember value
    public int tempIndex;//remembered index for a short period 

    [Space]
    [Header("Other NPC's dialogue themes")]
    public bool isHearOtherNpc = false;
    public bool isFoundInterestingWord = false;
    public bool goToOtherNPC;
    public bool hasComment;
    public float distanceToNpc;
    [SerializeField] private Vector3 npcToFollowPosition;
    [SerializeField] private Npc npcToFollow;
    public string reactionToTheme;

    [Space]
    [Header ("Current character's dialogue datas")]
    public List<DialogueData> listOfDialogues;
    public int triggeredDialogue = -1;

    [Space]
    [Space]
    public Material dialogueColor;
    //добавить буловую переменную inDialogue;


    private void Start()
    {
        _timeToRemember = timeToRemember;
    }

    private void Update()
    {
        if (isFoundInterestingWord == true)
        {
            ShowPopUp();
        }
       //else if (isFoundInterestingWord == false)
       // {
       //     RemovePopUp();
       //     Debug.Log(npcName + ": disable popup");
       // }

        if (isRememberLine == true)
        {
            if (timeToRemember > 0)
            {
                timeToRemember -= Time.deltaTime;
            }
            else if (timeToRemember <= 0)
            {
                tempIndex = 0;
                timeToRemember = _timeToRemember;
                hasComment = false;
                isRememberLine = false;
                isFoundInterestingWord = false;
            }
        }

        if(isHearOtherNpc == true && dialogue != null)
        {
            dialogueText = dialogue.textComponent.text;
        }

        if(goToOtherNPC == true)
        {
            //agent.destination = npcToFollow;
            distance = Vector3.Distance(transform.position, npcToFollowPosition);
            agent.destination = npcToFollowPosition;
            agent.stoppingDistance = npcToFollow.radius;

            if(npcToFollow.radius >= distance)
            {
                goToOtherNPC = false;
                agent.destination = transform.position;
                npcToFollow = null;
                agent.ResetPath();
            }

            if (transform.position == agent.destination)
            {
                goToOtherNPC = false;
                agent.destination = transform.position;
                npcToFollow = null;
                agent.ResetPath();
            }


        }   
        
    }

    /// <summary>
    /// сделать метод в котором ishearOtherNpc = true
    /// и персонаж анализирует кого он услышал и через передаваемый параметер в заголовок
    /// после чего начинает идти в его сторону по навмешу

    /// нужно два цикла - один который проходит по всем темам нпс
    /// второй - вложенный, он сравнивает все теги конкретной темы с активной темой нпс 
    /// если находится совпадение, то запоминается индекс первого цикла
    ///
    /// </summary>
    /// 

    public void FindInterestingWords(NpcHearing npcsVoice)
    {
        if(isFoundInterestingWord == false)
        {
            var found = 0;
            if (dialogue == null)
            {
                dialogue = Dialogue.instance;
            }

            Npc npc = npcsVoice.transform.parent.GetComponent<Npc>();
            DialogueData npcData = npc.listOfDialogues[0];

            for(int x = 0; x < listOfDialogues.Count; x++)
            {
                for (int i = 0; i < listOfDialogues[x].tags.Length; i++)
                {
                    for (int y = 0; y < dialogueText.Length; y++)
                    {
                        found = dialogueText.IndexOf(listOfDialogues[x].tags[i]);
                    }
                    //Debug.Log(found);

                    if (found > 0)
                    {
                        triggeredDialogue = x;
                        //Debug.Log("Found word: " + listOfDialogues[x].tags[i]);
                        isFoundInterestingWord = true;

                        interestingWordPopUp.text = listOfDialogues[x].tags[i];

                        if (gameObject.GetComponent<MoveNpc>() != null)
                        {
                            gameObject.GetComponent<MoveNpc>().enabled = false;
                            agent.enabled = true;
                        }

                        if(gameObject.GetComponent<ResetPos>() != null)
                        {
                            gameObject.GetComponent<ResetPos>().speed = 0;
                        }

                       // npcData = npc.listOfDialogues[x];
                        DefineBehaviour(npcData, npcsVoice);
                        found = 0;                       
                    }
                }
            }
        }
    }

    public void FindsInterestingThemes(NpcHearing npcsVoice)
    {
        string npcTheme;    

        if(dialogue == null)
        {
            dialogue = Dialogue.instance;
        }

        Npc npc = npcsVoice.transform.parent.GetComponent<Npc>();
        DialogueData npcData = npc.listOfDialogues[0];// 0 replace with correct index in future

        /// add for cycle with list of dialogues above existed cycles

        for(int i = 0; i < listOfDialogues[0].tags.Length; i++)
        {
            for(int y = 0; y<npcData.tags.Length; y++)
            {
                if (listOfDialogues[0].tags[i] == npcData.tags[y])
                {
                   choosenTrait = npcData.ChoseTrait(npcData.fixedTrait, choosenTrait, opennesToExperience, 
                       conscientiousness, extraversion, agreeableness, neuroticism);                   
                   
                    if(choosenTrait == 1)
                    {
                        npcToFollowPosition = npcsVoice.transform.parent.position;
                        dialogue.queueToPlay.Add(this);
                        goToOtherNPC = true;
                        npcTheme = npcData.theme;
                        reactionToTheme = "React to this text " + "'" + npcTheme + "'" + " in 10 words";
                        GenerateGPTDialogue(reactionToTheme);
                        return;
                    }
                    else
                    {
                        npcTheme = npcData.theme;
                        reactionToTheme = "Tell you have heard a story about: " + "'" + npcTheme + "'" + " in 10 words";
                        GenerateGPTDialogue(reactionToTheme);
                        hasComment = true;
                        return;
                    }

                }
                
            }
         
        }
       // Debug.Log("no matches");
        goToOtherNPC = false;
    }

    private void DefineBehaviour(DialogueData npcData, NpcHearing npcsVoice)
    {
        string npcTheme;
        choosenTrait = npcData.ChoseTrait(npcData.fixedTrait, choosenTrait, opennesToExperience, conscientiousness, extraversion, agreeableness, neuroticism);

        if (choosenTrait == 1)
        {
            npcToFollowPosition = npcsVoice.transform.parent.position;
            npcToFollow = npcsVoice.transform.parent.GetComponent<Npc>();
            dialogue.queueToPlay.Insert(0, this);
            goToOtherNPC = true;
            npcTheme = npcData.theme;
            reactionToTheme = "React to this text " + "'" + npcTheme + "'" + " in 10 words";
            if (dialogue.useAi == Dialogue.UseAi.HasAI)
            {
                GenerateGPTDialogue(reactionToTheme);
            }
            hasComment = true;
            return;
        }
        else if (choosenTrait < 1 && choosenTrait >0) 
        {
            npcToFollowPosition = npcsVoice.transform.parent.position;
            npcToFollow = npcsVoice.transform.parent.GetComponent<Npc>();
            dialogue.queueToPlay.Add(this);
            goToOtherNPC = true;
            npcTheme = npcData.theme;
            reactionToTheme = "React to this text " + "'" + npcTheme + "'" + " in 10 words";
            if(dialogue.useAi == Dialogue.UseAi.HasAI)
            {
                GenerateGPTDialogue(reactionToTheme);
            }
            hasComment = true;
            return;
        }
        else
        {
            npcTheme = npcData.theme;
            reactionToTheme = "Tell you have heard a story about: " + "'" + npcTheme + "'" + " in 10 words";
            if (dialogue.useAi == Dialogue.UseAi.HasAI)
            {
                GenerateGPTDialogue(reactionToTheme);
            }
            isRememberLine = true;
            hasComment = true;
            return;
        }
    }

    public void GenerateGPTDialogue(string text)
    {
        var context = text.Trim();
        ChatManager.Instance.ChatGPT.Send(context, GetComponent<AITestController>());
    }

    private void ShowPopUp()
    {
        if(interestingWordPopUp.alpha < 1) 
        {
            interestingWordPopUp.alpha += Time.deltaTime;
            Debug.Log(npcName + ": Search word");
        }
    }

    public void RemovePopUp()
    { 
            interestingWordPopUp.alpha = 0;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }

}
