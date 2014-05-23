using System;

namespace Agency.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Serializable]
    public class RequiredQueryFieldAttribute : Attribute
    {
        public RequiredQueryFieldAttribute(string clause)
        {
            Clause = clause;
        }

        public string Clause{ get; set;}
    }
}
