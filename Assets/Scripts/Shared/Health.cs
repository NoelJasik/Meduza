using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHitPoints;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] deathSounds;
    [SerializeField] private GameObject thingToFlash;

    protected float currentHitPoints;
    protected virtual void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public void Update()
    {
        if (transform.position.y < -10)
        {
            Death();
        }
        
    }

    public virtual void ReceiveDamage(float dmg)
    {
        print(gameObject.name + " received " + dmg + " damage");
        SoundManager.Instance.PlayRandom(hurtSounds);
        currentHitPoints -= dmg;
        if (currentHitPoints <= 0)
        {
            Death();
        }

        //Doesnt work with models now
        if(thingToFlash != null)
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        thingToFlash.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        thingToFlash.SetActive(true);
    }

    protected virtual void Death()
    {
        SoundManager.Instance.PlayRandom(deathSounds);
    }
}