using UnityEngine;
using UnityEngine.Events;

namespace Event
{
	public class EventBase <T> : ScriptableObject
	{
   
		[SerializeField] private UnityEvent<T> _eventResponses;
		
		//Lambda Expression =>
		public void Invoke(T context) => _eventResponses?.Invoke(context);
		public void Add(UnityAction <T> action) => _eventResponses.AddListener(action);
		public void Remove(UnityAction <T> action) => _eventResponses.RemoveListener(action);
		
   
   
	}
	
}


