using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoverPelota : MonoBehaviour
{
    public float velocidadMovimiento;
    public float velocidadExtraMaxima;
    public int contadorPerdidasMaximo = 3;

    int contadorGolpes = 0;
    int contadorPerdidas = 0;

    Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(this.IniciarPelota());
    }

    public IEnumerator IniciarPelota()
    {
        contadorGolpes = 0;
        yield return new WaitForSeconds(0.5f); // Pequeña pausa antes de iniciar
        MovimientoPelota(new Vector2(0, 1)); // Inicia la pelota en una dirección
    }

    public void MovimientoPelota(Vector2 dir)
    {
        dir = dir.normalized;
        float velocidad = velocidadMovimiento + Mathf.Min(contadorGolpes, velocidadExtraMaxima);

        // Validar que la dirección no contenga valores NaN antes de asignarla
        if (float.IsNaN(dir.x) || float.IsNaN(dir.y))
        {
            dir = new Vector2(Random.Range(-1f, 1f), -1).normalized; // Dirección predeterminada en caso de error
        }

        rigidBody2D.velocity = dir * velocidad;

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocidadActual = rigidBody2D.velocity.normalized;
        Vector2 nuevaDireccion = velocidadActual;

        if (collision.gameObject.CompareTag("CuboEliminar"))
        {
            // Eliminar el cubo y cambiar dirección
            Destroy(collision.gameObject);
            contadorGolpes++;
            nuevaDireccion = new Vector2(velocidadActual.x, -velocidadActual.y);

        }
        else if (collision.gameObject.CompareTag("ParedDerecha") || collision.gameObject.CompareTag("ParedIzquierda"))
        {
            // Invertir solo la componente X (para paredes laterales)
            nuevaDireccion = new Vector2(-velocidadActual.x, velocidadActual.y);
        }
        else if (collision.gameObject.CompareTag("ParedArriba") || collision.gameObject.CompareTag("ParedAbajo"))
        {
            // Invertir solo la componente Y (para paredes superior e inferior)
            nuevaDireccion = new Vector2(velocidadActual.x, -velocidadActual.y);
        }

        MovimientoPelota(nuevaDireccion);

        // Verificar si la pelota tocó la pared inferior para restar una vida
        if (collision.gameObject.CompareTag("ParedAbajo"))
        {
            contadorPerdidas++;
            if (contadorPerdidas >= contadorPerdidasMaximo)
            {
                SceneManager.LoadScene("GameOverScene"); // Cargar escena de Game Over
            }
        }

        if(contadorGolpes >= 32)
        {
            
           SceneManager.LoadScene("Gane"); // Cargar escena de Gane
           
        }
    }
}
