using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSymbolFollow : MonoBehaviour
{
    [SerializeField] private speed;

    private Transform objectToFollow;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = transform.parent;
        offset = objectToFollow.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
