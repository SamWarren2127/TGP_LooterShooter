using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeEffect : MonoBehaviour
{
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] GameObject grenadeEffect;
    MeshRenderer renderer;
    Rigidbody rigidbody;
    public float effectDelay;
    public float throwSpeed;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void throwEffect(Transform pos)
    {
        StartCoroutine(Throw(pos));
    }

    IEnumerator Throw(Transform pos)
    {
        if (grenadeEffect != null)
        {
            rigidbody.AddForce(pos.forward * throwSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(3);
            renderer.enabled = false;
            Instantiate(ExplosionEffect, transform.position, transform.rotation);
            Instantiate(grenadeEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (ExplosionEffect != null)
        {
            rigidbody.AddForce(pos.forward * throwSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(3);
            renderer.enabled = false;
            Instantiate(ExplosionEffect, transform.position, transform.rotation);

        }
        else
        {
            rigidbody.AddForce(pos.forward * throwSpeed, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
