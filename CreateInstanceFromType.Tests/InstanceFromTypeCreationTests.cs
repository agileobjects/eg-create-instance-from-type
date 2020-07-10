namespace CreateInstanceFromType.Tests
{
    using System;
    using Xunit;

    public class InstanceFromTypeCreationTests
    {
        [Fact]
        public void ShouldUseAParameterlessCtor()
        {
            var instance = (Parameterless)CreateInstanceFromType2020
                .GetInstance(typeof(Parameterless));

            Assert.NotNull(instance);
        }

        [Fact]
        public void ShouldUseASingleParameterCtor()
        {
            var instance = (OneParamCtor)CreateInstanceFromType2020
                .GetInstance(typeof(OneParamCtor), "hello!");

            Assert.NotNull(instance);
            Assert.Equal("hello!", instance.Value);
        }

        [Fact]
        public void ShouldUseATwoParameterCtor()
        {
            var instance = (TwoParamCtor)CreateInstanceFromType2020
                .GetInstance(typeof(TwoParamCtor), "hello again!", 123);

            Assert.NotNull(instance);
            Assert.Equal("hello again!", instance.StringValue);
            Assert.Equal(123, instance.IntValue);
        }

        [Fact]
        public void ShouldSelectACtorFromArguments()
        {
            var twoParamInstance = (MultiCtor)CreateInstanceFromType2020
                .GetInstance(typeof(MultiCtor), "hello there!", 456);

            Assert.NotNull(twoParamInstance);
            Assert.Equal("hello there!", twoParamInstance.StringValue);
            Assert.Equal(456, twoParamInstance.IntValue);
            Assert.Equal(default, twoParamInstance.DateValue);

            var oneParamInstance = (MultiCtor)CreateInstanceFromType2020
                .GetInstance(typeof(MultiCtor), "hello you!");

            Assert.NotNull(oneParamInstance);
            Assert.Equal("hello you!", oneParamInstance.StringValue);
            Assert.Equal(default, oneParamInstance.IntValue);
            Assert.Equal(default, twoParamInstance.DateValue);

            var threeParamInstance = (MultiCtor)CreateInstanceFromType2020
                .GetInstance(typeof(MultiCtor), "hello blah!", 999, DateTime.MinValue);

            Assert.NotNull(threeParamInstance);
            Assert.Equal("hello blah!", threeParamInstance.StringValue);
            Assert.Equal(999, threeParamInstance.IntValue);
            Assert.Equal(DateTime.MinValue, threeParamInstance.DateValue);
        }

        #region Helper Members

        public class Parameterless { }

        public class OneParamCtor
        {
            public OneParamCtor(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }

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

        #endregion
    }
}
