using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [SerializeField] private float m_movementDist;
    [SerializeField] private float m_timeToDist;

    public float m_force;
    private float m_tarAngle;

    public bool m_leftVertPos = false;
    public bool m_leftVertNeg = false;

    public bool m_leftHorzPos = false;
    public bool m_leftHorzNeg = false;

    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
       

        if (Input.GetAxisRaw("J1LeftVertical") < 0f)
        {
            m_leftVertPos = true;
        }
        else
        {
            m_leftVertPos = false;
        }

        if (Input.GetAxisRaw("J1LeftVertical") > 0f)
        {
            m_leftVertNeg = true;
        }
        else
        {
            m_leftVertNeg = false;
        }

        if (Input.GetAxisRaw("J1LeftHorizontal") > 0f)
        {
            m_leftHorzPos = true;
        }
        else
        {
            m_leftHorzPos = false;
        }

        if (Input.GetAxisRaw("J1LeftHorizontal") < 0f)
        {
            m_leftHorzNeg = true;
        }
        else
        {
            m_leftHorzNeg = false;
        }

    }

    private void FixedUpdate()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        m_tarAngle = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;

        if(m_leftVertPos)
        {
            m_force = CalculateMovementForce(m_movementDist, m_timeToDist, m_rb.velocity.y);
            m_rb.AddForce(transform.up * m_force, ForceMode2D.Force);
        }
        else if(m_leftVertNeg)
        {
            m_force = CalculateMovementForce(m_movementDist, m_timeToDist, -m_rb.velocity.y);
            m_rb.AddForce(transform.up * -m_force, ForceMode2D.Force);

        }
        else if (m_leftHorzPos)
        {
            m_force = CalculateMovementForce(m_movementDist, m_timeToDist, m_rb.velocity.x);
            m_rb.AddForce(transform.right * m_force, ForceMode2D.Force);
        }
        else if (m_leftHorzNeg)
        {
            m_force = CalculateMovementForce(m_movementDist, m_timeToDist, -m_rb.velocity.x);
            m_rb.AddForce(transform.right * -m_force, ForceMode2D.Force);
        }

    }

    private float CalculateMovementForce(float distance, float time, float initVelocity)
    {
        float finalVelocity = CalculateFinalVelocity(distance, time, initVelocity);
        float acceleration = CalculateAcceleration(finalVelocity, initVelocity, time);
        return CalculateLaunchForce(m_rb.mass, acceleration);
    }

    float CalculateFinalVelocity(float dist, float time, float initVelocity)
    {
        return (dist / time) - initVelocity / 2;
    }
    float CalculateAcceleration(float finalVelocity, float initVelocity, float time)
    {
        return (finalVelocity - initVelocity) / time;
    }
    float CalculateLaunchForce(float mass, float acceleration)
    {
        return mass * acceleration;
    }
}
