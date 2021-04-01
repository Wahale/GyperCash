using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement_Script : MonoBehaviour
{
    private GameObject player;
    #region Panels
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelSettings;
    [SerializeField] GameObject panelGameMenu;
    [SerializeField] GameObject panelGameOver;
    #endregion

    public Text textPanelGameOver;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        panelMainMenu.SetActive(true);
        panelSettings.SetActive(false);
        panelGameMenu.SetActive(false);
        panelGameOver.SetActive(false);

        textPanelGameOver.GetComponent<Text>();
    }

    #region panelMainMenu
    public void StartGame()
    {
        panelMainMenu.SetActive(false);
        panelSettings.SetActive(true);

        player.GetComponent<PlayerController>().Game();
    }

    public void Shop()
    {

    }

    public void TurnOffADS()
    {

    }
    #endregion

    #region panelSettings
    public void Settings()
    {
        panelGameMenu.SetActive(true);
    }
    #endregion

    #region panelGameMenu
    public void MainMenu()
    {
        panelMainMenu.SetActive(true);
        panelGameMenu.SetActive(false);
        panelSettings.SetActive(false);

        player.GetComponent<PlayerController>().NoGame();

        SceneManager.LoadScene(0);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    public void PlayADS()
    {
        /// Запуск рекламы
    }

    public void GameOverVictory()
    {
        panelGameOver.SetActive(true);
        textPanelGameOver.text = "You won";
    }

    public void GameOverLost()
    {
        panelGameOver.SetActive(true);
        textPanelGameOver.text = "You've lost";
    }
}