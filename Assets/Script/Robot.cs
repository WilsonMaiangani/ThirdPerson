using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField] private float velocidade;
    [SerializeField] private float velocidadeCorrida;
    [SerializeField] private float forcaGravidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private bool estaNoChao;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask colisaoLayer;
    [SerializeField] private LayerMask chaoLayer;
    [SerializeField] private float distanciaColisaoChao;
    [SerializeField] private bool atack;
    [SerializeField] private float tempoAtack;
    [SerializeField] private int limiteVelocidadeCorrida;
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;
    private RaycastHit hit;
    private float velocidadeDirecao;
    private Vector3 direcao;
    Vector3 moveParaBaixo;
    private AtackPlayer atackPlayer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
        atackPlayer = GetComponent<AtackPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
    }

    private void Movimentacao()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direcao = new Vector3(horizontal, 0, vertical);

        direcao = myCamera.TransformDirection(direcao);
        direcao.y = 0;


        if (direcao != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direcao), Time.deltaTime * 10);

        estaNoChao = Physics.Raycast(transform.position, Vector3.down, out hit, distanciaColisaoChao, chaoLayer);
        // estaNoChao = Physics.CheckSphere(peDoPersonagem.position, distanciaColisaoChao, colisaoLayer);
        animator.SetBool("EstaNoChao", estaNoChao);

        if (estaNoChao && !Input.GetKey(KeyCode.LeftShift))
        {
            if (limiteVelocidadeCorrida < 100)
                GameManager.instancia.PreencherBarraVelocidade(limiteVelocidadeCorrida++);

            ExecutarAnimAndar();
        }

        if (direcao != Vector3.zero && estaNoChao && Input.GetKey(KeyCode.LeftShift))
        {
            if (limiteVelocidadeCorrida > 0)
            {
                velocidadeDirecao = velocidadeCorrida;

                animator.SetBool("Andar", false);
                animator.SetBool("Correr", direcao != Vector3.zero);

                GameManager.instancia.DiminuirBarraVelocidade(limiteVelocidadeCorrida--);
            }
            if (limiteVelocidadeCorrida == 0) ExecutarAnimAndar();
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao && direcao == Vector3.zero)
        {
            forcaGravidade = forcaPulo;
            animator.SetTrigger("Saltar");
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao && direcao != Vector3.zero)
        {
            forcaGravidade = forcaPulo;
            animator.SetTrigger("Andar_Pular");
        }
        if (Input.GetMouseButtonDown(0) && estaNoChao && direcao == Vector3.zero)
        {
            atack = Input.GetMouseButtonDown(0);
            StartCoroutine(ExecutarAtack(atack));
        }

        if (estaNoChao && Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) && estaNoChao && direcao != Vector3.zero)
        {
            forcaGravidade = forcaPulo;
            animator.SetTrigger("Andar_Pular");
            Debug.Log("aqui");
        }



        controller.Move(direcao * velocidadeDirecao * Time.deltaTime);

        //Gravidade
        if (forcaGravidade > -9.81f) forcaGravidade += -9.81f * Time.deltaTime;

        controller.Move(new Vector3(0f, forcaGravidade, 0f) * Time.deltaTime);
        //

    }

    private IEnumerator ExecutarAtack(bool Atack)
    {
        animator.SetBool("Atack", Atack);
        atackPlayer.Atack();
        yield return new WaitForSeconds(tempoAtack);
        atack = !Atack;
        animator.SetBool("Atack", atack);
    }

    private void ExecutarAnimAndar()
    {
        velocidadeDirecao = velocidade;
        animator.SetBool("Andar", direcao != Vector3.zero);
        animator.SetBool("Correr", false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log(hit.gameObject.tag);

        // if (hit.gameObject.CompareTag("Armadilha_Pico"))
        // {
        //     GameManager.instancia.CausarDanoPlayer(10);
        // }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Armadilha_Pico"))
        {
            // GameManager.instancia.CausarDanoPlayer(10);
        }
    }




    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.position, Vector3.down * chaoLayer);
    // }

    void GuardarCOD()
    {
        // estaNoChao = Physics.CheckSphere(peDoPersonagem.position, 0.3f, colisaoLayer);
        // animator.SetBool("EstaNoChao", estaNoChao);

        // if (Physics.Raycast(transform.position, Vector3.down, out hit, distanciaColisaoChao, chaoLayer))
        // {
        //     Debug.DrawRay(transform.position, Vector3.down * distanciaColisaoChao, Color.green);

        //     estaNoChao = true;
        //     animator.SetBool("EstaNoChao", estaNoChao);

        // }
        // else
        // {
        //     estaNoChao = false;
        //     Debug.DrawRay(transform.position, Vector3.down * distanciaColisaoChao, Color.red);
        // }
    }



}
