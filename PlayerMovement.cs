using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]Image boostbar;
    [SerializeField]private int maxSpeed;
    [SerializeField]private int boostSpeed;
    [SerializeField]private float rotationamount;
    [SerializeField]private float maxRotationSpeedX;
    [SerializeField]private float maxRotationSpeedY;
    [SerializeField]private float maxRotationZ;
    private Vector3 rotation = new Vector3(0, 0, 0);
    private Rigidbody _Rigidbody;   
    private float speed = 0;
    private float differentX;
    private float differentY;
    private bool upsideDown;
    private bool boost = false;
    private bool boostregen = false;
    private bool godown = false;
    private float boostcounter = 100;
    private float fillAmount;
    public Vector3 Rotation()
    {
        return new Vector3(rotation.x,rotation.y,0) ;
    }
    public float rotationZ()
    {
        return rotation.z;
    }
    public bool UpsideDown()
    {
        return upsideDown;
    }

    private void Awake ()
    {
        
        _Rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        print(Camera.main.pixelHeight);
        print(Camera.main.pixelWidth);
    }
    private void Update()
    {
        if (!boost && !boostregen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("start");
                speed = boostSpeed;
                boost = true;
            }
        }
        else if (boost && !boostregen)
        {
            boostcounter--;
            fillAmount = boostcounter / 100;
            boostbar.fillAmount = fillAmount;
            if (boostcounter <= 0)
            {
                speed = maxSpeed;
                boostcounter = 0;
                boostregen = true;
                print("stop");
            }
        }
        else if(boost && boostregen)
        {
            boostcounter++;
            fillAmount = boostcounter / 1000;
            boostbar.fillAmount = fillAmount;
            if (boostcounter >= 1000)
            {
                boostcounter = 100;
                boost = false;
                boostregen = false;
                print("stopr");
            }
               
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SpeedUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SlowDown();
        }
        GetRotation();
    }
    private void GetRotation()
    {
        if (Input.mousePosition.x != Camera.main.pixelWidth / 2)//left right
        {
            differentX = Camera.main.pixelWidth / 2 - Input.mousePosition.x;
            if (rotation.x >= 360)
            {
                rotation.x -= 360;
            }
            else if (rotation.x <= -360)
            {
                rotation.x += 360;
            }
            if (rotation.x > -90 && rotation.x < 90)
            {
                upsideDown = false;
            }
            else
            {
                upsideDown = true;
            }
            if (!upsideDown)
            {
                if (-differentX * rotationamount > maxRotationSpeedX)//moet voor rotation.y 
                {
                    rotation.y += maxRotationSpeedX;
                }
                else if (-differentX * rotationamount < -maxRotationSpeedX)
                {
                    rotation.y += -maxRotationSpeedX;
                }
                else
                {
                    rotation.y += -differentX * rotationamount;
                }
            }
            else
            {
                if (differentX * rotationamount > maxRotationSpeedX)//moet voor rotation.y 
                {
                    rotation.y += maxRotationSpeedX;
                }
                else if (differentX * rotationamount < -maxRotationSpeedX)
                {
                    rotation.y += -maxRotationSpeedX;
                }
                else
                {
                    rotation.y += differentX * rotationamount;
                }
            }    
            rotation.z = differentX / 5;
            if (rotation.z < -maxRotationZ)
            {
                rotation.z = -maxRotationZ;
            }
            else if (rotation.z > maxRotationZ)
            {
                rotation.z = maxRotationZ;
            }
            move();
        }
        if (Input.mousePosition.y != Camera.main.pixelHeight / 2)//up down
        {
            differentY = Camera.main.pixelHeight / 2 - Input.mousePosition.y;            
            if (differentY * rotationamount > maxRotationSpeedY)
            {
                rotation.x += maxRotationSpeedY;
            }
            else if (differentY * rotationamount < -maxRotationSpeedY)
            {
                rotation.x += -maxRotationSpeedY;
            }
            else
            {
                rotation.x += differentY * rotationamount;
            }
        }
        if (godown)
        {
            rotation.x += 2;
            print("da");
        }
        transform.rotation = Quaternion.Euler(rotation);
    }
    private void move()
    {        
        _Rigidbody.velocity = transform.forward * speed;
    }
    private void SpeedUp()//w
    {
        speed = maxSpeed;      
    }
    private void SlowDown()//s
    {
        if (speed > 0)
        {
            speed -= 0;
        }
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "border")
        {
            godown = true;
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "border")
        {
            godown = false;
        }
    }
}
