using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class cleanscript : MonoBehaviour
{
     public bool top = true;
     public bool bot = true;
     public bool left = true;
     public bool right = true;
    
    
    public bool boss = false ;
    public bool spawn = false ;

    public void init(bool t, bool b, bool l, bool r)
    {
        
        transform.GetChild(1).gameObject.SetActive(!t);
        transform.GetChild(2).gameObject.SetActive(!b);
        transform.GetChild(3).gameObject.SetActive(!l);
        transform.GetChild(4).gameObject.SetActive(!r);
    }
}
