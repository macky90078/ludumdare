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
        //Apply scale
        other.gameObject.transform.localScale = Vector3.Scale(other.transform.localScale, new Vector3(m_scale, m_scale, m_scale));

        //player
        if (other.tag == "Player")
        {
            m_player = other.gameObject;
            other.gameObject.GetComponent<CharacterMovement>().ScaleRegTimeToDist(1.1f / m_scale);
        }

        //Enemy
        if(other.tag == "Enemy0")
        {
            other.gameObject.GetComponent<Enemy0>().ScaleTimeToDist(1.1f / m_scale);
        }
        else if (other.tag == "ChaserEnemy")
        {
            other.gameObject.GetComponent<SeekerEnemy>().m_target = m_player;
            other.gameObject.GetComponent<SeekerEnemy>().ScaleForce(m_scale);
        }
        else if (other.tag == "DasherEnemy")
        {
            other.gameObject.GetComponent<DasherEnemy>().m_target = m_player;
        }
    }
}
