using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public GameObject prefabPlayer;
        [Range(1f,20f)]
    public float maxArea = 5f;

    void Start()
    {
        CriarJogador();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void CriarJogador()
    {
        bool temGenteAqui = true;
        RaycastHit hit;

        Vector3 circulo = Vector3.zero;
        while(temGenteAqui)
        {
          circulo = Random.insideUnitCircle * maxArea;
          circulo = new Vector3(circulo.x, 0.5f, circulo.y);

            if (Physics.SphereCast(circulo, 1f, Vector3.forward, out hit, 1f))
            {
                temGenteAqui = hit.transform.CompareTag("Player");
                
                    
                
            }

        }

        PhotonNetwork.Instantiate(prefabPlayer.name, circulo, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(Vector3.zero, maxArea);
    }

}
