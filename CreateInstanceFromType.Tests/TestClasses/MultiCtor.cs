using System;

namespace CreateInstanceFromType.Tests.TestClasses
{
    public class MultiCtor
    {
        public MultiCtor(string stringValue)
        {
            StringValue = stringValue;
        }

        public MultiCtor(string stringValue, int intValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
        }

        public MultiCtor(string stringValue, int intValue, DateTime dateValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
            DateValue = dateValue;
        }

        public string StringValue { get; }

        public int IntValue { get; }

        public DateTime DateValue { get; }
    }
}