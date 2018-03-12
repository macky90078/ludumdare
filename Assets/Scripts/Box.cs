using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    private GameObject m_player;
    private float m_scale;

	// Use this for initialization
	void Start () {
        m_scale = GameObject.Find("GameManager").GetComponent<MultiPlayerGameManager>().getEntityScale();
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.localScale = Vector3.Scale(other.transform.localScale, new Vector3(m_scale, m_scale, m_scale));

        //Set player
        if (other.tag == "Player")
        {
            m_player = other.gameObject;
        }

        //Set target
        if(other.tag == "ChaserEnemy")
        {
            other.gameObject.GetComponent<SeekerEnemy>().m_target = m_player;
        }
        if (other.tag == "DasherEnemy")
        {
            other.gameObject.GetComponent<DasherEnemy>().m_target = m_player;
        }
    }
}
