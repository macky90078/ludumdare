using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject m_target;

    private Rigidbody2D m_rb;
    [SerializeField] private float m_force;
    private Vector3 m_moveDirection;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (m_target != null)
        {
            m_moveDirection = CalculateMoveDirection();
            m_rb.AddForce(m_moveDirection * m_force);
        }
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 direction = new Vector3();
        direction = m_target.transform.position - this.transform.position;

        return direction.normalized;
    }
}

