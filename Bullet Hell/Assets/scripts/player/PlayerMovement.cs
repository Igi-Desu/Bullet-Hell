using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : Singleton<PlayerMovement>
{
    // Start is called before the first frame update
    [SerializeField] float speed = 20f;
    Vector3 moveDirection = new(0, 0, 0);
    bool facingRight = false;
    public bool FacingRight => facingRight;
    float basegodtimer = 3;
    System.Action<InputAction.CallbackContext> horizontalMove, verticalMove, shot;
    Rigidbody2D rb;

    [SerializeField] GameObject pocisk;

    void Start()
    {
        rb = Player.Instance.GetComponent<Rigidbody2D>();
        return;
    }
    void FixedUpdate()
    {
        if (GameManager.Instance.GameOver) return;
        rb.MovePosition(transform.position + moveDirection.normalized * Time.deltaTime * speed);
    }
    /// <summary>
    /// removed for now
    /// </summary>
    /// <param name="ctx"></param>
    public void Shoot(InputAction.CallbackContext ctx)
    {
        Debug.Log("Bang");
        return;
        //Instantiate(pocisk, transform.position, transform.rotation);
    }
    public void Movement(InputAction.CallbackContext ctx)
    {
        moveDirection = ctx.ReadValue<Vector2>();
        if (moveDirection.x < 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x > 0 && facingRight)
        {
            Flip();
        }
    }
    //flips character
    void Flip()
    {
        Player.Instance.SpriteRenderer.flipX = !Player.Instance.SpriteRenderer.flipX;
        facingRight = !facingRight;
    }

    public bool getFacingright()
    {
        return facingRight;
    }





}
