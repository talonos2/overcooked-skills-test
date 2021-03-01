using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

[System.Serializable]
public class IngredientData
{
    public Color color;
}
