using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlayer : MonoBehaviour
{
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;
    [SerializeField] private bool estaNoChao;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask colisaoLayer;
    private float forcaY;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moviment = new Vector3(horizontal, 0, vertical);

        moviment = myCamera.TransformDirection(moviment);
        moviment.y = 0;

        controller.Move(moviment * Time.deltaTime * 5);


        if (moviment != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moviment), Time.deltaTime * 10);

        animator.SetBool("Mover", moviment != Vector3.zero);

        estaNoChao = Physics.CheckSphere(peDoPersonagem.position, 0.3f, colisaoLayer);
        animator.SetBool("EstaNoChao", estaNoChao);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {            
            forcaY = 5f;
            animator.SetTrigger("Saltar");
        }
        if (forcaY > -9.8f)
        {
            forcaY += -9.81f * Time.deltaTime;
        }
        
        controller.Move(new Vector3(0, forcaY, 0) * Time.deltaTime);
    }
}
