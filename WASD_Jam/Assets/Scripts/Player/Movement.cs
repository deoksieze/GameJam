using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // public float speed;
    private float moveVerctical;
    private float moveHorizontal;
    private Vector2 directon;

    [HideInInspector]
    public Vector2 lastMovedVector;

    //Reference
    private Rigidbody2D rb;
    public CharacterScriptableObject characterData;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1f, 0f); //Наппавление стрельбы, если мы не двигаемся
    }

    // Update is called once per frame
    void Update()
    {
        moveVerctical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        directon = new Vector2(moveHorizontal, moveVerctical).normalized;

        if(directon.x != 0) {
            
            lastMovedVector = new Vector2(directon.x, 0f);
        }
        if(directon.y != 0) {
            lastMovedVector = new Vector2(0f, directon.y);
        }
        
        if (directon.x != 0 && directon.y != 0)
        {
            lastMovedVector = new Vector2(directon.x, directon.y);
        }


        rb.linearVelocity = directon * characterData.MoveSpeed; 
    }

    public Vector2 GetDirection() 
    {
        return directon;
    }
}