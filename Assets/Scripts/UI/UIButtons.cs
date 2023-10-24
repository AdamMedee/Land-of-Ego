using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            transform.localScale = new Vector3(1, 1, 0.3f);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector3(1, 1, 0.6f);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 0.6f);
        }
    }

    void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
