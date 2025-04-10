using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask chaoLayer;
    [SerializeField] private float distanciaColisaoChao;
    [SerializeField] private bool estaNoChao;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distanciaColisaoChao, chaoLayer))
        {
            estaNoChao = true;
            Debug.DrawRay(transform.position, Vector3.down * distanciaColisaoChao, Color.green);
        }
        else
        {
            estaNoChao = false;
            Debug.DrawRay(transform.position, Vector3.down * distanciaColisaoChao, Color.red);
        }
            
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.position, Vector3.down * chaoLayer);
    // }
}
