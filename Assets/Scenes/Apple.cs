using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < bottomY) {

            string tag = this.gameObject.tag;
            Destroy( this.gameObject );

            //remove all other apples
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();

            apScript.FruitMissed(tag);
        }

        
    }

    
}
