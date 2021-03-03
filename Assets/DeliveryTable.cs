using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTable : PlaceableArea
{
    public Salad saladPrefab;
    public float rotationSpeed = 36;
    public GameObject successParticles;
    public GameObject failParticles;


    private Salad saladOnDisplay;
    private List<List<IngredientData>> salads = new List<List<IngredientData>>();
    private float rotation;


    // Start is called before the first frame update
    void Start()
    {
        GenerateSalads();
        rotation = UnityEngine.Random.value * 360;
        PickANewSalad();
    }

    private void PickANewSalad()
    {
        saladOnDisplay = GameObject.Instantiate<Salad>(saladPrefab);
        saladOnDisplay.Init();
        saladOnDisplay.SetColliderEnabled(false);
        int random = UnityEngine.Random.Range(0, salads.Count);
        foreach (IngredientData ingredient in salads[random])
        {
            saladOnDisplay.AddIngredient(ingredient);
        }
        saladOnDisplay.transform.position = this.transform.position + Vector3.up * 1.2f;
    }

    private void GenerateSalads()
    {
        IngredientData r = new IngredientData();
        r.color = new Color(1, 0, 0);
        IngredientData g = new IngredientData();
        g.color = new Color(0, 1, 0);
        IngredientData b = new IngredientData();
        b.color = new Color(0, 0, 1);
        IngredientData e = new IngredientData();
        e.color = new Color(1, 1, 1);
        IngredientData y = new IngredientData();
        y.color = new Color(.5f, .5f, .5f);
        IngredientData k = new IngredientData();
        k.color = new Color(0, 0, 0);
        salads.Add(new List<IngredientData>(new IngredientData[] { r, g }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, b }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, e }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, y }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, b }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, e }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, y }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { b, e }));
        salads.Add(new List<IngredientData>(new IngredientData[] { b, y }));
        salads.Add(new List<IngredientData>(new IngredientData[] { b, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { e, y }));
        salads.Add(new List<IngredientData>(new IngredientData[] { y, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, g, b, y, e, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { r, b, k }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, b, e }));
        salads.Add(new List<IngredientData>(new IngredientData[] { g, r, y }));
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime * rotationSpeed;
        saladOnDisplay.transform.localRotation = Quaternion.Euler(0, rotation, 90);
    }

    public override bool CanAccept(Carriable carriable)
    {
        return (carriable is Salad);
    }

    public override void Recieve(Carriable carriable, ChefInputManager chef)
    {
        if (((Salad)carriable).Equals(saladOnDisplay))
        {
            GameObject go = GameObject.Instantiate(successParticles);
            go.transform.position = this.transform.position;
            GameObject.Destroy(saladOnDisplay.gameObject);
            PickANewSalad();
        }
        else
        {
            GameObject go = GameObject.Instantiate(failParticles);
            go.transform.position = this.transform.position;
        }
        GameObject.Destroy(carriable.gameObject);
    }
}
