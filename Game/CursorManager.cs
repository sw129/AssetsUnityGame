using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private KeyCode _keyVisibleCursor = KeyCode.Escape;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyVisibleCursor))
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }   
            else
            {
               Cursor.visible = true;
               Cursor.lockState = CursorLockMode.None;
            }
        }
    }

}
