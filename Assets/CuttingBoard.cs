using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : PlaceableArea
{
    public Salad prototypeSalad;
    public float timeItTakesToChopAThing = 3;

    private float timeUntilChopFinishes = 0;

    private ChefInputManager capturedChef;
    private Ingredient capturedIngredient;
    private Collider collider;
    private Salad containedSalad = null;

    public override bool CanAccept(Carriable carriable)
    {
        return (carriable is Ingredient && capturedChef == null && (containedSalad == null || !containedSalad.ContainsIngredient((Ingredient)carriable)));
    }

    public override void Recieve(Carriable carriable, ChefInputManager chef)
    {
        this.capturedChef = chef;
        chef.ForceChop();
        timeUntilChopFinishes = timeItTakesToChopAThing;
        carriable.SetColliderEnabled(false);
        capturedIngredient = (Ingredient)carriable;
        capturedIngredient.transform.parent = null;
        capturedIngredient.transform.position = this.transform.position + Vector3.up;
        if (containedSalad!=null)
        {
            containedSalad.SetColliderEnabled(false);
        }
        collider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilChopFinishes > 0)
        {
            timeUntilChopFinishes -= Time.deltaTime;
            if (timeUntilChopFinishes <= 0)
            {
                capturedChef.ReleaseChop();
                capturedChef = null;
                collider.enabled = true;
                if (containedSalad != null)
                {
                    containedSalad.AddIngredient(capturedIngredient.data);
                    containedSalad.SetColliderEnabled(true);
                }
                else
                {
                    containedSalad = GameObject.Instantiate<Salad>(prototypeSalad);
                    containedSalad.transform.position = this.transform.position+Vector3.up;
                    containedSalad.Init();
                    containedSalad.AddIngredient(capturedIngredient.data);
                }
                GameObject.Destroy(capturedIngredient.gameObject);
            }
        }
        else
        {

            if (containedSalad != null && containedSalad.transform.parent != null)
            {
                this.containedSalad = null;
            }
        }
    }
}
