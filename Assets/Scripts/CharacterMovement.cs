using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    [SerializeField] private int m_iPlayerNumber;

    [SerializeField] private float m_regMovementDist;
    [SerializeField] private float m_regTimeToDist;

    [SerializeField] private float m_dashMovementDist;
    [SerializeField] private float m_dashTimeToDist;
    [SerializeField] private float m_dashInvulnerableTime;
    [SerializeField] private float m_effectRadius = 1;

    [SerializeField] private bool m_bIsMultiplayer = false;
    [SerializeField] private bool m_bIsSingleplayer = true;

    [SerializeField] private GameObject m_gDashParticle;

    private float m_moveforce;
    private float m_dashForce;

    private float m_horz;
    private float m_vert;
    private float m_tarAngle;

    private bool m_bPlayerMoveInput = false;
    private bool m_bPlayerDashInput = false;
    private bool m_bPlayerdashed = false;

    private Vector3 m_moveDirection;

    private GameManager m_gameManager;
    private MultiPlayerGameManager m_gameManagerMultiplayer;

    private Rigidbody2D m_rb;

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

    }

    private void Update()
    {
        m_horz = InputManager.LeftHorizontal(m_iPlayerNumber);
        m_vert = InputManager.LeftVertical(m_iPlayerNumber);

        if (InputManager.LeftHorizontal(m_iPlayerNumber) != 0 || InputManager.LeftVertical(m_iPlayerNumber) != 0)
        {
            m_bPlayerMoveInput = true;
        }
        else
        {
            m_bPlayerMoveInput = false;
        }

        if (InputManager.AButton(m_iPlayerNumber))
        {
            m_bPlayerDashInput = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy0" || collision.gameObject.tag == "DasherEnemy" || collision.gameObject.tag == "ChaserEnemy")
        {
            if (!m_bPlayerdashed)
            {
                if (m_bIsSingleplayer)
                {
                    m_gameManager.m_playerDead = true;
                }
                if (m_bIsMultiplayer)
                {
                    m_gameManagerMultiplayer.m_playerDead = true;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        m_rb.velocity = new Vector2(Mathf.Clamp(m_rb.velocity.x, -75f, 75f), Mathf.Clamp(m_rb.velocity.y, -75f, 75f));
        m_tarAngle = Mathf.Atan2(m_vert, m_horz) * Mathf.Rad2Deg;

        if (m_bPlayerMoveInput)
        {
            m_moveDirection = Quaternion.AngleAxis(m_tarAngle, Vector3.forward) * Vector3.right;
            m_moveforce = CalculateMovementForce(m_regMovementDist, m_regTimeToDist, m_rb.velocity.magnitude);
            m_rb.AddForce(m_moveDirection.normalized * m_moveforce);
        }

        if(m_bPlayerDashInput)
        {
            m_dashForce = CalculateMovementForce(m_dashMovementDist, m_dashTimeToDist, m_rb.velocity.magnitude);
            StartCoroutine(Dashed(m_dashInvulnerableTime));
            m_rb.AddForce(m_moveDirection.normalized * m_dashForce);
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

    IEnumerator Dashed(float time)
    {
        m_bPlayerdashed = true;
        m_bPlayerDashInput = false;
        GameObject dashParticle = Instantiate(m_gDashParticle, gameObject.transform.position, gameObject.transform.rotation);
        dashParticle.transform.parent = gameObject.transform;

        Collider2D[] inRange = Physics2D.OverlapCircleAll(transform.position, m_effectRadius);
        foreach (Collider2D item in inRange)
        {
            Vector2 m_enemyDist = item.transform.position - transform.position;

            if (item.GetComponent<Rigidbody2D>() && ((item.CompareTag("Enemy0") || item.CompareTag("DasherEnemy") || item.CompareTag("ChaserEnemy"))))
            {
                item.attachedRigidbody.AddForce((m_enemyDist).normalized * m_dashForce);
            }

        }

        yield return new WaitForSeconds(time);

        Destroy(dashParticle);
        m_bPlayerdashed = false;
    }
}
