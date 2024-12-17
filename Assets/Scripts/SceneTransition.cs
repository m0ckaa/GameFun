using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public float speed = 1.0f;
    public Image background;
    public Color bgColor = Color.clear;

    private int sceneToLoad;
    private float targetA;
    private bool loadScene;

    public static SceneTransition Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if(loadScene)
        {
            bgColor.a = Mathf.MoveTowards(bgColor.a, targetA,
                Time.deltaTime * speed);
            background.color = bgColor;

            if(bgColor.a == targetA)
            {
                if (targetA == 1.0f)
                {
                    SceneManager.LoadScene(sceneToLoad);
                    FadeOut();
                }
                else
                {
                    loadScene = false;
                    background.gameObject.SetActive(false);
                }
            }

        }

        if (!loadScene && Input.GetKeyDown(KeyCode.P))
        {
            LoadScene(0);
        }
    }
       

    public void LoadScene(int index)
    {
        sceneToLoad = index;
        loadScene = true;
        FadeIn();
    }

    private void FadeIn()
    {
        targetA = 1.0f;
        background.gameObject.SetActive(true);
    }

    private void FadeOut()
    {
        targetA = 0.0f;
    }
}
