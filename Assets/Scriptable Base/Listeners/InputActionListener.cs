using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Listener

{
	public class InputActionListener : MonoBehaviour
	{
		public Event.InputAction atEvent;
		public UnityEvent <InputAction.CallbackContext> unityEvent;
		
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