using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseCollider : MonoBehaviour
{
    [SerializeField] GameObject engine;
    [SerializeField] GameObject brokenEgg;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip catchSound;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("egg")) // Check if the colliding object is tagged as "Fruit"
        {
            engine.GetComponent<Engine>().lifes--; // subtract life if collision with ground
            Destroy(collision.gameObject); // destroy fruit
            GameObject instantiatedBrokenEgg = Instantiate(brokenEgg, collision.transform.position, Quaternion.identity);
            Destroy(instantiatedBrokenEgg, 1f); // Destroy broken egg after 1 second
        }else if(collision.gameObject.CompareTag("goldenegg")){
            Destroy(collision.gameObject);
            GameObject instantiatedBrokenEgg = Instantiate(brokenEgg, collision.transform.position, Quaternion.identity);
            Destroy(instantiatedBrokenEgg, 1f); // Destroy broken egg after 1 second
        }
        audioSource.PlayOneShot(catchSound); //play sfx after catch
    }
}

