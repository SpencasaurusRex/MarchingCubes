using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // Configuration
    public float Speed = 8;
    public float InputSharpness = 5;

    // Runtime
    Vector2 inputMove;
    Vector2 inputSmoothed;

    Vector3 velocity;

    public void OnMove(InputValue value)
    {
        inputMove = value.Get<Vector2>() * Speed;
    }

    void Update()
    {
        inputSmoothed = Vector2.Lerp(inputSmoothed, inputMove, 1f - Mathf.Exp(-InputSharpness * Time.deltaTime));

        velocity = transform.right * inputSmoothed.x + transform.forward * inputSmoothed.y;
        velocity.y = 0;
    }

    void FixedUpdate()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
