using UnityEngine;

public class SpikeAction : MonoBehaviour
{
    float speed = 5f;

    void Update()
    {
        float moveVectorY = Time.deltaTime * speed;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - moveVectorY,
            transform.position.z
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("SpikeDestroyer"))
        {
            Destroy(gameObject);
            Debug.Log("Spike : aa");
        }
    }
}
