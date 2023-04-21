using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Catching : MonoBehaviour
{
    //if the ball in proximity to the player 
    //the user can press "keyCatchAndRelease" to catch the ball,
    //this happens by translating the ball to the positon
    //of a gameobject (ballPosGameObject) that is a child of the player gameobject.

    //The ball can be released by pressing "keyCatchAndRelease" again; 
    //that will leave the ball in its place,
    //or can be shot off by pressing "keyReleaseWithForce" 
    //ofcourse after catching it with "keyCatchAndRelease".

    [SerializeField] GameObject ballPosGameObject, ballGameObject;
    [SerializeField] KeyCode keyCatchAndRelease = KeyCode.K, 
            keyReleaseWithForce = KeyCode.I;
    [SerializeField] LayerMask ballLayerMask;
    [SerializeField] float proximityCircleRadius = 2f,
             defaultMass = 0.75f, NormalthrowingForce = 4f, MaxThrowingForce = 10f;
    //if left then the value should be 1, else -1.
    [SerializeField] int leftPlayerOrRight = 1;
    int count = 0; 
    //Check if the ball is caught to avoid weird behavior
    public static bool isCaught;
    bool BallIsInProximity, ballIsToBeCaught, isToRelease, isToReleaseWithForce;
    Transform transform;
    Rigidbody rigidbody; 

    void Awake() 
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        SetBallPosGameObject();
    }
    void Update()
    {
        Controls();
        isInProximity();
        MassController();
        if(ballIsToBeCaught) MoveBallToPos();
        else if(isToRelease) Release(NormalthrowingForce);
        else if(isToReleaseWithForce) Release(MaxThrowingForce);
        

    }
    void Controls()
    {
        if(Input.GetKeyDown(keyCatchAndRelease) && count == 0 && BallIsInProximity && !isCaught)
        {
            ballIsToBeCaught = true;
            isCaught = true;
            count++;
        }
        else if(Input.GetKeyDown(keyCatchAndRelease) && count == 1)
        {
            ballIsToBeCaught = false;
            isToRelease = true;
            isCaught = false;
            count--;
        }
        else if(Input.GetKeyDown(keyReleaseWithForce) && count == 1)
        {
            ballIsToBeCaught = false;
            isToReleaseWithForce = true;
            isCaught = false;
            count--;
        }
    }

    void MoveBallToPos()
    {
        ballGameObject.GetComponent<Transform>().position = 
            ballPosGameObject.GetComponent<Transform>().position;
        
        //change velocity of ball to match that of the player avoiding weird behavior.
        ballGameObject.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
    }

    void MassController()
    {
        //change the mass of the ball to zero so it 
        //doesn't affect the player's movement.
        if(ballIsToBeCaught)
            ballGameObject.GetComponent<Rigidbody>().mass = 0f;
        //change back after ball isn't caught
        else ballGameObject.GetComponent<Rigidbody>().mass = defaultMass;
    }
    void isInProximity()
    {
        Vector3 raycastDir = ballGameObject
            .GetComponent<Transform>().position - transform.position;

        BallIsInProximity = Physics.Raycast(transform.position, 
            raycastDir, proximityCircleRadius, ballLayerMask);
    }

    void SetBallPosGameObject()
    {
        ballPosGameObject.GetComponent<Transform>().Translate(new Vector3((transform.lossyScale.x + 
            ballGameObject.GetComponent<Transform>().lossyScale.x) / 2, 0, 0) * leftPlayerOrRight);
    }

    void Release(float Force)
    {
        ballGameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * leftPlayerOrRight * Force, ForceMode.Impulse);
        isToRelease = false;
        isToReleaseWithForce = false;
    }
    

}