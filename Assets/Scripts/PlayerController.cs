using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public InputAction LeftAction;
    public InputAction MoveAction;

    // Start is called before the first frame update
    void Start()
    {
        //LeftAction.Enable();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        //un nouveau commentaire

        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = 0.0f;
        //float vertical = 0.0f;
        //if (LeftAction.IsPressed())
        //{
        //    horizontal = -1.0f;
        //}
        //else if (Keyboard.current.rightArrowKey.isPressed)
        //{
        //    horizontal = 1.0f;
        //}
        //else if (Keyboard.current.upArrowKey.isPressed)
        //{
        //    vertical = 1.0f;
        //}
        //else if (Keyboard.current.downArrowKey.isPressed)
        //{
        //    vertical = -1.0f;
        //}

        //Debug.Log(horizontal);

        //Vector2 position = transform.position;
        //position.x = position.x + 0.1f * horizontal;
        //position.y = position.y + 0.1f * vertical;
        //transform.position = position;

        Vector2 move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
        Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;
        transform.position = position;
    }
}
