using UnityEngine;

//Family Name
namespace Variable
{
    //Abstract - The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class. (Child class / inhereted)
                                        //Family Genes
    public abstract class BaseVariable : ScriptableObject
    {


        public abstract void SaveToInitialValue();
        public abstract void ToggleRuntimePersistence();
        public abstract void ToggleRuntimeMode();


    }


}
