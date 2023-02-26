using UnityEngine;
using UnityEngine.Events;

namespace Listener

{
	public class StringListener : MonoBehaviour
	{
		public Event.String atEvent;
		public UnityEvent<string> unityEvent;
		
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

