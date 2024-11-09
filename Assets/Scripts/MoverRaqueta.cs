using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverRaqueta: MonoBehaviour
{
    public float velocidadMovimiento;

    private void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(v, 0) * velocidadMovimiento;
    }

}
