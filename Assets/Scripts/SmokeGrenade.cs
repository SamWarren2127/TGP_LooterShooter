using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeGrenade : MonoBehaviour
{
    [SerializeField] GameObject smokeEffect;
    public float smokeDuration;
    public float throwDelay;
    MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SmokeGrenadeThrown(smokeDuration);
        }
    }

    void SmokeGrenadeThrown (float duration)
    {
        GameObject smokeeffect = Instantiate(smokeEffect, transform.position, transform.rotation);
        renderer.enabled = false;
        Destroy(gameObject, duration);
    }
}
