using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiPlayerGameManager : MonoBehaviour {

    public int m_totalPlayers;
    private int m_totalPlayersRemain;
    public int m_testTotalPlayer;
    private GameObject[] m_players;

    private int[] m_scores;

    private Vector2[] m_wallsPosition;
    private Vector2 m_wallSize;
    public Hashtable m_entityScale;

    [SerializeField] private GameObject m_player;
    [SerializeField] private GameObject m_wall0;
    [SerializeField] private GameObject m_wall1;
    [SerializeField] private GameObject m_enemy0;
    [SerializeField] private GameObject m_seekerEnemy;
    [SerializeField] private GameObject m_PickUpObj;
    [SerializeField] private GameObject m_cameraObj;

    [HideInInspector] public bool m_gameOver = false;

    //Sounds
    [SerializeField] private AudioClip m_DeathSound;
    private AudioSource m_music;
    private AudioSource m_soundEffect;

    //UI
    [SerializeField] private GameObject[] m_DeathScreenUI;
    [SerializeField] private GameObject[] m_InGameUI;

    // Use this for initialization
    void Awake () {
        //set total player
        //For test, add test total player.
        if (m_testTotalPlayer == 0)
        {
            m_totalPlayers = MenuManager.m_totalPlayer;
        }
        else
        {
            m_totalPlayers = m_testTotalPlayer;
        }
        m_totalPlayersRemain = m_totalPlayers;

        //Set scale
        m_entityScale = new Hashtable();
        m_entityScale.Clear();
        m_entityScale.Add(1, 1.0f);
        m_entityScale.Add(2, 0.9f);
        m_entityScale.Add(3, 0.7f);
        m_entityScale.Add(4, 0.7f);

        //Generate walls
        m_wallsPosition = new Vector2[m_totalPlayers];
        GenerateWalls(m_totalPlayers);

        //Create score array
        m_scores = new int[m_totalPlayers];

        //Generate players
        m_players = new GameObject[m_totalPlayers];
        for(int i = 0; i<m_totalPlayers; i++)
        {
            GameObject player;

            Vector3 position = new Vector3(
                m_wallsPosition[i].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
                m_wallsPosition[i].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
                0f
                );

            player = Instantiate(m_player, position, Quaternion.identity);
            player.GetComponent<CharacterMovement>().setPlayerNumber(i+1);
            player.GetComponent<CharacterMovement>().m_bIsSingleplayer = false;
            player.GetComponent<CharacterMovement>().m_bIsMultiplayer = true;
            player.GetComponent<CharacterMovement>().m_gameManagerMultiplayer = this;
            m_players[i] = player;
            //player.transform.localScale = Vector3.Scale(player.transform.localScale, new Vector3((float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers]));
            m_scores[i] = 0;
            m_InGameUI[i].gameObject.SetActive(true);
        }

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
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        if (m_gameOver)
        {
            for (int j = 0; j < m_totalPlayers - 1; j++)
            {
                for (int i = 0; i < m_totalPlayers - 1; i++)
                {
                    if (m_scores[i] < m_scores[i + 1])
                    {
                        int temp;
                        temp = m_scores[i];
                        m_scores[i] = m_scores[i + 1];
                        m_scores[i + 1] = temp;
                    }
                }
            }

            //Active death screen UI
            m_DeathScreenUI[0].SetActive(true);
            m_DeathScreenUI[1].SetActive(true);
            if (m_totalPlayers >= 3)
            {
                m_DeathScreenUI[2].SetActive(true);

                if (m_totalPlayers >= 4)
                {
                    m_DeathScreenUI[3].SetActive(true);
                }
            }

            //Set text
            for(int i = 0; i < m_totalPlayers; i++)
            {
                m_DeathScreenUI[i].transform.Find("Score").GetComponent<Text>().text = m_scores[i].ToString();
            }

            
            m_music.Pause();
            m_soundEffect.PlayOneShot(m_DeathSound);
            m_gameOver = false;
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

        int otherIndex;
        otherIndex = thisIndex + 1;
        if (otherIndex == m_totalPlayers)
        {
            otherIndex = 0;
        }

        while (!m_players[otherIndex].activeSelf)
        {
            otherIndex++;
            if (otherIndex == m_totalPlayers)
            {
                otherIndex = 0;
            }
        }

        Vector3 position = new Vector3(
            m_wallsPosition[otherIndex].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
            m_wallsPosition[otherIndex].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
            0f
            );
        
        enemy = Instantiate(m_enemy0, position, m_enemy0.transform.rotation);
        //enemy.transform.localScale = Vector3.Scale(enemy.transform.localScale, new Vector3((float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers], (float)m_entityScale[m_totalPlayers]));
    }

    public float getEntityScale()
    {
        return (float)m_entityScale[m_totalPlayers];
    }

    public void addScore(int index, int points)
    {
        m_scores[index] += points;

        int playerID = index + 1;
        m_InGameUI[index].GetComponent<Text>().text = "Player " + playerID + ": " + m_scores[index];
    }

    public void PlayerDie(int index)
    {
        m_soundEffect.PlayOneShot(m_DeathSound);
        m_totalPlayersRemain--;
        if(m_totalPlayersRemain == 1)
        {
            m_gameOver = true;
        }
    }
}
