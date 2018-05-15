using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public float delay = 1.5f;
    public int numOfBricks = 56;
    public int lives = 3;
    public GameObject Won;
    public GameObject Lost;
    public GameObject brickTemplate;
    public GameObject basePaddle;
    public GameObject resettingPaddle;
    public GameObject FirstHeart;
    public GameObject SecondHeart;
    public GameObject ThirdHeart;

    public static Gameplay Instance = null;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        Setup();
    }

    public void Setup()
    {
        resettingPaddle = Instantiate(basePaddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(brickTemplate, transform.position, Quaternion.identity);
    }

    private void CheckIfWon()
    {
        if (numOfBricks < 1)
        {
            Won.SetActive(true);
            Invoke("ResetLevel", delay);
        }
    }

    private void CheckIfLost()
    {
        if (lives < 1)
        {
            Lost.SetActive(true);
            Invoke("ResetLevel", delay);
        }
    }

    private void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BrickBreakerGame");
    }

    public void LoseLife()
    {
        switch (lives)
        {
            case 3:
                FirstHeart.SetActive(false);
                break;
            case 2:
                SecondHeart.SetActive(false);
                break;
            case 1:
                ThirdHeart.SetActive(false);
                break;
        }
        lives--;
        Destroy(resettingPaddle);
        CheckIfLost();
        Invoke("ResetPaddleAfterDeath", delay);
    }

    private void ResetPaddleAfterDeath()
    {
        resettingPaddle = Instantiate(basePaddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        numOfBricks--;
        CheckIfWon();
    }
}