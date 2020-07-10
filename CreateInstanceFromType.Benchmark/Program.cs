using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CreateInstanceFromType.Tests.TestClasses;

namespace CreateInstanceFromType.Benchmark
{
    public class Program
    {
        [MemoryDiagnoser]
        [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
        public class CreateInstanceFromTypeBenchmark
        {
            private const string _parameterless = "Parameterless";
            private const string _oneParamCtor = "One Param";
            private const string _twoParamsCtor = "Two Params";
            private const string _threeParamsCtor = "Three Params";

            [Benchmark(Baseline = true, Description = "New"), BenchmarkCategory(_parameterless)]
            public object New_0()
            {
                return new Parameterless();
            }

            [Benchmark(Baseline = true, Description = "New"), BenchmarkCategory(_oneParamCtor)]
            public object New_1()
            {
                return new OneParamCtor("hello!");
            }

            [Benchmark(Baseline = true, Description = "New"), BenchmarkCategory(_twoParamsCtor)]
            public object New_2()
            {
                return new TwoParamCtor("hello!", 123);
            }

            [Benchmark(Baseline = true, Description = "New"), BenchmarkCategory(_threeParamsCtor)]
            public object New_3()
            {
                return new MultiCtor("hello!", 123, DateTime.MinValue);
            }

            #region Activator.CreateInstance

            [Benchmark(Description = "Activator"), BenchmarkCategory(_parameterless)]
            public object ActivatorCreateInstance_0()
            {
                return Activator.CreateInstance(typeof(Parameterless));
            }

            [Benchmark(Description = "Activator"), BenchmarkCategory(_oneParamCtor)]
            public object ActivatorCreateInstance_1()
            {
                return Activator.CreateInstance(typeof(OneParamCtor), "hello!");
            }

            [Benchmark(Description = "Activator"), BenchmarkCategory(_twoParamsCtor)]
            public object ActivatorCreateInstance_2()
            {
                return Activator.CreateInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [Benchmark(Description = "Activator"), BenchmarkCategory(_threeParamsCtor)]
            public object ActivatorCreateInstance_3()
            {
                return Activator.CreateInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2012

            [Benchmark(Description = "2012"), BenchmarkCategory(_parameterless)]
            public object CreateInstanceFromType_2012_0()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(Parameterless));
            }

            [Benchmark(Description = "2012"), BenchmarkCategory(_oneParamCtor)]
            public object CreateInstanceFromType_2012_1()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [Benchmark(Description = "2012"), BenchmarkCategory(_twoParamsCtor)]
            public object CreateInstanceFromType_2012_2()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [Benchmark(Description = "2012"), BenchmarkCategory(_threeParamsCtor)]
            public object CreateInstanceFromType_2012_3()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2020 Design-Time Args

            [Benchmark(Description = "2020"), BenchmarkCategory(_parameterless)]
            public object CreateInstanceFromType_2020_D0()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(Parameterless));
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_oneParamCtor)]
            public object CreateInstanceFromType_2020_D1()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_twoParamsCtor)]
            public object CreateInstanceFromType_2020_D2()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_threeParamsCtor)]
            public object CreateInstanceFromType_2020_D3()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2020 Runtime Args

            [Benchmark(Description = "2020"), BenchmarkCategory(_parameterless)]
            public object CreateInstanceFromType_2020_R0()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(Parameterless));
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_oneParamCtor)]
            public object CreateInstanceFromType_2020_R1()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_twoParamsCtor)]
            public object CreateInstanceFromType_2020_R2()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [Benchmark(Description = "2020"), BenchmarkCategory(_threeParamsCtor)]
            public object CreateInstanceFromType_2020_R3()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion
        }

        [MemoryDiagnoser]
        public class CreateInstanceFromTypeOneParamCtor
        {
            [Benchmark(Baseline = true)]
            public object New()
            {
                return new OneParamCtor("Hello!");
            }

            [Benchmark]
            public object ActivatorCreateInstance()
            {
                return Activator
                    .CreateInstance(typeof(OneParamCtor), "Hello!");
            }

            [Benchmark]
            public object CreateInstanceFromType_2012()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(OneParamCtor), "Hello!");
            }

            [Benchmark]
            public object CreateInstanceFromType_2020()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(OneParamCtor), "Hello!");
            }
        }

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<CreateInstanceFromTypeBenchmark>();
        }
    }
}
