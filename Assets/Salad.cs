﻿using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Carriable
{
    private List<IngredientData> ingredients = new List<IngredientData>();
    private MeshRenderer renderer;

    [SerializeField]
    private Vector3 sizeScalar = new Vector3(.4f, .05f, .4f);

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    internal bool ContainsIngredient(Ingredient ingredient)
    {
        return ingredients.Contains(ingredient.data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        this.renderer = this.GetComponent<MeshRenderer>();
        this.renderer.material = new Material(this.renderer.material);
        base.Start();
    }

    public void AddIngredient(IngredientData newIngredient)
    {
        ingredients.Add(newIngredient);
        float r = 0;
        float g = 0;
        float b = 0;
        foreach (IngredientData i in ingredients)
        {
            r += i.color.r;
            g += i.color.g;
            b += i.color.b;
        }

        this.renderer.material.color = new Color(r / ingredients.Count, g / ingredients.Count, b / ingredients.Count);
        float size = Mathf.Pow(ingredients.Count, 1f / 3f);
        this.transform.localScale = new Vector3(size * sizeScalar.x, size*sizeScalar.y, size * sizeScalar.z);
    }

    public override bool Equals(object obj)
    {
        Salad s = obj as Salad;
        if (!s)
        {
            return false;
        }
        Color a = this.renderer.material.color;
        Color b = s.renderer.material.color;
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }

    internal Color GetSaladColor()
    {
        return renderer.material.color;
    }

    public override bool CanBePickedUpBy(ChefInputManager chef)
    {
        return chef.GetNumberOfItemsCarried() < 1;
    }
}
