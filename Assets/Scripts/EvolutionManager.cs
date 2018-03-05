using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionManager : MonoBehaviour {

    // Variables for Bouncer into Chaser evolution
    [SerializeField] private GameObject m_chaserEnemy;
     public List<GameObject> m_bouncersInRange;
     public int m_bouncerEvolveCount = 0;
    [HideInInspector] public Transform m_bouncerLastPos;

    // Variables for Chaser into Dasher evolution
    [SerializeField] private GameObject m_dasherEnemy;
    [HideInInspector] public List<GameObject> m_chasersInRange;
    [HideInInspector] public int m_chaserEvolveCount = 0;
    [HideInInspector] public Transform m_chaserLastPos;

    void Update ()
    {
        // Evolve Bouncer Into Chaser type enemy
        if (m_bouncerEvolveCount >= 3)
        {
            foreach (GameObject item in m_bouncersInRange)
            {
                Destroy(item);
            }
            Instantiate(m_chaserEnemy, m_bouncerLastPos.position, m_bouncerLastPos.rotation);
            m_bouncerEvolveCount = 0;
            m_bouncerLastPos = null;
            m_bouncersInRange.Clear();
        }

        // Evolve Chaser Into Dasher type enemy
        if (m_chaserEvolveCount >= 3)
        {
            foreach(GameObject item in m_chasersInRange)
            {
                Destroy(item);
            }
            Instantiate(m_dasherEnemy, m_chaserLastPos.position, m_chaserLastPos.rotation);
            m_chaserEvolveCount = 0;
            m_chaserLastPos = null;
            m_chasersInRange.Clear();
        }
    }
}
