using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCanvas : MonoBehaviour {

    [SerializeField] private GameObject m_onCanvas;
    [SerializeField] private GameObject m_offCanvas;
    [SerializeField] private GameObject m_firstObject;

    public void Switch()
    {
        m_offCanvas.SetActive(false);
        m_onCanvas.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(m_firstObject, null);
    }
}
