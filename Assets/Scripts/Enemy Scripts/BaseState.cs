using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{

    public BaseState(GameObject gameObject)
    {
        this.m_gameObject = gameObject;
        this.m_transform = gameObject.transform;
    }
    protected GameObject m_gameObject;
    protected Transform m_transform;

    public abstract Type Tick();
    
}
