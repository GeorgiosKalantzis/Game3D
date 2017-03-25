using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  
   public float healthpoints = 500f;

    private void FixedUpdate()
    {
        if (healthpoints < 0)
        {
            Destroy(gameObject);
        }
    }
    //public float Healthpoints
    //{
    //    get { return healthpoints; }

    //    set
    //    {
    //        healthpoints = value;

    //        if (healthpoints <= 0)
    //        {
    //            Destroy(gameObject);

    //        }
    //    }
    //}
}
