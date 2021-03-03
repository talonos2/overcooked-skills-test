﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Ingredient : Carriable
{
    public IngredientData data;

    private Renderer renderer;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        this.renderer = this.GetComponent<Renderer>();
        this.renderer.material = new Material(this.renderer.material);
    }

    public override bool CanBePickedUpBy(ChefInputManager chef)
    {
        return chef.GetNumberOfItemsCarried() < 2;
    }
}

[System.Serializable]
public class IngredientData
{

    public Color color;

    public override bool Equals(object obj)
    {
        IngredientData data = obj as IngredientData;
        return data != null && color.r == data.color.r && color.g == data.color.g && color.b == data.color.b;
    }
}
