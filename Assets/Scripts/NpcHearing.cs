using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHearing : MonoBehaviour
{
    public float HearingRange;
    public LayerMask HearNpcLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public Collider[] NpcsColliders;
    public List<Npc> npcs;

    void Start()
    {
       
    }
    void Update()
    {
   
        //DrawHearingSphere();        

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {           
            if (other is CapsuleCollider)
            {
                if (other.GetComponent<Npc>().isHearOtherNpc == false)
                {
                    npcs.Add(other.gameObject.GetComponent<Npc>());
                    other.GetComponent<Npc>().isHearOtherNpc = true;

                    other.GetComponent<Npc>().FindsInterestingThemes(this);
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
                    other.GetComponent<Npc>().isHearOtherNpc = false;
                
            }
        }
    }


    //private void DrawHearingSphere()
    //{
    //    NpcsColliders = Physics.OverlapSphere(this.transform.position, HearingRange, HearNpcLayer);



    //}

    //private void OnDrawGizmos()
    //{
    //    if (NpcsColliders.Length == 0)
    //    {
    //        Gizmos.color = Color.yellow;

    //    }
    //    else
    //    {
    //        Gizmos.color = Color.green;
    //    }


    //    Gizmos.DrawWireSphere(transform.position, HearingRange);
    //}
}
