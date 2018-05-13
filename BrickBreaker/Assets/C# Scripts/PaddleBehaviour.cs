using UnityEngine;

public class PaddleBehaviour : MonoBehaviour
{
    private const float PADDLE_HEIGHT = -4.5f;
    private const float MIN_LIMIT = -8;
    private const float MAX_LIMIT = 8;
    public const float speed = 1;
    private Vector2 position = new Vector2(0, PADDLE_HEIGHT);

    void Update()
    {
        float newXPosition = transform.position.x + Input.GetAxis("Horizontal") * speed;
        position = new Vector2(Mathf.Clamp(newXPosition, MIN_LIMIT, MAX_LIMIT), PADDLE_HEIGHT);
        transform.position = position;
    }
}
