using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    //public RigidBody myRigidBody;

    public Rigidbody myRigidBody;


    private bool readyToPerformThrow = false;
    private bool hasThrown = false;

    private Vector3 startingDirection;
    private Vector3 startingPosition;

    public AudioSource diceAudioSource;

    public AudioClip hitSound;
    public AudioClip resultSound;
    public AudioClip resultSoundD20;
    public AudioClip throwSound;
    public GameObject resuldEffectD20;

    private bool isRolling = false;
    private bool finishedRolling = false;
    private bool hasProcessedDiceResult = false;

    private bool readyForVelocityCheck = false;

    public Transform my20Evaluator;

    public GameObject hitEffectPrefab;
    public GameObject hitEffectPrefabBig;

    // Start is called before the first frame update
    void Start()
    {
        startingDirection = transform.forward;
        startingPosition = transform.position;

        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

        //StartCoroutine(CheckForMagitudeDelay());
    }

    //Effects 
    //First Two hits = dirt puff & sound
    
    void OnCollisionEnter(Collision collision)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        // Play a sound if the colliding objects had a big impact.
        if (collision.relativeVelocity.magnitude > 3)
        {
            

            if (collision.relativeVelocity.magnitude > 8)
            {
                Instantiate(hitEffectPrefabBig, collision.GetContact(0).point, this.transform.rotation,null);
            }
            else
            {
                Instantiate(hitEffectPrefab, collision.GetContact(0).point, this.transform.rotation,null);
            }


            

            if (collision.relativeVelocity.magnitude > 2)
            {
                diceAudioSource.PlayOneShot(hitSound);
            }
            
        }
            
    }


    void FixedUpdate()
    {
        if (isRolling == true && readyForVelocityCheck == true)
        {

            //Eval current velolicty - if < min FInished rolling = true
            if (myRigidBody.velocity.magnitude < 0.01f && myRigidBody.angularVelocity.magnitude < 0.01f)
            {
                finishedRolling = true;
            }

            if (finishedRolling == true)
            {

                if (hasProcessedDiceResult == false)
                {
                    hasProcessedDiceResult = true;
                    PerformResultAnalysis();
                }

            }

        }


    }

    private void PerformResultAnalysis()
    {
        Debug.Log("Dice has finished rolling!");

        if(my20Evaluator.transform.position.y > 2.1f)
        {
            Debug.Log("A D20 was rolled!");
            diceAudioSource.PlayOneShot(resultSoundD20);
            Instantiate(resuldEffectD20, this.transform.position, this.transform.rotation);
            //DO Fancy Effect
        }
        else
        {
            Debug.Log("A D20 was not rolled!");
        }
    }

    public void PerformThrow()
    {
        if (hasThrown == false)
        {

            hasThrown = true;
            // Throw
            myRigidBody.AddForce((startingDirection * 10), ForceMode.Impulse);

            //Torque
            Vector3 randomTorque = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
            myRigidBody.AddTorque(randomTorque * Random.Range(1, 50), ForceMode.Impulse);

            // Gravity
            myRigidBody.useGravity = true;

            isRolling = true;

            diceAudioSource.PlayOneShot(throwSound);

            StartCoroutine(CheckForMagitudeDelay());

        }

    }

    public void PerformReset()
    {
        //Physics
        myRigidBody.useGravity = false;
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.angularVelocity = Vector3.zero;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        transform.position = startingPosition;
        
        //Bools
        hasThrown = false;
        isRolling = false;
        finishedRolling = false;
        hasProcessedDiceResult = false;
        readyForVelocityCheck = false;
    }


    IEnumerator CheckForMagitudeDelay()
    {
        yield return new WaitForSeconds(0.1f);
        readyForVelocityCheck = true;
    }

}
