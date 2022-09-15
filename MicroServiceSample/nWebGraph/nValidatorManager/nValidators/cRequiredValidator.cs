using Core.nDTOs;
using Core.nDTOs.nEvent;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace MicroServiceSample.nWebGraph.nValidatorManager.nValidators
{
    public class cRequiredValidator : cBaseValidator
    {
        public cRequiredValidator() 
            : base("RequiredValidator")
        {
        }

        public override bool Validate<TItemType>(JObject _Object)
        {
            Type __Type = typeof(TItemType);

            FieldInfo[] __Fields = __Type.GetFields();
            foreach (FieldInfo __Field in __Fields)
            {
                DTOField __RequiredValue = __Field.GetCustomAttribute<DTOField>();
                if (__RequiredValue != null)
                {
                    if (!__RequiredValue.Nullable && _Object.ContainsKey(__Field.Name))
                    {
                        object __Value = _Object.GetValue( __Field.Name);
                        if (string.IsNullOrEmpty(__Value.ToString()))
                        {
                            return false;
                        }

                        if (typeof(IDTOObject).IsAssignableFrom(__Field.FieldType))
                        {
                            Type __ThisType = this.GetType();
                            MethodInfo __ValidMethodInfo = __ThisType.GetMethod("Validate");

                            MethodInfo __ValidMethodInfoGeneric = __ValidMethodInfo.MakeGenericMethod(__Field.FieldType);

                            bool __Result = (bool)__ValidMethodInfoGeneric.Invoke(this, new object[] { __Value });
                            if (!__Result)
                            {
                                return false;
                            }
                        }
                       
                    }
                    else if (!__RequiredValue.Nullable && !_Object.ContainsKey(__Field.Name))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
