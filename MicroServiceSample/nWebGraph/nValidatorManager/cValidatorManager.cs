using MicroServiceSample.nWebGraph.nValidatorManager.nValidators;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MicroServiceSample.nWebGraph.nValidatorManager
{
    public class cValidatorManager
    {
        List<cBaseValidator> m_Validators { get; set; }
        public cValidatorManager()
        {
            m_Validators = new List<cBaseValidator>();
            InitValidators();
        }

        private void InitValidators()
        {
            m_Validators.Add(new cRequiredValidator());
        }

        public List<string> Validate<TItemType>(JObject _Object)
        {
            List<string> __Validation = new List<string>();
            foreach (cBaseValidator __Validator in m_Validators)
            {
                if (!__Validator.Validate<TItemType>(_Object))
                {
                    __Validation.Add(__Validator.ValidationType);
                }
            }
            
            return __Validation;
        }
    }
}
