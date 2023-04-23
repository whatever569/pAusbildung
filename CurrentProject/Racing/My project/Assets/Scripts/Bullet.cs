using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            //TODO: Add damage to enemy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}