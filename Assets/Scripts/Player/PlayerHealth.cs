using UnityEngine;

public class PlayerHealth : Health
{
    private Vector3 startPos;
    [SerializeField] private Animator anim;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    protected override void Death()
    {
        anim.Play("Die");
        SceneSwitcher.Instance.ReloadScene();
        //Respawn();
    }

    private void Respawn()
    {
        currentHitPoints = maxHitPoints;
        transform.position = startPos;
    }

    public float GetCurrentHealth()
    {
        return currentHitPoints;
    }
    
    public float GetMaxHealth()
    {
        return maxHitPoints;
    }

}