using UnityEngine;

public class BallController : MonoBehaviour
{    
    [SerializeField] private int deathY;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(transform.position.y < deathY)
        {
            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.GameOver();
        }
    }
}
