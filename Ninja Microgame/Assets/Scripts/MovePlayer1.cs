using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer1 : MonoBehaviour
{
    
    Rigidbody2D rb2d;

    [Header("MoveSet")]
    private float HorizontalMove = 0f;
    public float SpeedMove;
    [Range(0f,0.3f)]
    public float SmoothMove;
    private Vector3 Speed = Vector3.zero;
    private bool LookRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Orientación según las teclas presionadas A o D.
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb2d.velocity = new Vector2(SpeedMove, rb2d.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb2d.velocity = new Vector2(-SpeedMove, rb2d.velocity.y);
        }
    }
    // Mejor control de físicas.
    void FixedUpdate()
    {
        HorizontalMove = rb2d.velocity.x;
        MovePlayer(HorizontalMove * Time.fixedDeltaTime);
    }
    // Movimiento de izquierda a derecha, y suavizado de frenado.
    void MovePlayer(float Move)
    {
        Vector3 SpeedObjective = new Vector2(Move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, SpeedObjective, ref Speed, SmoothMove);
        if (Move > 0 && !LookRight)
        {
            Turn();
        }else if(Move < 0 && LookRight)
        {
            Turn();
        }
    }
    // Girar el personaje hacia el lado que está mirando.
    private void Turn()
    {
        LookRight = !LookRight;
        Vector3 scaleP = transform.localScale;
        scaleP.x *= -1;
        transform.localScale = scaleP;
    }
}
