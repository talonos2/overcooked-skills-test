using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableArea : Interactable
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract bool CanAccept(Carriable carriable);

    public abstract void Recieve(Carriable carriable, ChefInputManager chef);
}
