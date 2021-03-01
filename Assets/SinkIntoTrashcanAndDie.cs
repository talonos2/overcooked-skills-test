using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkIntoTrashcanAndDie : MonoBehaviour
{
    Renderer renderer;
    float deathTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.renderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.deathTime += Time.deltaTime;
        if (deathTime > 1)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Time.deltaTime * .25f, this.transform.position.z);
        this.renderer.material.color = new Color(this.renderer.material.color.r, this.renderer.material.color.g, this.renderer.material.color.b, 1 - deathTime);
    }
}
