using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private Transform normalObj;
    
    private Vector3 normal;

    private void Start()
    {
        normal = (transform.position - normalObj.position).normalized;
    }

    public void Reflect(Projectile projectile)
    {
        projectile.Direction = Vector3.Reflect(projectile.Direction, normal);
    }
}
