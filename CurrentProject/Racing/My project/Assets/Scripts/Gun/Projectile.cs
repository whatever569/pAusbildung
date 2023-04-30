using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public virtual int Damage => 10;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //send bullet origin name through take damage
            other.gameObject.GetComponent<Car>().TakeDamage(Damage, gameObject.name);
        }
    }
}

