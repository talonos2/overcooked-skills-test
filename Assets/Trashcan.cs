using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : PlaceableArea
{
    public override bool CanAccept(Carriable carriable)
    {
        return true;
    }

    public override void Recieve(Carriable carriable)
    {
        carriable.gameObject.AddComponent<SinkIntoTrashcanAndDie>();
        carriable.transform.position = this.transform.position + Vector3.up;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
