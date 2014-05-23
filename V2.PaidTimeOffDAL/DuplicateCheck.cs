namespace Agency.PaidTimeOffDAL
{
    /// <summary>
    /// This class represent the return value of the dynamic query that check if there is a duplicate item in a table.
    /// </summary>
    internal class DuplicateCheck
    {
        public int DuplicateCount { get; set; }
    }
}
