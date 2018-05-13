using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        Gameplay.Instance.LoseLife();
    }
}
