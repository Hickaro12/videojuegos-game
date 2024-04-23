using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{

        public float moveSpeed = .50f;
        public  float JumpForce = 1.9f;

        public float gravityScale = 20.0f;

        private Vector3 moveDirection;    

        public CharacterController controller;

        public Camera playerCamera;

        public GameObject playerModel;

        public float rotationspeed = 5f;

        public Animator animator;

        public int maxJumps = 2; 
        public int  jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }   
    

    // Update is called once per frame
    void Update()

    {
        float xDisplacement = Input.GetAxis("Horizontal");
        float zDisplacement = Input.GetAxis("Vertical");
        float yStore = moveDirection.y;

        // movimiento
        moveDirection = (transform.forward * zDisplacement + transform.right * xDisplacement);
        moveDirection.Normalize();
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y =yStore;

        //Salto
        if (controller.isGrounded)
        {
            jumpCount = 0; // Restablece el contador de saltos cuando el jugador está en el suelo
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            moveDirection.y = JumpForce;
            jumpCount++; // Incrementa el contador de saltos cada vez que se salta
        }
        
        // Asegúrate de aplicar gravedad u otras físicas necesarias aquí
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
       
        moveDirection.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime * gravityScale);

        if(xDisplacement != 0 || zDisplacement !=0)
        {
            transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationspeed * Time.deltaTime);

        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        animator.SetBool("isGrounded",controller.isGrounded);
    }
}

