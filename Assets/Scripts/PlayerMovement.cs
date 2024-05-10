using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float _speed;
    [Space]
    [SerializeField] private GameObject dialogueWindow;

    private DialogueData npcDataOfDialogues; // get list of npc's speech 


    [SerializeField] private Npc npcParameters; // make private in future
    [SerializeField] private bool _canSpeak;

    [Space]
    [SerializeField] private GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * _speed);

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);


        if (_canSpeak)
        {
            if (Input.GetKeyDown(KeyCode.X) && npcParameters != null && dialogueWindow.GetComponent<Dialogue>().inDialogue == false)
            {
                //npcDataOfDialogues = npcParameters.GetComponent<Npc>().listOfDialogues[0];// 0 replace with correct index in future

                //dialogueWindow.GetComponent<Dialogue>().lines = new string[npcDataOfDialogues.dialogueLines.Length];

                //for (int i = 0; i < npcDataOfDialogues.dialogueLines.Length; i++)
                //{
                //    dialogueWindow.GetComponent<Dialogue>().lines[i] = npcDataOfDialogues.dialogueLines[i];
                //}

                //if (npcParameters.GetComponent<Npc>().tempIndex != 0)
                //{
                //    dialogueWindow.GetComponent<Dialogue>().tempIndex = npcParameters.GetComponent<Npc>().tempIndex;
                //}


                //npcParameters.GetComponent<Npc>().dialogueBubble.SetActive(true);
                //npcParameters.GetComponent<Npc>().dialogueInteractionIcon.SetActive(false);


                dialogueWindow.GetComponent<Dialogue>().SetDialogue(dialogueWindow.GetComponent<Dialogue>(), npcParameters);

                dialogueWindow.GetComponent<Dialogue>().queueToPlay.Add(npcParameters);
                dialogueWindow.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (dialogueWindow.GetComponent<Dialogue>().inDialogue == true)
                {
                    dialogueWindow.GetComponent<Dialogue>().InterruptDialogue(npcParameters.GetComponent<Npc>());
                }
            }

            if (dialogueWindow.GetComponent<Dialogue>().inDialogue == false)
            {
                npcParameters.GetComponent<Npc>().dialogueBubble.SetActive(false);
                npcParameters.GetComponent<Npc>().dialogueInteractionIcon.SetActive(true);
            }

        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "NPC")
    //    {
    //        if(dialogueWindow.GetComponent<Dialogue>().inDialogue == false)
    //        {
    //            other.GetComponent<Npc>().dialogueInteractionIcon.SetActive(true);
    //        }

    //        _canSpeak = true;
    //        npcParameters = other.GetComponent<Npc>();
    //    }


    //    if(other.gameObject.tag == "Exit")
    //    {
    //        exit.SetActive(true);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (dialogueWindow.GetComponent<Dialogue>().inDialogue == false)
            {
                other.GetComponent<Npc>().dialogueInteractionIcon.SetActive(true);
            }

            _canSpeak = true;
            npcParameters = other.GetComponent<Npc>();
        }


        if (other.gameObject.tag == "Exit")
        {
            exit.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            _canSpeak = false;
            npcParameters = null;
            //if (dialogueWindow.GetComponent<Dialogue>().inDialogue == true)
            //{

            //    dialogueWindow.GetComponent<Dialogue>().InterruptDialogue(other.GetComponent<Npc>());

            //}

            other.GetComponent<Npc>().dialogueInteractionIcon.SetActive(false);
           // other.GetComponent<Npc>().dialogueBubble.SetActive(false);
            //dialogueWindow.SetActive(false);


        }

    }
}
