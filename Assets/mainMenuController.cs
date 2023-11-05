using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject creditsObj;
    [SerializeField] private GameObject mainScreenObj;

    public void OnPlayButton()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnCreditsButton()
    {
        creditsObj.SetActive(true);
        mainScreenObj.SetActive(false);
    }

    public void OnBackButton()
    {
        creditsObj.SetActive(false);
        mainScreenObj.SetActive(true);
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
}
