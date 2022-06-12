namespace InsuranceRecordsWeb.StaticClasses
{
    public static class Uppercase
    {
        //Returns string from the parameter with the uppercased first letter 
        public static string UpperCase(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
