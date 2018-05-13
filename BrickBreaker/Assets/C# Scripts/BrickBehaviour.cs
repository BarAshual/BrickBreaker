using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D()
    {
        Gameplay.Instance.DestroyBrick();
        Destroy(gameObject);
    }
}
