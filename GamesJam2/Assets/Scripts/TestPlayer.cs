using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float speed;
    public int playerNum;
    [SerializeField]
    private DefineColors defineColors;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(x * speed * Time.deltaTime, 0, y * speed * Time.deltaTime);
    }

    public void SetPlayerNumber(int number)
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = defineColors.colors[number];
    }
}
