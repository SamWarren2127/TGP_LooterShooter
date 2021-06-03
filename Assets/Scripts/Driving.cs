using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{

    GameObject[] m_Dnodes;
    
    private EnodeSetUp m_enodeSetUp;

    // Start is called before the first frame update
    void Start()
    {
        m_enodeSetUp = GameObject.FindGameObjectWithTag("ESetUp").GetComponent<EnodeSetUp>();
        m_Dnodes = m_enodeSetUp.Dnodes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
