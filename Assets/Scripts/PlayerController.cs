// using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 9f;
    public float maxSpeed = 8f;
    Rigidbody2D rb;
    public GameObject boosterFlame;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplier = 10f;
    public UIDocument uiDocument;
    private Label scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        UpdateScore();
        MovePlayer();
    }

    void UpdateScore()
    {
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);

        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        scoreText.text = "Score: " + score;
    }

    void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            //Vector3 mousePos = Mouse.current.position.value;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Debug.Log("Mouse was pressed at position: " + mousePos);

            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;

            // scale so per-frame calls sum to 'thrustForce' per physics step
            float perFrameScale = Time.deltaTime / Time.fixedDeltaTime;
            rb.AddForce(direction * thrustForce * perFrameScale);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
