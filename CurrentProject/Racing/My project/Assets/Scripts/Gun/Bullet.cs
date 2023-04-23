using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Car;
public class Bullet : MonoBehaviour
{

    public static int damage = 10;
   private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            //send bullet origin name through take damage 
            other.gameObject.GetComponent<Car>().TakeDamage(damage, gameObject.name);
        }
    }
}
