using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {
    [SerializeField]
    private int _lightHandler;
    private Renderer render;
    private Ray ray;

    public int LightHandler
    {
        get { return _lightHandler; }
        set { _lightHandler = value; }
    }

    void Awake()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (LightHandler == 0) render.material.color = Color.green;
        else render.material.color = Color.red;
    }

    void OnMouseUp()
    {
        if (LightHandler == 1) LightHandler = 0;
        else LightHandler = 1;
    }
}
