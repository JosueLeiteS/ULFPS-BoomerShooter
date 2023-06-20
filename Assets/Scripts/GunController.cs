using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public float gunDmg = 1f;
    [SerializeField]
    public float shootDistance = 4f;
    [SerializeField]
    public ParticleSystem shootPS;
    [SerializeField]

    void Start()
    {
        
    }

}
