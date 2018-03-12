using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerPointsPickUp : MonoBehaviour {

    MultiPlayerGameManager m_gameManager;

    private int m_index;
    [SerializeField] private int m_pointsAmount;


    // Use this for initialization
    void Start () {
        m_gameManager = GameObject.Find("GameManager").GetComponent<MultiPlayerGameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_gameManager.GeneratePointsPickUp(m_index);
            m_gameManager.SpawnEnemy(m_index);
            m_gameManager.addScore(m_index, m_pointsAmount);
            Destroy(gameObject);
        }
    }

    public void SetIndex(int index)
    {
        m_index = index;
    }
}
