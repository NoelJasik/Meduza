using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHitPoints;
    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private AudioClip[] deathSounds;

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

        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    protected virtual void Death()
    {
        SoundManager.Instance.PlayRandom(deathSounds);
    }
}