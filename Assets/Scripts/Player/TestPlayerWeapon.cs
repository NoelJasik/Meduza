using UnityEngine;

public class TestPlayerWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float damage;
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(hit.point, damage, 
            projectileSpeed, LayerMask.NameToLayer("PlayerProjectile"));
    }
}
