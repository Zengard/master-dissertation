using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName ="DialogueDatabase/Dialigue")]
public class DialogueData : ScriptableObject
{
    public enum TraitsType
    {
        Oppennes = 0,
        Conscientiousness = 1,
        Extraversion = 2,
        Agreeableness = 3,
        Neuroticism = 4,
    }

    public int id;
    public string theme;
    public TraitsType fixedTrait;
    public string[] tags;

    [TextAreaAttribute]
    public string[] dialogueLines;

    [TextArea(8, 8)]
    public string fullDialogueSpeech;  

    public float ChoseTrait(TraitsType fixedTrait, float choosenTrait, float oppennes, float conscientiousness, float extraversion, float agreeableness, float neuroticism) 
    {
        switch (fixedTrait)
        {
            case TraitsType.Oppennes:
                choosenTrait = oppennes;
                return choosenTrait;

            case TraitsType.Conscientiousness:
                choosenTrait = conscientiousness;
                return choosenTrait;
                
            case TraitsType.Extraversion:
                choosenTrait = extraversion;
                return choosenTrait;
                ;
            case TraitsType.Agreeableness:
                choosenTrait = agreeableness;
                return choosenTrait;
                
            case TraitsType.Neuroticism:
                choosenTrait = neuroticism;
                return choosenTrait;

            default:
                return choosenTrait;

        }
    }
}
