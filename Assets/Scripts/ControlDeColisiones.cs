using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeColisiones : MonoBehaviour
{
    public MoverPelota moverPelota;

    void ReboteConRaqueta(Collision2D col)
    {
        Vector3 posicionPelota = this.transform.position;
        Vector3 posicion = col.gameObject.transform.position;
        float altura = col.collider.bounds.size.y;
        float ancho = col.collider.bounds.size.x;

        float x = (posicionPelota.x - posicion.x) / ancho;


        float y = (posicionPelota.y - posicion.y) / altura;

        this.moverPelota.MovimientoPelota(new Vector2(x, y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)
        {
            this.ReboteConRaqueta(collision);
        }
    }
}
