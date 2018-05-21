using UnityEngine;
using System.Linq;

public class Gameplay : MonoBehaviour
{
    private const float ResetDelay = 1.5f;
    private const string SceneName = "BrickBreakerGame";
    public int BrickAmount;
    public int Lives;
    public int CurrentLevel = 1;
    public GameObject ResettingPaddle;

    public static Gameplay Instance;

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
        BrickAmount = 56;
        Lives = 3;
        Destroy(GameObject.Find("Won(Clone)"));
        Instantiate(Resources.Load("Heart"), GameObject.Find("CanvasUI").transform);
        Instantiate(Resources.Load("Heart 2"), GameObject.Find("CanvasUI").transform);
        Instantiate(Resources.Load("Heart 3"), GameObject.Find("CanvasUI").transform);
        ResettingPaddle = Instantiate(Resources.Load("Paddle"), transform.position, Quaternion.identity) as GameObject;
        var currentBrick = Resources.Load("Lev" + CurrentLevel + "Bricks") as GameObject;
        var colliders = currentBrick.GetComponentsInChildren(typeof(BoxCollider2D), true);
        colliders.Select(x => ((BoxCollider2D) (x)).enabled = true);
      
        Instantiate(currentBrick, transform.position, Quaternion.identity);
    }

    private void CheckIfWon() 
    {
        if (BrickAmount < 1)
        {
            Instantiate(Resources.Load("Won"), GameObject.Find("CanvasUI").transform);
            if(CurrentLevel == 3)
            {
                CurrentLevel = 1;
            }
            else
            {
                CurrentLevel++;
            }
            Invoke("FinishPreviousLevel", 0);
            Invoke("Setup", ResetDelay);
        }
    }

    private void FinishPreviousLevel()
    {
        Destroy(GameObject.Find("Ball"));
        Destroy(GameObject.Find("Paddle(Clone)"));
        Destroy(ResettingPaddle);
        Destroy(GameObject.Find("Lev" + (CurrentLevel - 1) + "Bricks"));
        Destroy(GameObject.Find("Heart(Clone)"));
        Destroy(GameObject.Find("Heart 2(Clone)"));
        Destroy(GameObject.Find("Heart 3(Clone)"));
    }

    private void CheckIfLost()
    {
        if (Lives < 1)
        {
            Instantiate(Resources.Load("Lost"), GameObject.Find("CanvasUI").transform);
            Invoke("ResetLevel", ResetDelay);
        }
    }

    private void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }

    public void LoseLife()
    {
        switch (Lives)
        {
            case 3:
                Destroy(GameObject.Find("Heart 3(Clone)"));
                break;
            case 2:
                Destroy(GameObject.Find("Heart 2(Clone)"));
                break;
            case 1:
                Destroy(GameObject.Find("Heart(Clone)"));
                break;
        }

        Lives--;
        Destroy(ResettingPaddle);
        Destroy(GameObject.Find("Ball"));
        CheckIfLost();
        Invoke("ResetPaddleAfterDeath", ResetDelay);
    }

    private void ResetPaddleAfterDeath()
    {
        ResettingPaddle = Instantiate(Resources.Load("Paddle"), transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        BrickAmount--;
        CheckIfWon();
    }
}