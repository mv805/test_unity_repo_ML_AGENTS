using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 100f;
    public float maxSpeed = 300f;
    public float maxSpinSpeed = 20f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //test comment
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        // Smaller obstacles move faster to balance gameplay difficulty
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update() { }
}
