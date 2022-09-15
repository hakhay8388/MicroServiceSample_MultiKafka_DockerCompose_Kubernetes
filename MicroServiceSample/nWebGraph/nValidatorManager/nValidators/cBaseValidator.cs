using Newtonsoft.Json.Linq;

namespace MicroServiceSample.nWebGraph.nValidatorManager.nValidators
{
    public abstract class cBaseValidator
    {
        public string ValidationType { get; set; }
        public cBaseValidator(string _ValidationType)
        {
            ValidationType = _ValidationType;
        }
        public abstract bool Validate<TItemType>(JObject _Object);
        
    }
}
