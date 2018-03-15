using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHasEvolved : MonoBehaviour {

    private AudioSource m_soundEffect;
    [SerializeField] private AudioClip m_EvolveSound;

    [SerializeField] private GameObject m_evolveParticle;
    private GameObject m_childObject;
    private GameObject m_spawnedEvolveParticle;

    private void Awake()
    {
        m_soundEffect = GetComponent<AudioSource>();
        m_childObject = this.gameObject.transform.GetChild(0).gameObject;
    }

    void Start ()
    {
        StartCoroutine(HasSpawned());
	}
	
    private IEnumerator HasSpawned()
    {
        m_childObject.gameObject.SetActive(false);
        m_spawnedEvolveParticle = Instantiate(m_evolveParticle, transform.position, transform.rotation);
        m_soundEffect.PlayOneShot(m_EvolveSound);
        yield return new WaitForSeconds(2f);
        m_childObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Destroy(m_spawnedEvolveParticle);
    }
}
