﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnodeSetUp : MonoBehaviour
{
    public GameObject Enode;
    public GameObject[] Enodes = new GameObject[4];
    public GameObject Dnode;
    public GameObject[] Dnodes = new GameObject[4];


    private void Awake()
    {
        Debug.Log("Awake");
        //Coords for Lewis' Scene
        //Vector3 pos = new Vector3(-30, 1, -20);
        //GameObject m_node = Instantiate(Enode);
        //m_node.transform.position = pos;
        //Enodes[0] = m_node;

        //pos = new Vector3(30, 1, -20);
        //m_node = Instantiate(Enode);
        //m_node.transform.position = pos;
        //Enodes[1] = m_node;

        //pos = new Vector3(30, 1, 20);
        //m_node = Instantiate(Enode);
        //m_node.transform.position = pos;
        //Enodes[2] = m_node;

        //pos = new Vector3(-30, 1, 20);
        //m_node = Instantiate(Enode);
        //m_node.transform.position = pos;
        //Enodes[3] = m_node;


        Vector3 pos = new Vector3(-127, -77, 170);
        GameObject m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[0] = m_node;

        pos = new Vector3(-127, -77, 270);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[1] = m_node;

        pos = new Vector3(40, -77, 270);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[2] = m_node;

        pos = new Vector3(40, -77, 170);
        m_node = Instantiate(Enode);
        m_node.transform.position = pos;
        Enodes[3] = m_node;


        //Driving Points
        pos = new Vector3(-85, -77, 118);
        m_node = Instantiate(Dnode);
        m_node.transform.position = pos;
        Dnodes[0] = m_node;

        pos = new Vector3(-85, -77, 157);
        m_node = Instantiate(Dnode);
        m_node.transform.position = pos;
        Dnodes[1] = m_node;

        pos = new Vector3(3, -77, 157);
        m_node = Instantiate(Dnode);
        m_node.transform.position = pos;
        Dnodes[2] = m_node;

        pos = new Vector3(3, -77, 118);
        m_node = Instantiate(Dnode);
        m_node.transform.position = pos;
        Dnodes[3] = m_node;
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
