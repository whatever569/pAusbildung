using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    //the seperation between the gunturret and the gun's top is done so that the y axis rotation doesnt affect the x axis rotation
    //The hierarchy is as follows: GunTurret -> GunTop, xAxisRotationLimit

    #region Variables
    [Header("Gun")]
    [SerializeField] Transform _xAxisRotationLimitTransform, _carTopTransform, _gunTopTransform;
    [SerializeField] float _xGunRotationSpeed, _yGunRotationSpeed;
    Transform _transform;
    [SerializeField] Transform _firePoint;
    Rigidbody _rigidbody;

    [Header("Bullet")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed, _bulletLifeTime;
    float _xGunRotationControl, _yGunRotationControl, _xAxisRotationRange;
    #endregion

    #region 
    void Awake()
    {

        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();

        XAxisRotationLimitCalculator();

    }

    void Update()
    {
        GetUserInput();
        GunRotation();
        GunFire();

    }
    #endregion
    #region Methods
    void GunRotation()
    {
        transform.Rotate(Vector3.up * _yGunRotationControl * _yGunRotationSpeed * Time.deltaTime);

        // calculate the new xaxis rotation angle for the gun top
        float newGunTopXRotation = _gunTopTransform.localEulerAngles.x + _xGunRotationControl * _xGunRotationSpeed * Time.deltaTime;

        // adjust the new angle to be within the range of -180 to 180 degrees, solving angle wrap around issues
        if (newGunTopXRotation > 180) newGunTopXRotation -= 360;

        // clamp the new X-axis rotation angle within the specified range
        float clampedGunTopXRotation = Mathf.Clamp(newGunTopXRotation, 90 - _xAxisRotationRange, 90);

        // Apply the clamped rotation angle to the gun top
        _gunTopTransform.localRotation = Quaternion.Euler(clampedGunTopXRotation, _gunTopTransform.localEulerAngles.y, _gunTopTransform.localEulerAngles.z);
    }

    void GetUserInput()
    {
        _xGunRotationControl = Input.GetAxis("Mouse Y");
        _yGunRotationControl = Input.GetAxis("Mouse X");
    }

    /*
    So the gun doesnt point directly down, limits the gun's X axis rotaion to 
    in between the sky and the xAxisRotationLimit by using the following equation
    180 - arctan(the distance from the gun to the car's top / it's distance from the xAxisRotationLimit)
    */

    void XAxisRotationLimitCalculator()
    {
        float _b1, _b2;

        _b1 = transform.localPosition.y - _carTopTransform.localPosition.y;
        _b2 = transform.localPosition.z - _xAxisRotationLimitTransform.localPosition.z;

        _xAxisRotationRange = 180 - Mathf.Atan(_b2 / _b1) * Mathf.Rad2Deg;
    }

    void GunFire()
    {
    
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            //make the bullet's name the same as the car's name
            bullet.name = transform.parent.name;
            bullet.GetComponent<Rigidbody>().velocity = _firePoint.forward * _bulletSpeed;
            Destroy(bullet, _bulletLifeTime);
        }


    }
    #endregion

}
