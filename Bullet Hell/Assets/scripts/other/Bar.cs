using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform front;
    RectTransform back;
    void Start()
    {
        front = transform.Find("front").GetComponent<RectTransform>();
        back = transform.Find("back").GetComponent<RectTransform>();
    }

    public void ResizeBar(float val){
        val = Math.Clamp(val,0,1);
        Debug.Log($"value = {val}");
        front.localScale = new Vector3(val,1,1);
    }
}
