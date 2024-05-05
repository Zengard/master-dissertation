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
    void Start()
    {
        defaultSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "End")
        {
            randomPos = Random.Range(leftBorder.position.x, rightBorder.position.x);

            transform.position = new Vector3(randomPos, 0, -17);
        }
    }
}
