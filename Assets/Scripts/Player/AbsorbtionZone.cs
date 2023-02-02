using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbtionZone : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private Transform barrel;
    [SerializeField] private Transform hitPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by:" + other.name + " " + other.tag);
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().ReceiveDamage(PlayerCombat.ActualSwingDamage);
        }

        if (other.tag != "Projectile") 
        {
           return;
        }

        if (PlayerCombat.IsSwinging && other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
            projectile.GetComponent<Projectile>().Initialize(hitPoint.position, PlayerCombat.ActualReflectDamage , PlayerCombat.ActualProjectileDeflectSpeed, LayerMask.NameToLayer("PlayerProjectile"));
        }

        if (PlayerCombat.IsBlocking)
        {
            PlayerCombat.IsHoldingProjectile = true;
        }
        
        Destroy(other.gameObject);

    }
}
