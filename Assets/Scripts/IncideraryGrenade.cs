using UnityEngine;

public class IncideraryGrenade : MonoBehaviour
{
    [SerializeField] GameObject grenadeEffect;
    [SerializeField] GameObject fireEffect;
    public float throwDelay; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void IncideraryGrenadeThrown()
    {
        Instantiate(grenadeEffect, transform.position, transform.rotation);
        Instantiate(fireEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
