
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
    
    
                //Generic Type <T> is the generic type       //Inheritance     // Interface
    public class GenericVariable <T> : BaseVariable, ISerializationCallbackReceiver
    {
        public enum RunTimeMode { ReadOnly,ReadWrite}
        public enum PersistenceMode { None,Persist} // None = 0, Persist = 1 like how Enums work

        //Changed these to public to allow it to be used in the Timer Scriptable object
        [SerializeField] public T _initialValue;
        [SerializeField] public T _runtimeValue;


        [SerializeField] private RunTimeMode _runtimeMode;
        [SerializeField] private PersistenceMode _persistenceMode;


        // => is the Lambda expression when used below, it allows us to apply functionalism and pass it back to our variable

        // persistance is set to true, if persistenceMode is == the Enum Persist;
        private bool _persistence => _persistenceMode == PersistenceMode.Persist;
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
                switch (_runtimeMode)
                {
                    case RunTimeMode.ReadOnly:
                        Debug.Log("Attempted to set read only variable");
                        break;

                    case RunTimeMode.ReadWrite:
                        if (_persistence)
                        {
                            _initialValue = value;
                        }
                        else
                        {
                            _runtimeValue = value;
                        }
                        break;
                    
                    default:
                        Debug.Log("Attempted to set read only variable... Default");
                        break;
                }       

            }
        }

        //public is an accessor, static belongs to the class not the object, implicit operator is:
        // Implicit operators convert automatically when required, Implicit must always be static
        //Implicit seems to convert something?? When something cannot be implicitly converted (Check with Jaymie)

        public static implicit operator T(GenericVariable<T> variable)
        {
            return variable.Value;
        }

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

        }

        public void OnAfterDeserialize()
        {
            if(!_persistence)
            {
                _runtimeValue = _initialValue;
            }

        }


    }

}
