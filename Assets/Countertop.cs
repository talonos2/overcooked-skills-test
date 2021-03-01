using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countertop : PlaceableArea
{
    private Carriable containedObject = null;
    private Collider collider;

    public override bool CanAccept(Carriable carriable)
    {
        return (containedObject == null);
    }

    public override void Recieve(Carriable carriable)
    {
        this.containedObject = carriable;
        carriable.transform.position = this.transform.position + Vector3.up * 1.15f;
        this.containedObject.SetColliderEnabled(true);
        this.collider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if our object left.
        if (this.containedObject)
        {
            if (this.containedObject.transform.parent != null)
            {
                this.containedObject = null;
                this.collider.enabled = true;
            }
        }
    }
}
