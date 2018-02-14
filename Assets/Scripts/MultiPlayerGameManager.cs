using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiPlayerGameManager : MonoBehaviour {

    public int m_totalPlayers;

    private Vector2[] m_wallsPosition;
    private Vector2 m_wallSize;

    [SerializeField] private GameObject m_wall0;
    [SerializeField] private GameObject m_wall1;
    [SerializeField] private GameObject m_enemy0;
    [SerializeField] private GameObject m_PickUpObj;
    [SerializeField] private GameObject m_playerObj;

    //Sounds
    private AudioSource m_music;
    private AudioSource m_soundEffect;

    // Use this for initialization
    void Start () {
        m_wallsPosition = new Vector2[m_totalPlayers];

        GenerateWalls(m_totalPlayers);

        for(int i = 0; i < m_totalPlayers; i++)
        {
            GeneratePointsPickUp(i);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("XboxStartButton"))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
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
        GameObject pickUp = m_PickUpObj;

        Vector3 position = new Vector3(
            m_wallsPosition[index].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
            m_wallsPosition[index].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
            0f
            );
        
        Instantiate(pickUp, position, Quaternion.identity).GetComponent<MultiPlayerPointsPickUp>().SetIndex(index);
    }

    public void SpawnEnemy(int thisIndex)
    {
        int otherIndex = thisIndex + 1;

        if(otherIndex == m_totalPlayers)
        {
            otherIndex = 0;
        }

        GameObject enemy = m_enemy0;
        Vector3 position = new Vector3(
            m_wallsPosition[otherIndex].x + Random.Range(-m_wallSize.x / 2, m_wallSize.x / 2),
            m_wallsPosition[otherIndex].y + Random.Range(-m_wallSize.y / 2, m_wallSize.y / 2),
            0f
            );
        
        Instantiate(enemy, position, m_enemy0.transform.rotation);
    }
}
