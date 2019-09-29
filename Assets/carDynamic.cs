using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carDynamic : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10f;
    public float jump = 10f;
    public float rotate = 2f;
    public bool forwardWheel = true;
    public bool onGround = true;
    public float xTrue ;
    public float zTrue ;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        Forward();
        Rotate();
        Jump();
        RotationValues();
       
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private void Forward() {
        if (forwardWheel && RotationValues())
        {
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            transform.Translate(0, 0, vertical);
            
        }
    }
    public bool RotationValues()
    {
        xTrue = UnityEditor.TransformUtils.GetInspectorRotation(rb.transform).x;
        zTrue = UnityEditor.TransformUtils.GetInspectorRotation(rb.transform).z;
        return ((xTrue < 30) && (xTrue > -30) && (zTrue < 30) && (zTrue > -30));
    }

    private void Rotate()
    {
        if (System.Math.Abs(Input.GetAxis("Vertical")) > 0 && RotationValues())
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                transform.Rotate(0f, -rotate, 0f);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
               
                transform.Rotate(0f, rotate, 0f);
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
            {
                transform.Rotate(0f, rotate, 0f);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {

                transform.Rotate(0f, -rotate, 0f);
            }
        }
    }
    private void Jump()
    {
        if ((Input.GetButtonDown("Jump")) && onGround && RotationValues())
        {
            onGround = false;
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            forwardWheel = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        forwardWheel = true;
        if (collision.gameObject.name == "water")
        {
            forwardWheel = false;
            print("We're on " + collision.gameObject.name);
        }
         
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        }
    }
    //private void Flip()
    //{
    //    timer += Time.deltaTime;
    //    float seconds = timer % 60;
    //    if (seconds > 2)
    //    {
    //        print("DO SOMETHING");

    //    }
    //}
}
