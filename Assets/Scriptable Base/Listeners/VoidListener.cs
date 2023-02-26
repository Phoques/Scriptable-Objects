using UnityEngine;
using UnityEngine.Events;

namespace Listener

{
	public class VoidListener : MonoBehaviour
	{
		public Event.Void atEvent;
		public UnityEvent unityEvent;
		
		private void OnEnable()
		{
			atEvent.Add(unityEvent.Invoke);
		}
		
		private void OnDisable()
		{
			atEvent.Remove(unityEvent.Invoke);
		}
	}
}

//Homework Mess around with this system, make more variable types (Float in String are done) Mess around and try to get one event working
// using the scriptable event we created today. Can be any event.