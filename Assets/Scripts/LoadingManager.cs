using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform grid;

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            GameObject buttonGO = Instantiate(buttonPrefab, grid);

            TextMeshProUGUI tmp = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = i.ToString();

            Button button = buttonGO.GetComponent<Button>();

            int sceneIndex = i;
            button.onClick.AddListener(() => LoadScene(sceneIndex));
        }
    }

    void LoadScene(int index)
    {
        //SceneManager.LoadScene(index);

        SceneTransition.Instance.LoadScene(index);
    }
}
