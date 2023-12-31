using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float shootDistance = 4f;
    [SerializeField]
    private float gunDmg = 1f;
    [SerializeField]
    private ParticleSystem shootPS;
    [SerializeField]
    private GameObject primaryWpn;
    [SerializeField]
    private GameObject secondaryWpn;
    [SerializeField]

    private float health;
    
    private GunController gunController;
    private Rigidbody mRb;
    private Vector2 mDirection;
    private Vector2 mDeltaLook;
    private Transform cameraMain;
    private GameObject debugImpactSphere;
    private GameObject bloodObjectParticles;
    private GameObject otherObjectParticles;
    private bool primaryEquipped;

    private void Start()
    {
        mRb = GetComponent<Rigidbody>();
        cameraMain = transform.Find("Main Camera");

        debugImpactSphere = Resources.Load<GameObject>("DebugImpactSphere");
        bloodObjectParticles = Resources.Load<GameObject>("BloodSplat_FX Variant");
        otherObjectParticles = Resources.Load<GameObject>("GunShot_Smoke_FX Variant");

        Cursor.lockState = CursorLockMode.Locked;

        gunController = secondaryWpn.GetComponent<GunController>();
        primaryEquipped = false;
        UpdateGunStats();
    }

    private void FixedUpdate()
    {
        mRb.velocity = mDirection.y * speed * transform.forward 
            + mDirection.x * speed * transform.right;

        transform.Rotate(
            Vector3.up,
            turnSpeed * Time.deltaTime * mDeltaLook.x
        );
        cameraMain.GetComponent<CameraMovement>().RotateUpDown(
            -turnSpeed * Time.deltaTime * mDeltaLook.y
        );

    }

    private void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        mDeltaLook = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            Shoot();
        }
    }

    private void OnSwitch(InputValue value)
    {
        if(primaryEquipped){
            gunController = secondaryWpn.GetComponent<GunController>();
        }else
        {
            gunController = primaryWpn.GetComponent<GunController>();
        }

        primaryWpn.SetActive(!primaryEquipped);
        secondaryWpn.SetActive(primaryEquipped);        
        primaryEquipped = !primaryEquipped;
       
        UpdateGunStats();
    }

    private void Shoot()
    {
        shootPS.Play();

        RaycastHit hit;
        if (Physics.Raycast(
            cameraMain.position,
            cameraMain.forward,
            out hit,
            shootDistance
        ))
        {
            if (hit.collider.CompareTag("Enemigos"))
            {
                var bloodPS = Instantiate(bloodObjectParticles, hit.point, Quaternion.identity);
                Destroy(bloodPS, 3f);
                var enemyController = hit.collider.GetComponent<EnemyController>();
                enemyController.TakeDamage(gunDmg);
            }else
            {
                var otherPS = Instantiate(otherObjectParticles, hit.point, Quaternion.identity);
                otherPS.GetComponent<ParticleSystem>().Play();
                Destroy(otherPS, 3f);
            }
            
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            // Fin del juego
            Debug.Log("Fin del juego");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemigo-Attack"))
        {
            Debug.Log("Player recibio danho");
            TakeDamage(1f);
        }
        
    }

    private void UpdateGunStats()
    {
        gunDmg = gunController.gunDmg;
        shootDistance = gunController.shootDistance;
        shootPS = gunController.shootPS;
    }

}
