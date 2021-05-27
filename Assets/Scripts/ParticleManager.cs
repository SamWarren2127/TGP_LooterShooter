using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private List<GameObject> ParticleEffects = new List<GameObject>();
    [SerializeField] GameObject healingEffect;

    // Start is called before the first frame update
    void Start()
    {
        //ParticleEffects.Add(null);
        ParticleEffects.Add(healingEffect);
    }

    public void SpawnParticleEffect(string _particleEffect, Vector3 _position, Quaternion _rotation)
    {
        GameObject effectInstance = Instantiate(GetEffectByString(_particleEffect), _position, _rotation);
        Debug.Log(effectInstance + "Instantiated");
        Destroy(effectInstance, 5.0f);
    }

    public GameObject GetEffectByString(string _particleEffect)
    {
        foreach(GameObject effect in ParticleEffects)
        {
            if(effect.name == _particleEffect)
            {
                return effect;
            }
        }

        Debug.Log("Could not find effect by string");
        return null;
    }
}
