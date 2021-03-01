using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Carriable
{
    private List<IngredientData> ingredients;
    private MeshRenderer renderer;

    [SerializeField]
    private float sizeScalar = .4f;

    // Start is called before the first frame update
    void Start()
    {
        this.renderer = this.GetComponent<MeshRenderer>();
        this.renderer.material = new Material(this.renderer.material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddIngredient(IngredientData newIngredient)
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
        this.transform.localScale = new Vector3(size, size, size)*sizeScalar;
    }
}
