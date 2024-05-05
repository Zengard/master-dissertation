using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNpc : MonoBehaviour
{
    private bool _isMovingToPoints = false;
     private Transform[] _movingPointsPosition;
    private int _nextPosIndex;
    [SerializeField] private Transform _nextPosition;

    [SerializeField] private float _speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoints();
    }

    private void MoveToPoints()
    {
        if (transform.position != _nextPosition.position && _isMovingToPoints == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition.position, _speed * Time.deltaTime);
        }
        else
        {
            _isMovingToPoints = false;
        }
    }

    private void OnEnable()
    {
        _isMovingToPoints = true;
    }

    private void OnDisable()
    {
        _isMovingToPoints = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _nextPosition.gameObject)
        {
            this.enabled = false;
        }
    }

}
