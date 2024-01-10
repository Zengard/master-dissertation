using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    //добавить объект класса NPC чтобы ссылаться на него
    //сделать синглтон чтобы персонажи могли вызывать методы

    [TextAreaAttribute]
    public string[] lines;

    public float textSpeed;

    private int index;
    public int tempIndex;//////////////////////////////////// проверить на правильность

    public bool inDialogue;

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
            inDialogue = false;
            tempIndex = 0;
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
