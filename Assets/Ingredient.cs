using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Carriable
{
    public IngredientData data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class IngredientData
{
    public Color color;
}
