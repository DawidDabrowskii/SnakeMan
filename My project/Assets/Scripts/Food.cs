using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition() // funkcja losowego spawnu
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(-23, 23);
        float y = Random.Range(-11, 11);

        this.transform.position = new Vector3(Mathf.Round(x),Mathf.Round(y),0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other) // na kolizje z "Player" respawn
    {
        if (other.tag == "Player")
        {
            RandomizePosition();
        }      
    }
}
