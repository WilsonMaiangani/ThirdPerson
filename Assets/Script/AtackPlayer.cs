using UnityEngine;

public class AtackPlayer : MonoBehaviour
{
    [SerializeField] private Transform pontoAtack;
    [SerializeField] private float distanciaAtack = 0.5f;
    [SerializeField] private LayerMask layerInimigo;
    [SerializeField] private int danoAtack = 30;


    public void Atack()
    {
        Collider[] iminigos = Physics.OverlapSphere(pontoAtack.position, distanciaAtack, layerInimigo);

        foreach (Collider inimigo in iminigos)
        {
            inimigo.GetComponent<Inimigo>().CausarDanoInimigo(danoAtack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (pontoAtack == null) return;

        Gizmos.DrawWireSphere(pontoAtack.position, distanciaAtack);
    }

}
