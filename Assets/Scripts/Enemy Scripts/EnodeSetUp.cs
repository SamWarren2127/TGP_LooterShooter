using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnodeSetUp : MonoBehaviour
{
    public GameObject Enode;
    public GameObject[] Enodes = new GameObject[4];
    public GameObject Dnode;
    private GameObject[] Dnodes;


    private void Awake()
    {
        Debug.Log("Awake");
        Vector3 pos = new Vector3(-30, 1, -20);
        GameObject m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[0] = m_node;

        pos = new Vector3(30, 1, -20);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[1] = m_node;

        pos = new Vector3(30, 1, 20);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[2] = m_node;

        pos = new Vector3(-30, 1, 20);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[3] = m_node;
    }


    //// Start is called before the first frame update
    //void Start()
    //{
        

    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
