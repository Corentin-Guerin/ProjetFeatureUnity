using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFPS : MonoBehaviour
{
   
    public Camera playerCamera;
    public CharacterController characterController;
   

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 15f;
    public float jumpSpeed = 8f;
 
    float gravity = 20f;
 
    Vector3 moveDirection;
 
    private bool isRunning = false;
 
    float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationXLimit = 45.0f;


    private bool dogPet = true;
    private bool monkeyPet = true;
    private bool parrotPet = true;

    private bool activePet = true;
    [SerializeField]
    private float cdActivePet = 10.0f;

    public MouvementDog mouvDog;
    public MouvementMonkey mouvMonkey;
    public MouvementParrot mouvParrot;






    public void Start()
    {
        //Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        mouvDog = FindObjectOfType<MouvementDog>();
        mouvMonkey = FindObjectOfType<MouvementMonkey>();
        mouvParrot = FindObjectOfType<MouvementParrot>();

    }
 
  
    public void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
 
  
        float speedZ = Input.GetAxis("Vertical");
        float speedX = Input.GetAxis("Horizontal");
        float speedY = moveDirection.y;
 

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
 

        if (isRunning)
        {
            speedX = speedX * runningSpeed;
            speedZ = speedZ * runningSpeed;
        }
        else
        {
            speedX = speedX * walkingSpeed;
            speedZ = speedZ * walkingSpeed;
        }
 

        moveDirection = forward * speedZ + right * speedX;
 
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
 
            moveDirection.y = jumpSpeed;
        }
      else
        {
            moveDirection.y = speedY;
        }
 

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
 
        characterController.Move(moveDirection * Time.deltaTime);
 
 

        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
 
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);


        /*if (Input.GetKeyDown(KeyCode.E) && (activePet))
        {   
            activePet = false;
            if (dogPet)
            {
                mouvDog.LaunchSearch();
            }

            if (monkeyPet)
            {
                mouvMonkey.LaunchSearch();
            }
            if (parrotPet)
            {
                mouvParrot.LaunchSearch();
            }

        }

        if (!activePet)
        {
            StartCoroutine(CouldownActivePet(cdActivePet));
            
        }*/
    }
    /*IEnumerator CouldownActivePet(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activePet = true; 
    }*/
}