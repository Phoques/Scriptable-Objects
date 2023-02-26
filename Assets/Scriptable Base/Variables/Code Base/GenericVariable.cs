
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
//Family Name
namespace Variable
{


    // The ISerializationCallbackReceiver is an interface to recieve callbacks upon serialization and deserialization.
    //Unity's serializer is able to serialize most datatypes, but not all of them. In these cases, there are two callbacks available for you to manually process these datatypes so that
    //Unity can serialize and deserialise them.
    //Care needs to be taken whilst within these callbacks, as Unity's serializer runs on a different thread to most of the Unity API. It is advisable to only process fields directly owned
    //by the object, keeping the processing burden as low as possible.
    //Serialization can occur during all kinds of operations.For example, when using Instantiate() to clone an object, Unity serializes and deserializes the original object in order to
    //find internal references to the original object, so that it can replace them with references to the cloned object. In this situation, you can also employ the callbacks to update any
    //internal references using types that Unity can't serialize.
    //The callback interface only works with classes.It does not work with structs.
    
	
	//Generic Type - <T> (This is predefined by C#) allow us to create a 'variable' of type T, to assign later.
	//Like Below, we have created two 'T' types that we can then assign later.
	
	// Inheritance - Inhereting allows a class to 'inheret' from its parent, in this case the 'child' or derived class can use / change variables, functions etc. 
	// (Protected moreso needed than private for encapsulation in inheretance.) Also See Polymorphism
	
	// Interface - Interfaces defines a contract, shared functionality. Interfaces defined on a parent class means all derived classes will have this interface functionality.
	//Some interfaces are predefined (IPointerDown etc) but others can be created by the programmer. In this circumstance, I SerialisationCallbackReciver
	// has an 'OnBeforeSerialize' and 'OnAfterDeserialize' Functions
    
                //Generic Type <T> is the generic type       //Inheritance     // Interface
    public class GenericVariable <T> : BaseVariable, ISerializationCallbackReceiver
    {
		// This is assigning the Enum 'container' / List called 'RunTimeMode' to the _runtimeMode variable. and below with PersistenceMode
        //RuntimeMode - change between Read only or Read and Write
		public enum RunTimeMode { ReadOnly,ReadWrite}
		[SerializeField] private RunTimeMode _runtimeMode;
		
		//PersistenceMode - Changes what is being edited, None means runtime value and Persist initial value
        public enum PersistenceMode { None,Persist} // None = 0, Persist = 1 like how Enums work
		[SerializeField] private PersistenceMode _persistenceMode;

        //The 2 values stored that we can interact with.
        [SerializeField] private T _initialValue;
        [SerializeField] private T _runtimeValue;
        
		//This apparently this bypasses the functionality already written below as the Generic property
		#region Properties
		/*public T InitialValue
		{
			//Reading the value
			get{return _initialValue;}
			//Writing the value
			set{_initialValue = value;}
		}
		
		public T RuntimeValue
		{
			get{return _runtimeValue;}
			set{_runtimeValue = value;}
		}*/
		
		#endregion
		

        // => is the Lambda expression when used below, it allows us to apply functionalism and pass it back to our variable

        // persistance is set to true, if persistenceMode is == the Enum Persist;
        private bool _persistence => _persistenceMode == PersistenceMode.Persist;
        
		
		
		
		// Public Generic property that can take in any value.
		public T Value
        { // get allows you to Read.
          // the ? is short hand for if, so 'if persistance bool is true, it equals _initialValue,  ':' means else, so else (we are false) _runtimeValue;
          // ? and : are shorthands

        //Like
        // if(persistence)
        //{
        //   return _initialValue;
        //}
        //else
        //{
        // return _runTimeValue;
        //}
            get => (_persistence) ? _initialValue: _runtimeValue;
            set
            {
				//Change what we are writing to based off the runtime mode
                switch (_runtimeMode)
                {
					//if the runtime mode is set to ReadOnly 
                    case RunTimeMode.ReadOnly:
					//Log an error that we can not write it.
                        Debug.Log("Attempted to set read only variable");
                        break;
						
						//If the runtime mode is set to Read and Write

                    case RunTimeMode.ReadWrite:
					
					//If the persistence is set to 'Persist' then we write to the intiial value.
                        if (_persistence)
                        {
                            _initialValue = value;
                        }
						
						//If the persistence is set to None then we write to the runtime Value;
                        else
                        {
                            _runtimeValue = value;
                        }
                        break;
						
						//This should never run but incase we did soemthing stupid it lets us know.
                    
                    default:
                        Debug.Log("Attempted to set read only variable... Default");
                        break;
                }       

            }
        }

        //public is an accessor, static belongs to the class not the object, implicit operator is:
        // Implicit operators convert automatically when required, Implicit must always be static
        //Implicit seems to convert something?? When something cannot be implicitly converted (Check with Jaymie)

//Cant find the implicit >_> dunno where it went... >_<

		// This is only passing over a generic type, and it is then being converted in the 'timer' script (TimerJames) 
		//If this type was defined as an int for example, and it was being passed over then converted to a float, it would need to be an Explicit operator.
		//Though James wasnt too sure, and it isnt actually doing anything here so, has been commented out.
        
		
		
		

        //Inheretance Behaviours
        public override void SaveToInitialValue()
        {
            _initialValue = _runtimeValue;

            Debug.Log("Save Runtime to Initial Value");
        }
        /// <summary>
        /// BLAH BLAH by putting this here above a function will mean that when you mouse over the function it will show this summary.
        /// </summary>
        public override void ToggleRuntimePersistence()
            //Set _persistenceMode to the opposite mode / toggles it
        {//  _persistenceMode equals If persistenceMode == 0 (None in the Enum) PersistenceMode is now 1 (Persist in the Enum) ELSE (:) 0 (none)

            //This basically says if its none, it becomes persist, and if its persist it becomes None AKA it toggles.
            _persistenceMode = (_persistenceMode == 0) ? (PersistenceMode) 1 : 0;
        }
        /// <summary>
        /// ToggleRuntimeMode allows you to Toggle the _runtimeMode
        /// This means that if its ReadOnly it becomes ReadWrite and if its ReadWroite it becomes ReadOnly
        /// AKA it Toggles
        /// </summary>
        public override void ToggleRuntimeMode()
        {
            _runtimeMode = (_runtimeMode == 0) ? (RunTimeMode) 1 : 0;
        }


        //Interface Behaviours
        public void OnBeforeSerialize()
        {
			//Not used
        }

        public void OnAfterDeserialize()
        {
			// If you press play, then stop then press play again if persistence is not active (In the inspector) then the runtime value is = to the initial value.
			//If our PersistenceMode is set to None
			//we are using the InitialValue as oour start value and modifying our RuntimeValue
			//This between playmode resets (Pressing play and stop then play) allows us to set the RuntimeValue back to the initialValue.
            if(!_persistence)
            {
                _runtimeValue = _initialValue;
            }

        }


    }

}
