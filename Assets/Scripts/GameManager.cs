using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text m_scoreText;

    [SerializeField] private GameObject m_enemy0;
    [SerializeField] private GameObject m_PickUpObj;
    [SerializeField] private GameObject m_playerObj;

    public int m_playerPoints;

    [HideInInspector] public bool m_spawnEnemy0 = false;
    [HideInInspector] public bool m_spawnPickUp = false;
    [HideInInspector] public bool m_playerDead = false;
	
	void Update ()
    {
        m_scoreText.text = "Score: " + m_playerPoints.ToString();


        if (Input.GetButtonDown("XboxStartButton"))
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

		if(m_spawnEnemy0)
        {
            Vector3 position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-1.6f, 1.6f), 0f);
            GameObject enemy = m_enemy0;
            Instantiate(enemy, position, m_enemy0.transform.rotation);
            m_spawnEnemy0 = false;
        }
        if(m_spawnPickUp)
        {
            Vector3 position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-1.6f, 1.6f), 0f);
            GameObject pickUp = m_PickUpObj;
            Instantiate(pickUp, position, m_enemy0.transform.rotation);
            m_spawnPickUp = false;
        }
        if(m_playerDead)
        {
            m_playerObj.SetActive(false);
        }
    }
}
