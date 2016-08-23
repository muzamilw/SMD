namespace SMD.Interfaces.Data
{
    /// <summary>
    /// Security Access Right
    /// Has Right Id - Right Id should not be autoincremented but it should take section id if parent section
    /// and for child actions it should be incremented or we need to change the rightId with RightName
    /// </summary>
    public enum SecurityAccessRight
    {
        /// <summary>
        /// Can View Security
        /// </summary>
        CanViewSuperNovaAdmin = 1,

        
    }
}
