using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private bool inMotion;
    public float initialSpeed = 2500;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!inMotion && Input.GetButtonDown("Vertical"))
        {
            inMotion = true;
            transform.parent = null;
            rigid.AddForce(new Vector2(initialSpeed, initialSpeed));
        }
    }
}
