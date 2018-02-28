using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text m_scoreText;

    [SerializeField] private GameObject m_enemy0;
    [SerializeField] private GameObject m_PickUpObj;
    [SerializeField] private GameObject m_playerObj;

    [SerializeField] private GameObject m_cameraObj;
    private AudioSource m_music;

    private AudioSource m_soundEffect;
    [SerializeField] private AudioClip m_DeathSound;
    
    public int m_playerPoints;

    [HideInInspector] public bool m_spawnEnemy0 = false;
    [HideInInspector] public bool m_spawnPickUp = false;
    [HideInInspector] public bool m_playerDead = false;

    private void Awake()
    {
        m_music = m_cameraObj.GetComponent<AudioSource>();
        m_soundEffect = GetComponent<AudioSource>();
    }

    void Update ()
    {
        m_scoreText.text = "Score: " + m_playerPoints.ToString();


        if (Input.GetButtonDown("XboxStartButton"))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }

		if(m_spawnEnemy0)
        {
            Vector3 position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-1.6f, 1.6f), 0f);
            GameObject enemy = m_enemy0;
            Instantiate(enemy, position, m_enemy0.transform.rotation);
            m_spawnEnemy0 = false;
        }
        if(m_spawnPickUp)
        {
            m_music.pitch += 0.02f;
            Vector3 position = new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-1.6f, 1.6f), 0f);
            GameObject pickUp = m_PickUpObj;
            Instantiate(pickUp, position, m_enemy0.transform.rotation);
            m_spawnPickUp = false;
        }
        if(m_playerDead)
        {
            m_music.Pause();
            m_soundEffect.PlayOneShot(m_DeathSound);
            m_playerObj.SetActive(false);
            m_playerDead = false;
        }
    }
}
