using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] GameObject m_multiplayerModeCanvas;
    [SerializeField] Text m_buttonText;
    [SerializeField] Image m_displayImage;

    [SerializeField] Sprite m_twoPlayerDisplay;
    [SerializeField] Sprite m_threePlayerDisplay;
    [SerializeField] Sprite m_fourPlayerDisplay;

    private bool m_reset;
    public static int m_totalPlayer = 2;

    private void Update()
    {
        //For multiplayer mode
        if (m_multiplayerModeCanvas.activeSelf)
        {
            //Choose player number
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 && m_reset)
                {
                    m_reset = false;
                    m_totalPlayer++;
                    if (m_totalPlayer > 4)
                    {
                        m_totalPlayer = 2;
                    }
                    UpdateMultiplayerModeUI(m_totalPlayer);
                }
                else if (Input.GetAxisRaw("Horizontal") == -1 && m_reset)
                {
                    m_reset = false;
                    m_totalPlayer--;
                    if (m_totalPlayer < 2)
                    {
                        m_totalPlayer = 4;
                    }
                    UpdateMultiplayerModeUI(m_totalPlayer);
                }
            }
            else
            {
                if (!m_reset)
                {
                    m_reset = true;
                }
            }

            //Back
            if (Input.GetButtonDown("J1BButton"))
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }
    }

    private void UpdateMultiplayerModeUI(int totalPlayer)
    {
        switch (totalPlayer)
        {
            case 2:
                m_displayImage.GetComponent<Image>().sprite = m_twoPlayerDisplay;
                m_buttonText.text = "Two Players";
                break;
            case 3:
                m_displayImage.GetComponent<Image>().sprite = m_threePlayerDisplay;
                m_buttonText.text = "Three Players";
                break;
            case 4:
                m_displayImage.GetComponent<Image>().sprite = m_fourPlayerDisplay;
                m_buttonText.text = "Four Players";
                break;
        }
    }

    public void StartGame(int mode)
    {
        SceneManager.LoadScene(mode, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
