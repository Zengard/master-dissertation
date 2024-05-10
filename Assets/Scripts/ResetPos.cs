using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPos : MonoBehaviour
{

    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    public float speed;
    public float defaultSpeed;
    private float randomPos;
    [SerializeField] Transform endPoint;
    public Vector3 pointToMove;

    public GameObject TELEPORTER;
    [Space]
    [Space]
    public bool canGetNewTheme = false;
   public List<DialogueData> newThemes;
    private int randomTheme;
    void Start()
    {
        defaultSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);

        if(transform.position == pointToMove || transform.position.z >= pointToMove.z -1)
        {
            randomPos = Random.Range(leftBorder.position.x, rightBorder.position.x);

            transform.position = new Vector3(randomPos, 0, -17);

            if(canGetNewTheme == true)
            {
                randomTheme = Random.Range(0, newThemes.Count);
                gameObject.GetComponent<Npc>().listOfDialogues.Add(newThemes[randomTheme]);
            }
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "End")
    //    {
    //        randomPos = Random.Range(leftBorder.position.x, rightBorder.position.x);

    //        transform.position = new Vector3(randomPos, 0, -17);

    //        TELEPORTER = other.gameObject;

    //        Debug.Log("TELEPORTER TELEPORTER!!" + "   " + TELEPORTER);
    //    }
    //}
}
