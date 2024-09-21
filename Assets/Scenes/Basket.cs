using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{

    public ScoreCounter scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find( "ScoreCounter" );

        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //get curr screen pos of the mouse from Input

        Vector3 mousePos2D = Input.mousePosition;

        //cams position sets how far to push the mouse into 3d
        //if this line casues a NullReferenceException select the Main Camera
        //in the Heirarchy and set its tag to MainCamera in the Inspector

        mousePos2D.z = -Camera.main.transform.position.z;

        //convert the point from 2d screen space into 3d game world space

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );

        //Move the x position of this basket to the x position of the Mouse

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll) {
        //find out what hit basket

        GameObject collidedWith = coll.gameObject;

        if( collidedWith.CompareTag("Apple") ) {

            Destroy( collidedWith );
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score );
        }

        if( collidedWith.CompareTag("Pear") ) {

            Destroy( collidedWith );
            scoreCounter.score -= 500;
        }
    }
}
