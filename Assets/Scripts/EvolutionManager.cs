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

    void Update ()
    {
        // Evolve Bouncer Into Chaser type enemy
        if (m_bouncerEvolveCount >= 4)
        {
            foreach (Collider2D item in m_bouncersInRange)
            {
                if (item.CompareTag("Enemy0"))
                {
                    Destroy(item.gameObject);
                }
            }
                Instantiate(m_chaserEnemy, m_bouncerLastPos.position, m_bouncerLastPos.rotation);
                m_bouncerLastPos = null;
                m_bouncersInRange.Clear();
                m_bouncerEvolveCount = 0;
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
            m_chaserLastPos = null;
            m_chasersInRange.Clear();
            m_chaserEvolveCount = 0;
        }
    }


}
