using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    public GameObject dialogueInteractionIcon;
    public GameObject dialogueBubble;
    public GameObject voiceSpeech;
    [SerializeField] private NavMeshAgent agent;

    private Dialogue dialogue;

    [Space]
    [Header("Personality traits")]
    [Range(0, 1)] public float opennesToExperience;
    [Range(0, 1)] public float conscientiousness;
    [Range(0, 1)] public float extraversion;
    [Range(0, 1)] public float agreeableness;
    [Range(0, 1)] public float neuroticism;

    [Space]
    [Header("Trust level")]
    public int trustLevel;
    public bool isRememberLine;
    public float timeToRemember = 10f;
    private float _timeToRemember = 10; // default remember value
    public int tempIndex;//remembered index for a short period 

    [Space]
    [Header("Other NPC's dialogue themes")]
    public bool isHearOtherNpc;
    public float distanceToNpc;
    private Vector3 npcToFollow;
    public string[] listOfOthersNpcThemes;
    public string reactionToTheme;

    [Space]
    public DialogueData[] listOfDialogues;
    //�������� ������� ���������� inDialogue;

    private void Start()
    {
        
    }

    private void Update()
    {
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
                isRememberLine = false;
            }
        }

        if(isHearOtherNpc == true)
        {
            agent.SetDestination(npcToFollow * distanceToNpc);
        }
    }

    /// <summary>
    /// ������� ����� � ������� ishearOtherNpc = true
    /// � �������� ����������� ���� �� ������� � ����� ������������ ��������� � ���������
    /// ����� ���� �������� ���� � ��� ������� �� �������

    /// ����� ��� ����� - ���� ������� �������� �� ���� ����� ���
    /// ������ - ���������, �� ���������� ��� ���� ���������� ���� � �������� ����� ��� 
    /// ���� ��������� ����������, �� ������������ ������ ������� �����
    ///
    /// </summary>
    /// 

    public void FindsInterestingThemes(NpcHearing npcsVoice)
    {
        string npcTheme;

        isHearOtherNpc = true;

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
                    Debug.Log(listOfDialogues[0].tags[i]);
                    agent.SetDestination(npcsVoice.transform.parent.position.normalized);
                    npcToFollow = npcsVoice.transform.parent.position.normalized;
                    dialogue.queueToPlay.Add(this);

                    npcTheme = npcData.theme;
                    reactionToTheme = "React to this text " + "'" + npcTheme + "'" + " in 10 words";
                    dialogue.GenerateGPTDialogue(reactionToTheme);//�������� ����� ������������ ����� GPT, ���� ��������� �� scriptableObject ������������� �� NPC

                    return;
                }
                
            }
         
        }
        Debug.Log("no matches");
        isHearOtherNpc = false;
    }

    private bool CompareTags()
    {
        return true;
    }


    private void MemorySpeach()
    {

    }

}
