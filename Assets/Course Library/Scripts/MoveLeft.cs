using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 26;
    private PlayerController PC;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PC.isGameOver())
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < leftBound && gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }
}
