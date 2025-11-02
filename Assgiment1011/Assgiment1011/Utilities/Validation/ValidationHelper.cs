using Microsoft.Xrm.Sdk.Workflow.Activities;

namespace Assgiment1011.Utilities.Validation
{
    public static class ValidationHelper
    {
        private static string FieldVerificationValue;

        public static void SetFieldVerification(string value)
        {
            FieldVerificationValue = value;
        }

        public static bool GetValidation
        {
            get
            {
                var ifValidation = false;
                if(FieldVerificationValue == null)
                {
                    return ifValidation;
                }

                try
                {
                    ifValidation = Convert.ToBoolean(FieldVerificationValue);
                }
                catch
                {
                    return ifValidation;
                }

                return ifValidation;
            }
        }

        public static List<Workflow> Workflows { get; set; }
    }
}
