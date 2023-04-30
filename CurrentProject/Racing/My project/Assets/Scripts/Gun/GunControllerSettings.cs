using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GunControllerSettings
{
    public Transform XAxisRotationLimitTransform, CarTopTransform, GunTopTransform;
    public float XGunRotationSpeed, YGunRotationSpeed;
    public Transform Transform;
    public Transform FirePoint;
    public Rigidbody Rigidbody;
    public GameObject Parent;
    public GameObject BulletPrefab;
    public float XGunRotationControl, YGunRotationControl, XAxisRotationRange;

    public GunControllerSettings(Transform xAxisRotationLimitTransform, Transform carTopTransform, Transform gunTopTransform, float xGunRotationSpeed, float yGunRotationSpeed, Transform transform, Transform firePoint, Rigidbody rigidbody, GameObject parent, GameObject bulletPrefab, float xGunRotationControl, float yGunRotationControl, float xAxisRotationRange)
    {
        XAxisRotationLimitTransform = xAxisRotationLimitTransform;
        CarTopTransform = carTopTransform;
        GunTopTransform = gunTopTransform;
        XGunRotationSpeed = xGunRotationSpeed;
        YGunRotationSpeed = yGunRotationSpeed;
        Transform = transform;
        FirePoint = firePoint;
        Rigidbody = rigidbody;
        Parent = parent;
        BulletPrefab = bulletPrefab;
        XGunRotationControl = xGunRotationControl;
        YGunRotationControl = yGunRotationControl;
        XAxisRotationRange = xAxisRotationRange;
    }
}
