using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip gameWinClip;
    [SerializeField] AudioClip gameMusicClip;
    [SerializeField] GameObject plane;
    [SerializeField] TextMeshProUGUI uiText;
    [SerializeField] Image image;
    [SerializeField] Canvas canvas;

    private void Start()
    {
        image.enabled = false;
        uiText.enabled = false;
    }

    public void GameStart()
    {
        plane.GetComponent<FloorController>().currentRotation = Vector3.zero;
        ballRb.velocity = Vector3.zero;
        ballRb.transform.position = new Vector3(0, 8, 0);
        ballRb.gameObject.SetActive(true);
        canvas.enabled = false;
        
    }

    public void GameOver()
    {
        ballRb.gameObject.SetActive(false);
        canvas.enabled = true;
        image.enabled = true;
        uiText.enabled = true;
        uiText.text = "GAME OVER";
        source.PlayOneShot(gameOverClip);
    }
}
