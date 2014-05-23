using System;
using System.Collections.Generic;

namespace Agency.PaidTimeOffBLL.Framework
{
    [Serializable]
    internal class ENTProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    [Serializable]
    internal class ENTPropertyList : List<ENTProperty>
    {

    }
}
