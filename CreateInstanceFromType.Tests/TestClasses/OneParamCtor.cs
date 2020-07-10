namespace CreateInstanceFromType.Tests.TestClasses
{
    public class OneParamCtor
    {
        public OneParamCtor(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}