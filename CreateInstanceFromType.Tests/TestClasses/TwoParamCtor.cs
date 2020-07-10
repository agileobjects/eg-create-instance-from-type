namespace CreateInstanceFromType.Tests.TestClasses
{
    public class TwoParamCtor
    {
        public TwoParamCtor(string stringValue, int intValue)
        {
            StringValue = stringValue;
            IntValue = intValue;
        }

        public string StringValue { get; }

        public int IntValue { get; }
    }
}