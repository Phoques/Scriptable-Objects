using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Variable; // This is the namespace needed to allow the use of the 'Float' class.

public class TimerJames : MonoBehaviour
{

    // Using the Float Scriptable object we created.
    public Float timerFloat;
	public bool isCountdown;

    private void Start()
    {
		
       //timerFloat.RuntimeMode = timerFloat.InitialValue;
        

    }

    private void Update()
    {
		
		if(isCountdown)
		{
			if(timerFloat.Value > 0)
			{
				timerFloat.Value -= Time.deltaTime;
			}
			else
			{
				timerFloat.Value += Time.deltaTime;
			}
		}
		
        //timerFloat._runtimeValue += Time.deltaTime;
    }

}
