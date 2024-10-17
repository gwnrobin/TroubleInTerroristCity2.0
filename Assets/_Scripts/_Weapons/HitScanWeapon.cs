using Demo.Scripts.Runtime.Item;
using HQFPSTemplate.Surfaces;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    private GunSettings.RayShooting _raycastData;

    protected ItemProperty m_FireModes;
/*
    // Returns the aim point by default
    public virtual Transform GetAimPoint()
    {
        //return generalInfo.weaponTransformData.aimPoint;
    }

    public override void Initialize(EquipmentHandler eHandler)
    {
        base.Initialize(eHandler);

        //_raycastData = (EquipmentInfo as HitScanWeaponInfo)?.projectile;
    }


    public override void Shoot(Ray[] itemUseRays)
    {
        //base.Shoot(itemUseRays);

        // The points in space that this gun's bullets hit
        Vector3[] hitPoints = new Vector3[_raycastData.RayCount];

        Debug.DrawRay(itemUseRays[0].origin, itemUseRays[0].direction, Color.yellow, 5f);

        //Raycast Shooting with multiple rays (e.g. Shotgun)
        if (_raycastData.RayCount > 1)
        {
            for (int i = 0; i < _raycastData.RayCount; i++)
                hitPoints[i] = DoHitscan(itemUseRays[i]);
        }
        else
            //Raycast Shooting with one ray
            hitPoints[0] = DoHitscan(itemUseRays[0]);

        //FireHitPoints.Send(hitPoints);
    }

    public override float GetUseRaySpreadMod()
    {
        return _raycastData.RaySpread * _raycastData.SpreadOverTime.Evaluate(EHandler.ContinuouslyUsedTimes / (float)MagazineSize);
    }

    public override int GetUseRaysAmount()
    {
        return _raycastData.RayCount;
    }
    */
    protected Vector3 DoHitscan(Ray itemUseRay)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(itemUseRay, out hitInfo, _raycastData.RayImpact.MaxDistance, _raycastData.RayMask, QueryTriggerInteraction.Collide))
        {
            float impulse = _raycastData.RayImpact.GetImpulseAtDistance(hitInfo.distance);

            // Apply an impact impulse
            if (hitInfo.rigidbody != null)
                hitInfo.rigidbody.AddForceAtPosition(itemUseRay.direction * impulse, hitInfo.point, ForceMode.Impulse);

            // Get the damage amount
            float damage = _raycastData.RayImpact.GetDamageAtDistance(hitInfo.distance);

            Debug.DrawRay(itemUseRay.origin, itemUseRay.direction * 100, Color.yellow, 2, false);
            //var damageInfo = new DamageInfo(-damage, DamageType.Bullet, hitInfo.point, itemUseRay.direction, impulse * _raycastData.RayCount, hitInfo.normal, Player, hitInfo.transform);

            // Try to damage the Hit object
            //Player.DealDamage.Try(damageInfo, null);
            SurfaceManager.SpawnEffect(hitInfo, SurfaceEffects.BulletHit, 1f);
        }
        else
            hitInfo.point = itemUseRay.GetPoint(10f);

        return hitInfo.point;
    }
}

