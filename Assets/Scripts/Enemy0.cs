using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0 : MonoBehaviour {

    [SerializeField] private float m_movementDist;
    [SerializeField] private float m_timeToDist;

    public float m_force;

    public Vector3 m_moveDirection;

    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_moveDirection = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward) * Vector3.right;
    }

    private void FixedUpdate()
    {
        m_force = CalculateMovementForce(m_movementDist, m_timeToDist, m_rb.velocity.magnitude);
        m_force = Mathf.Clamp(m_force, -1000f, 1000f);
        m_rb.AddForce(m_moveDirection * m_force);

        if(m_rb.velocity.magnitude < 0f)
        {
            m_moveDirection = (m_moveDirection * -1) + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy0")
        {
            m_moveDirection = (m_moveDirection * -1) + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        }
    }

    private float CalculateMovementForce(float distance, float time, float initVelocity)
    {
        float finalVelocity = CalculateFinalVelocity(distance, time, initVelocity);
        float acceleration = CalculateAcceleration(finalVelocity, initVelocity, time);
        return CalculateForce(m_rb.mass, acceleration);
    }

    float CalculateFinalVelocity(float dist, float time, float initVelocity)
    {
        return (dist / time) - initVelocity / 2;
    }
    float CalculateAcceleration(float finalVelocity, float initVelocity, float time)
    {
        return (finalVelocity - initVelocity) / time;
    }
    float CalculateForce(float mass, float acceleration)
    {
        return mass * acceleration;
    }
}
