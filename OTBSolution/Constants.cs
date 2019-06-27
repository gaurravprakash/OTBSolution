namespace OTBSolution
{
    /// <summary>
    /// Class to hold fixed string values.
    /// </summary>
    public class Constants
    {
        public const string Error_No_Jobs = "There are no jobs to execute.";
        public const string Error_Self_Dependency = "Error : Jobs can’t depend on themselves.";
        public const string Error_Circular_Dependency = "Error : Jobs can’t have circular dependencies.";
    }
}
