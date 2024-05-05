using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHearing : MonoBehaviour
{
    [SerializeField] private List<Npc> npcs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {           
            if (other is CapsuleCollider)
            {
                if (other.GetComponent<Npc>().goToOtherNPC == false)
                {
                    npcs.Add(other.gameObject.GetComponent<Npc>());
                    //other.GetComponent<Npc>().FindsInterestingThemes(this);
                    other.GetComponent<Npc>().isHearOtherNpc = true;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (other is CapsuleCollider)
            {
                if (other.GetComponent<Npc>().goToOtherNPC == false)
                {
                    other.GetComponent<Npc>().FindInterestingWords(this);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (other is CapsuleCollider)
            {
                    npcs.Remove(other.gameObject.GetComponent<Npc>());
                    other.GetComponent<Npc>().goToOtherNPC = false;
                other.GetComponent<Npc>().dialogueText = "";
                other.GetComponent<Npc>().isHearOtherNpc = false;

            }
        }
    }

    private void OnDisable()
    {
        foreach (var npc in npcs)
        {
            npc.GetComponent<Npc>().dialogueText = "";
            npc.GetComponent<Npc>().isHearOtherNpc = false;
        }

        npcs.Clear();
    }

}
