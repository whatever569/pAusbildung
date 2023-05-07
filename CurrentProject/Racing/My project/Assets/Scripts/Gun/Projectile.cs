using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public virtual int Damage => 10;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<CustomTags>(out CustomTags customTags))
        {
            if (customTags.HasTag("Enemy"))
            {
                other.gameObject.GetComponent<Car>().TakeDamage(Damage, gameObject.name);
            }
        }
    }
}

