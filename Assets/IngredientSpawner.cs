using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public Ingredient ingredientToSpawn;
    public float timeItTakesToSpawnAnIngredient = 3;


    private Ingredient ingredientImHolding;
    private float timeUntilNextIngredient = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ingredientImHolding == null || ingredientImHolding.transform.parent != null)
        {
            timeUntilNextIngredient = 3;
            ingredientImHolding = GameObject.Instantiate<Ingredient>(ingredientToSpawn);
            ingredientImHolding.transform.position = this.transform.position + Vector3.down * .5f;
            ingredientImHolding.Init();
        }

        timeUntilNextIngredient -= Time.deltaTime;
        if (timeUntilNextIngredient > 0)
        {
            float t = timeUntilNextIngredient / timeItTakesToSpawnAnIngredient;
            ingredientImHolding.transform.position = this.transform.position + Vector3.down * (.5f-(1-t));
            ingredientImHolding.GetComponent<Renderer>().material.color = new Color(ingredientImHolding.data.color.r, ingredientImHolding.data.color.g, ingredientImHolding.data.color.b, 1-t);
        }
    }
}
