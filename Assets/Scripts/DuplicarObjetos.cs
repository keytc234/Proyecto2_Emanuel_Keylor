using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicarObjetos : MonoBehaviour
{
    public List<GameObject> objetosOriginales; // Asigna aquí tus prefabs en el inspector
    public int cantidadCopias = 3; // Número de duplicados que deseas crear

    void Start()
    {
        DuplicaObjetos();
    }

    public void DuplicaObjetos()
    {
        for (int j = 0; j < objetosOriginales.Count; j++)
        {
            GameObject objeto = objetosOriginales[j];
            Vector3 posicionInicial = objeto.transform.position - new Vector3(0, j * 0, 0);

            for (int i = 1; i <= cantidadCopias; i++)
            {
                Vector3 nuevaPosicion = posicionInicial - new Vector3(0, i * 5, 0);
                Instantiate(objeto, nuevaPosicion, Quaternion.identity);
            }
        }
    }

}



