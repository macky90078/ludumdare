using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour {

    // Variables for Bouncer into Chaser evolution
    [SerializeField] private GameObject m_chaserEnemy;
    [HideInInspector] public List<Collider2D> m_bouncersInRange;
    [HideInInspector] public int m_bouncerEvolveCount = 0;
    [HideInInspector] public Transform m_bouncerLastPos;

    // Variables for Chaser into Dasher evolution
    [SerializeField] private GameObject m_dasherEnemy;
    [HideInInspector] public List<Collider2D> m_chasersInRange;
    [HideInInspector] public int m_chaserEvolveCount = 0;
    [HideInInspector] public Transform m_chaserLastPos;

    [SerializeField] private GameObject m_gEvolutionParticle;
    private float m_evolutionTimer = 2f;

    private bool m_isEvolveParticle = false;

    void Update ()
    {
        // Evolve Bouncer Into Chaser type enemy
        if (m_bouncerEvolveCount >= 4)
        {
            foreach (Collider2D item in m_bouncersInRange)
            {
                if (item.CompareTag("Enemy0") && m_evolutionTimer <= 0f)
                {
                    Destroy(item.gameObject);
                }
            }
            m_evolutionTimer -= Time.deltaTime;
            GameObject evolutionParticle = null;
            if (!m_isEvolveParticle)
            {
                evolutionParticle = Instantiate(m_gEvolutionParticle, m_bouncerLastPos.position, m_bouncerLastPos.rotation);
                m_isEvolveParticle = true;
            }
            if (m_evolutionTimer <= 0f)
            {
                Instantiate(m_chaserEnemy, m_bouncerLastPos.position, m_bouncerLastPos.rotation);
                m_bouncerLastPos = null;
                Destroy(evolutionParticle);
                m_isEvolveParticle = false;
                m_bouncersInRange.Clear();
                m_evolutionTimer = 2f;
                m_bouncerEvolveCount = 0;
            }
        }

        // Evolve Chaser Into Dasher type enemy
        if (m_chaserEvolveCount >= 4)
        {
            foreach(Collider2D item in m_chasersInRange)
            {
                if (item.CompareTag("ChaserEnemy"))
                {
                    Destroy(item.gameObject);
                }
            }
            Instantiate(m_dasherEnemy, m_chaserLastPos.position, m_chaserLastPos.rotation);
            m_chaserEvolveCount = 0;
            m_chaserLastPos = null;
            m_chasersInRange.Clear();
        }
    }


}
