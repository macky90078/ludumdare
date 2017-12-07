using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickUp : MonoBehaviour {

    [SerializeField] private int m_pointsAmount;

    private GameObject m_gameManagerObj = null;
    GameManager m_gameManager;

    private void Awake()
    {
        m_gameManagerObj = GameObject.Find("GameManager");
        m_gameManager = m_gameManagerObj.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_gameManager.m_playerPoints += m_pointsAmount;
            m_gameManager.m_spawnPickUp = true;
            m_gameManager.m_spawnEnemy0 = true;
            Destroy(gameObject);
        }
    }
}
