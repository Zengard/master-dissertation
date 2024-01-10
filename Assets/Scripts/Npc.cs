using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public GameObject dialogueInteractionIcon;
    public GameObject dialogueBubble;

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
    public bool ishearOtherNpc;
    public string[] listOfOthersNpcThemes;

    [Space]
    public DialogueData[] listOfDialogues;

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
    }

    /// <summary>
    /// сделать метод в котором ishearOtherNpc = true
    /// и персонаж анализирует кого он услышал и через передаваемый параметер в заголовок
    /// после чего начинает идти в его сторону по навмешу
    /// </summary>


    public void MemorySpeach()
    {

    }

}
