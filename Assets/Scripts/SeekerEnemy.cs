using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : MonoBehaviour
{
    private GameObject m_target;

    private Rigidbody2D m_rb;

    [SerializeField] private float m_force;
    [SerializeField] private float m_effectRadius = 0.1f;

    private bool m_hasIncrementEvolveCount = false;

    private EvolutionManager m_evolutionManager;

    private Vector3 m_moveDirection;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

        m_evolutionManager = GameObject.FindGameObjectWithTag("EvolutionManager").GetComponent<EvolutionManager>();

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
        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, m_effectRadius);
        foreach (Collider2D item in inRange)
        {
            if (item.CompareTag("ChaserEnemy") && item.gameObject != gameObject && !m_hasIncrementEvolveCount)
            {
                m_evolutionManager.m_chasersInRange.Add(gameObject);
                m_evolutionManager.m_chaserLastPos = transform;
                m_evolutionManager.m_chaserEvolveCount += 1;
                m_hasIncrementEvolveCount = true;
            }
            else if(!item.CompareTag("ChaserEnemy") && item.gameObject != gameObject)
            {
                m_hasIncrementEvolveCount = false;
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

