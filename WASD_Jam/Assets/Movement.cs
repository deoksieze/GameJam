using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float moveVerctical;
    private float moveHorizontal;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVerctical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveHorizontal * speed, moveVerctical * speed);
    }
}