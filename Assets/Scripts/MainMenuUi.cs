using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject howToPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenHowTo()
    {
        if (howToPanel != null)
            howToPanel.SetActive(true);
    }

    public void CloseHowTo()
    {
        if (howToPanel != null)
            howToPanel.SetActive(false);
    }
}