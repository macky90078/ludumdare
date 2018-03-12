using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsPickUp : MonoBehaviour {

    [SerializeField] private int m_pointsAmount;

    private AudioSource m_soundEffect;
    [SerializeField] private AudioClip m_pickupSound;

    private GameObject m_gameManagerObj = null;
    GameManager m_gameManager;

    private void Awake()
    {
        m_gameManagerObj = GameObject.Find("GameManager");
        m_gameManager = m_gameManagerObj.GetComponent<GameManager>();
        m_soundEffect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_gameManager.m_playerPoints += m_pointsAmount;
            m_soundEffect.PlayOneShot(m_pickupSound);
            m_gameManager.m_spawnPickUp = true;
            m_gameManager.m_spawnEnemy = true;
            Destroy(gameObject);
        }
    }
}
