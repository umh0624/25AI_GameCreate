using UnityEngine;

public class AAA : MonoBehaviour
{
    float start_point = 6.0f;

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_point++;
        transform.position = new Vector3(start_point, 0.01f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        start_point -= 0.02f;
        transform.position = new Vector3(start_point, transform.position.y, transform.position.z);
    }
}
