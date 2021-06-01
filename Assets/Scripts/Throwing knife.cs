using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwingknife : MonoBehaviour
{
    public GameObject knifeModel;
    public Transform armLocation;
    public float throwSpeed;
    public float ammo;

    bool throwing; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Throw()
    {
        GameObject knifethrown = Instantiate(knifeModel, armLocation.position, knifeModel.transform.rotation);
        Rigidbody rigidbody = knifethrown.GetComponent<Rigidbody>();
        rigidbody.AddForce(armLocation.forward * throwSpeed, ForceMode.Impulse);
    }
}
