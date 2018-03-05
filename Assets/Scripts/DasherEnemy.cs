using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherEnemy : MonoBehaviour {

    [SerializeField] private float m_timeToDist;
    [SerializeField] private float m_launchCount = 5;

    private float m_movementDist;
    private float m_force;

    private GameObject m_target;

    private Rigidbody2D m_rb;

    private Vector2 m_relativePos;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_target = GameObject.FindGameObjectWithTag("Player");
    }

	void Update ()
    {
        m_launchCount -= Time.deltaTime;

        if (m_target != null)
        {
            RotateTowardsPlayer();
        }
    }

    private void FixedUpdate()
    {
        if(m_launchCount <=0 )
        {
            LaunchAtPlayer();
        }
    }

    private void LaunchAtPlayer()
    {
        m_movementDist = Vector2.Distance(m_target.transform.position, transform.position);
        m_force = CalculateMovementForce(m_movementDist, m_timeToDist, m_rb.velocity.magnitude);
        Vector3 dir = m_target.transform.position - transform.position;
        m_rb.AddForce(dir.normalized * m_force, ForceMode2D.Impulse);
        m_launchCount = 5f;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 vectorToTarget = m_target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
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
