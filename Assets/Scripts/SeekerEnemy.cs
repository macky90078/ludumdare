using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : MonoBehaviour
{
    private GameObject m_target;

    private Rigidbody2D m_rb;

    [SerializeField] private float m_force;
    [SerializeField] private float m_effectRadius = 0.5f;

    [SerializeField] Collider2D[] inRange;

    [SerializeField] private bool m_bIsMultiplayer = false;
    [SerializeField] private bool m_bIsSingleplayer = true;

    public bool m_hasIncrementEvolveCount = false;

    private GameManager m_gameManager;
    private MultiPlayerGameManager m_gameManagerMultiplayer;

    private Vector3 m_moveDirection;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

        if (m_bIsSingleplayer)
        {
            m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        if (m_bIsMultiplayer)
        {
            m_gameManagerMultiplayer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MultiPlayerGameManager>();
        }

        m_target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        EvolveIntoDasher();
    }

    private void FixedUpdate()
    {
        if (m_target != null)
        {
            m_moveDirection = CalculateMoveDirection();
            m_rb.AddForce(m_moveDirection * m_force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DasherEnemy")
        {
            Destroy(gameObject);
        }
    }

    private void EvolveIntoDasher()
    {
        inRange = Physics2D.OverlapCircleAll(transform.position, m_effectRadius);
        foreach (Collider2D item in inRange)
        {
            if (item.CompareTag("ChaserEnemy") && item.gameObject != gameObject && !m_hasIncrementEvolveCount)
            {
                m_gameManager.m_evolveCount += 1;
                m_hasIncrementEvolveCount = true;
            }
            if (m_gameManager.m_evolveCount >= 3)
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 direction = new Vector3();
        direction = m_target.transform.position - this.transform.position;

        return direction.normalized;
    }
}

