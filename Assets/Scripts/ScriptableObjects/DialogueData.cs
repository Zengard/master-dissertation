using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName ="DialogueDatabase/Dialigue")]
public class DialogueData : ScriptableObject
{
    public int id;
    public string theme;
    public TraitsType fixedTrait;
    public string[] tags;

    [TextAreaAttribute]
    public string[] dialogueLines;

    public enum TraitsType
    {
        Oppennes = 1 <<0,
        Conscientiousness = 1 << 1,
        Extraversion = 1 << 2,
        Agreeableness = 1 << 3,
        Neuroticism = 1 << 4,
    }
}
