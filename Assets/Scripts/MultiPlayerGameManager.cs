using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerGameManager : MonoBehaviour {

    public int m_totalPlayers;

    private Vector2[] m_wallsPosition;
    private Vector2 m_wallSize;
    private Hashtable m_entityScale;

    [SerializeField] private GameObject m_wall0;
    [SerializeField] private GameObject m_wall1;
    [SerializeField] private GameObject m_enemy0;
    [SerializeField] private GameObject m_seekerEnemy;
    [SerializeField] private GameObject m_PickUpObj;
    [SerializeField] private GameObject m_playerObj;
    [SerializeField] private GameObject m_cameraObj;

    [HideInInspector] public bool m_playerDead = false;

    //Sounds
    [SerializeField] private AudioClip m_DeathSound;
    private AudioSource m_music;
    private AudioSource m_soundEffect;

    // Use this for initialization
    void Awake () {
        //Set scale
        m_entityScale = new Hashtable();
        m_entityScale.Add(1, 1.0f);
        m_entityScale.Add(2, 0.9f);
        m_entityScale.Add(3, 0.7f);
        m_entityScale.Add(4, 0.7f);

        //Generate walls
        m_wallsPosition = new Vector2[m_totalPlayers];
        GenerateWalls(m_totalPlayers);

        //Generate pickups
        for(int i = 0; i < m_totalPlayers; i++)
        {
            GeneratePointsPickUp(i);
        }

        //Sounds
        m_music = m_cameraObj.GetComponent<AudioSource>();
        m_soundEffect = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("XboxStartButton"))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        if (m_playerDead)
        {
            m_music.Pause();
            m_soundEffect.PlayOneShot(m_DeathSound);
            m_playerObj.SetActive(false);
            m_playerDead = false;
        }
    }

    private void GenerateWalls(int totalPlayers)
    {
        //Positions depend on size of the background
        float backgroundWidth = GameObject.Find("Background").GetComponent<Renderer>().bounds.size.x*0.95f; //Make them closer.
        float backgroundHeight = GameObject.Find("Background").GetComponent<Renderer>().bounds.size.y;
        GameObject wall;
        Vector2 position;

        switch (totalPlayers)
        {
            case 2:
                wall = m_wall0;
                m_wallSize = new Vector2(2.9f,3.2f);

                position = new Vector2(-backgroundWidth/4, 0);
                m_wallsPosition[0] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(backgroundWidth / 4, 0);
                m_wallsPosition[1] = position;
                Instantiate(wall, position, Quaternion.identity);
                break;
            case 3:
                wall = m_wall1;
                m_wallSize = new Vector2(2.54f, 1.3f);

                position = new Vector2(0, backgroundHeight/4);
                m_wallsPosition[0] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(backgroundWidth / 4, -backgroundHeight / 4);
                m_wallsPosition[1] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(-backgroundWidth / 4, -backgroundHeight/4);
                m_wallsPosition[2] = position;
                Instantiate(wall, position, Quaternion.identity);
                break;
            case 4:
                wall = m_wall1;
                m_wallSize = new Vector2(2.54f, 1.3f);

                position = new Vector2(-backgroundWidth / 4, backgroundHeight / 4);
                m_wallsPosition[0] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(backgroundWidth / 4, backgroundHeight / 4);
                m_wallsPosition[1] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(backgroundWidth / 4, -backgroundHeight / 4);
                m_wallsPosition[2] = position;
                Instantiate(wall, position, Quaternion.identity);

                position = new Vector2(-backgroundWidth / 4, -backgroundHeight / 4);
                m_wallsPosition[3] = position;
                Instantiate(wall, position, Quaternion.identity);
                break;
        }
    }

    public void GeneratePointsPickUp(int index)
    {
        GameObject pickUp;

        Vector3 position = new Vector3(
            m_wallsPosition[index].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
            m_wallsPosition[index].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
            0f
            );
        
        pickUp = Instantiate(m_PickUpObj, position, Quaternion.identity);
        pickUp.GetComponent<MultiPlayerPointsPickUp>().SetIndex(index);
        pickUp.transform.localScale = Vector3.Scale(pickUp.transform.localScale, new Vector3((float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers]));
    }

    public void SpawnEnemy(int thisIndex)
    {
        GameObject enemy;

        int otherIndex = thisIndex + 1;
        if(otherIndex == m_totalPlayers)
        {
            otherIndex = 0;
        }

        Vector3 position = new Vector3(
            m_wallsPosition[otherIndex].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
            m_wallsPosition[otherIndex].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
            0f
            );
        
        enemy = Instantiate(m_enemy0, position, m_enemy0.transform.rotation);
        enemy.transform.localScale = Vector3.Scale(enemy.transform.localScale, new Vector3((float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers]));
    }
}
