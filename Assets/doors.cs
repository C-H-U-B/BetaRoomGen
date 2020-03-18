using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour
{
    public bool  top = true;
    public bool  bot = true;
    public bool  left = true;
    public bool right = true;

    public GameObject topdoor;
    public GameObject botdoor;
    public GameObject leftdoor;
    public GameObject rightdoor;
    public GameObject None;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!left)
        {
            leftdoor = None;
        }
        if (!right)
        {
            rightdoor = None;
        }
        if (!bot)
        {
            botdoor = None;
        }
        if (!top)
        {
            topdoor = None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
