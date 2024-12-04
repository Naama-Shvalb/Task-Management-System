namespace Dal;
/// <summary>
/// A class that houses the databases in lists
/// </summary>
internal static class DataSource
{
    /// <summary>
    /// A class that generates automatic running numbers
    /// </summary>
    internal static class Config
    {
        /// <summary>
        /// Id task
        /// </summary>
        internal const int StartTaskId = 1;
        private static int nextTaskId = StartTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
        /// <summary>
        /// Id dependency
        /// </summary>
        internal const int StartDependencyId = 1;
        private static int nextDependencyId = StartTaskId;
        internal static int NextDependencyId { get => nextDependencyId++; }
    }
    internal static List<DO.Engineer?> Engineers { get; } = new();
    internal static List<DO.Task?> Tasks { get; } = new();
    internal static List<DO.Dependency?> Dependencies { get; } = new();
}
