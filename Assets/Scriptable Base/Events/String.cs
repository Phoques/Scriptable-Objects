using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Event
{
	[CreateAssetMenu(menuName = "Events/String Event", fileName = "New String Event")]
	public class String : EventBase<string>
	{

		public void PrintMessage(string message)
		{
			Debug.Log(message);
		}

        

    }
}


