﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [SerializeField] private float m_movementDist;
    [SerializeField] private float m_timeToDist;

    private float m_force;
    private float m_tarAngle;

    private Vector3 m_moveDirection;

    [SerializeField] private GameObject m_gameManagerObj;
    private GameManager m_gameManager;

    private Rigidbody2D m_rb;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gameManager = m_gameManagerObj.GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy0")
        {
            m_gameManager.m_playerDead = true;
        }
    }

    private void FixedUpdate()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        m_tarAngle = Mathf.Atan2(vert, horz) * Mathf.Rad2Deg;

        m_moveDirection = Quaternion.AngleAxis(m_tarAngle, Vector3.forward) * Vector3.right;      

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            m_force = CalculateMovementForce(m_movementDist, m_timeToDist, m_rb.velocity.magnitude);
            m_rb.AddForce(m_moveDirection * m_force);
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
