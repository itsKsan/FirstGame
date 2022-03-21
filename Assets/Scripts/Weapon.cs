using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float _nextShootTime;
    
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform weaponPos;
    [SerializeField] private float projectileVelocity = 10;
    
    

    [SerializeField] private float msBetweenShoot = 100;
    
    public void Shoot()
    {
        if (!((Time.time) > _nextShootTime)) return;

        _nextShootTime = Time.time + msBetweenShoot / 1000;
        var projectile = Instantiate(this.projectile, weaponPos.position, weaponPos.rotation);
        projectile.SetSpeed(projectileVelocity);
    }
}
