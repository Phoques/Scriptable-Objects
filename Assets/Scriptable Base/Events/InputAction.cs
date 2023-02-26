using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Event
{
	[CreateAssetMenu(menuName = "Events/Input Action Event", fileName = "New Input Action Event")]
	public class InputAction : ScriptableObject
	{


		//This is putting a tooltip on the 'input' variable that shows up in the inspector when you mouse over the 'input' variable.
		[SerializeField, Tooltip("In this slot you need to attach an Input Action Reference from the newInput Systems, 'Input Actions' \n p.s you can make more of these, right click in Assets to make an Input Action")]
		private InputActionReference _input;
		
		//This spaces out how far it sits in the inspector and adding a header (Quite cool!)
		
		[Header("I AM A HEADER"), Space(25), SerializeField, Tooltip("")]
		private UnityEvent <CallbackContext> _actions;
		
		private void Awake()
		{
			hideFlags = HideFlags.DontUnloadUnusedAsset;
			
		}
		
		private void OnEnable()
		{
			hideFlags = HideFlags.DontUnloadUnusedAsset;
			if(!_input)
			{
				//If input isnt connected, do nothing.
				return;
			}
			//Add the action
			_input.action.performed += _actions.Invoke;
		}
		
		private void OnDisable()
		{
			if(!_input)
			{
				return;
			}
			//Remove the action, When using events / listeners you have to make sure to remove the listener after its been used or else it will cause problems.
			// Do more research for clarity.
			_input.action.performed -= _actions.Invoke;
		}
		
		public void Invoke(CallbackContext context) => _actions.Invoke(context);
		public void Add(UnityAction <CallbackContext> action) => _actions.AddListener(action);
		public void Remove(UnityAction <CallbackContext> action) => _actions.RemoveListener(action);
		
		[RuntimeInitializeOnLoadMethod] // This triggers the below event without user input at runtime.
		private static void Load() => Resources.LoadAll("", typeof(InputAction));
	}
}


