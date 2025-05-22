namespace Nexus.API.Manager.Validations
{
    public class CustomValidation : Exception
    {
        public Dictionary<string, string> validations { get; set; }

        public CustomValidation(Dictionary<string, string> Validations)
        {
            validations = Validations;
        }
    }
}
