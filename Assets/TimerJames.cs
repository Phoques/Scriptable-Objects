using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable; // This is the namespace needed to allow the use of the 'Float' class.

public class TimerJames : MonoBehaviour
{

    // Using the Float Scriptable object we created.
    public Float timerFloat;

    private void Start()
    {
        timerFloat._initialValue = 0f;
        

    }

    private void Update()
    {
        timerFloat._runtimeValue += Time.deltaTime;
    }

}
