using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorVisao : MonoBehaviour
{
    [SerializeField] private GameObject alvo;
    [SerializeField] private float raioDeVisao;
    [SerializeField] private float limite;
    private float anguloAtual;
    public bool vendo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vendo = false;
        Vector3 direcao = (alvo.transform.position - transform.position).normalized;
        anguloAtual = Vector3.Angle(transform.forward, direcao);
        //Debug.Log(anguloAtual);
        if (anguloAtual <= raioDeVisao)
        {
            //Dentro do campo de visão 
            //identificar se não tem nenhum obstáculo 
            RaycastHit coisa;
            Physics.Raycast(transform.position, direcao, out coisa, limite);
            if (coisa.collider.name == "jogador")
            {
                vendo = true;
            }

            #region retornar todos os objetos 
            /*
            RaycastHit[] varios;
            varios = Physics.RaycastAll(transform.position, direcao, limite);
            if (varios[0].collider.name != "jogador")
            {
                if (varios[0].collider.name == "janela")
                {
                    if (varios[1].collider.name == "jogador")
                    {
                        vendo = true;
                    }
                }
            }
            */
            #endregion
        }
        
    }
}
