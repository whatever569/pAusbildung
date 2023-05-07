using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// protoobject for pickupable objects
/// </summary>
public class Pickupable : MonoBehaviour, IPickupable
{
    private void OnCollisionEnter(Collision other) {

        bool isPlayable = false;
        if(other.gameObject.TryGetComponent<CustomTags>(out CustomTags customTags))
        {
            isPlayable = customTags.HasTag("Playable");
        }

        if(isPlayable)
        {
            Car car = other.gameObject.GetComponent<Car>();
            OnPickUp(car);
            Destroy(gameObject);
        }
    }

    public virtual void OnPickUp(Car car)
    {
        
    }

}
