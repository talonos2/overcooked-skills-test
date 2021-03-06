﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carriable : Interactable
{
    private Collider collider;

    // Start is called before the first frame update
    protected void Start()
    {
        this.collider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetColliderEnabled(bool v)
    {
        this.collider.enabled = v;
    }

    public abstract bool CanBePickedUpBy(ChefInputManager chef);
}
