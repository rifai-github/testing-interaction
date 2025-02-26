using UnityEngine;

public class InputManager
{
    private static bool initialize;
    private static InputSystem_Actions inputActions;

    private static void Initialize()
    {
        if (initialize)
            return;

        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        initialize = true;
    }

    public static Vector2 GetMovement()
    {
        Initialize();
        return inputActions.Player.Move.ReadValue<Vector2>();
    }
}