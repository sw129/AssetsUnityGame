using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private KeyCode _pauseKey = KeyCode.Escape;
    public static bool gamePauseState = false;

    void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            if(gamePauseState)
            {
                //������������� ���������� �������� ����
                Time.timeScale = 1;
                gamePauseState = false;
            }
            else
            {
                //������������� �������� ���� �� 0
                Time.timeScale = 0;
                gamePauseState = true;
            }
        }
    }
}
