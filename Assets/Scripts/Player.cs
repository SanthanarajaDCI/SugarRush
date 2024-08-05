using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer sr;
    public GameObject player;


    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        Debug.Log("Game is Start");

        Instantiate(player, new Vector3(1f, 2f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        transform.position = transform.position + pos * 10f * Time.deltaTime;
    }
}