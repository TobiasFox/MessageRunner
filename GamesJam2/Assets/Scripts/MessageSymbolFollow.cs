using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSymbolFollow : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform objectToFollow;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = transform.parent;
        offset = Vector3.Distance(transform.position,transform.parent.position);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectToFollow.position+(objectToFollow.transform.forward.normalized*offset*-1), speed * Time.deltaTime);
    }
}
