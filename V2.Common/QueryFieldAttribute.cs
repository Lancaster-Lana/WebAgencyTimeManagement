using System;

namespace Agency.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [Serializable]
    public class QueryFieldAttribute : Attribute    
    {
        public enum QueryFieldTypeEnum
        {
            NotSet,
            String,
            Date,
            Number,
            Boolean,
            Lookup
        }

        public QueryFieldAttribute(string fieldName, string friendlyFieldName, QueryFieldTypeEnum fieldType)
        {
            FieldName = fieldName;
            FriendlyFieldName = friendlyFieldName;
            FieldType = fieldType;
        }

        public string FieldName { get; set; }
        public string FriendlyFieldName { get; set; }
        public QueryFieldTypeEnum FieldType { get; set; }
        public string LookupFieldName { get; set; }
    }
}
