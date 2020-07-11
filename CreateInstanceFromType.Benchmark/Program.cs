namespace CreateInstanceFromType.Benchmark
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Running;
    using Tests.TestClasses;

    public class Program
    {
        [Config(typeof(Config))]
        public class CreateInstanceFromTypeBenchmark
        {
            private class Config : ManualConfig
            {
                public Config()
                {
                    AddDiagnoser(MemoryDiagnoser.Default);
                    AddLogicalGroupRules(BenchmarkLogicalGroupRule.ByCategory);
                    AddColumn(new TagColumn("Params", name => name.Substring(name.LastIndexOf('_') + 1)));
                    AddColumn(new TagColumn("Args", name => name[0] == 'R' ? "Runtime" : "Design-time"));
                }
            }

            private const string _parameterless = "Parameterless";
            private const string _oneParamCtor = "One Param";
            private const string _twoParamsCtor = "Two Params";
            private const string _threeParamsCtor = "Three Params";

            private const string _designTimeArgs = "Design-time args";
            private const string _runtimeArgs = "Runtime args";

            #region New (Design-time Args)

            [BenchmarkCategory(_designTimeArgs, _parameterless)]
            [Benchmark(Baseline = true, Description = "New")]
            public object D_New_0()
            {
                return new Parameterless();
            }

            [BenchmarkCategory(_designTimeArgs, _oneParamCtor)]
            [Benchmark(Baseline = true, Description = "New")]
            public object D_New_1()
            {
                return new OneParamCtor("hello!");
            }

            [BenchmarkCategory(_designTimeArgs, _twoParamsCtor)]
            [Benchmark(Baseline = true, Description = "New")]
            public object D_New_2()
            {
                return new TwoParamCtor("hello!", 123);
            }

            [BenchmarkCategory(_designTimeArgs, _threeParamsCtor)]
            [Benchmark(Baseline = true, Description = "New")]
            public object D_New_3()
            {
                return new MultiCtor("hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2012 (Design-time Args)

            [BenchmarkCategory(_designTimeArgs, _parameterless)]
            [Benchmark(Description = "2012")]
            public object D_2012_CreateInstanceFromType_0()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(Parameterless));
            }

            [BenchmarkCategory(_designTimeArgs, _oneParamCtor)]
            [Benchmark(Description = "2012")]
            public object D_2012_CreateInstanceFromType_1()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [BenchmarkCategory(_designTimeArgs, _twoParamsCtor)]
            [Benchmark(Description = "2012")]
            public object D_2012_CreateInstanceFromType_2()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [BenchmarkCategory(_designTimeArgs, _threeParamsCtor)]
            [Benchmark(Description = "2012")]
            public object D_2012_CreateInstanceFromType_3()
            {
                return CreateInstanceFromType2012
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2020 Design-Time Args

            [BenchmarkCategory(_designTimeArgs, _parameterless)]
            [Benchmark(Description = "2020")]
            public object D_2020_CreateInstanceFromType_0()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(Parameterless));
            }

            [BenchmarkCategory(_designTimeArgs, _oneParamCtor)]
            [Benchmark(Description = "2020")]
            public object D_2020_CreateInstanceFromType_1()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [BenchmarkCategory(_designTimeArgs, _twoParamsCtor)]
            [Benchmark(Description = "2020")]
            public object D_2020_CreateInstanceFromType_2()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [BenchmarkCategory(_designTimeArgs, _threeParamsCtor)]
            [Benchmark(Description = "2020")]
            public object D_2020_CreateInstanceFromType_3()
            {
                return CreateInstanceFromType2020DesignTimeArgs
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region Activator.CreateInstance (Runtime Args)

            [BenchmarkCategory(_runtimeArgs, _parameterless)]
            [Benchmark(Baseline = true, Description = "Activator")]
            public object R_ActivatorCreateInstance_0()
            {
                return Activator.CreateInstance(typeof(Parameterless));
            }

            [BenchmarkCategory(_runtimeArgs, _oneParamCtor)]
            [Benchmark(Baseline = true, Description = "Activator")]
            public object R_ActivatorCreateInstance_1()
            {
                return Activator.CreateInstance(typeof(OneParamCtor), "hello!");
            }

            [BenchmarkCategory(_runtimeArgs, _twoParamsCtor)]
            [Benchmark(Baseline = true, Description = "Activator")]
            public object R_ActivatorCreateInstance_2()
            {
                return Activator.CreateInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [BenchmarkCategory(_runtimeArgs, _threeParamsCtor)]
            [Benchmark(Baseline = true, Description = "Activator")]
            public object R_ActivatorCreateInstance_3()
            {
                return Activator.CreateInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion

            #region 2020 Runtime Args

            [BenchmarkCategory(_runtimeArgs, _parameterless)]
            [Benchmark(Description = "2020")]
            public object R_2020_CreateInstanceFromType_0()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(Parameterless));
            }

            [BenchmarkCategory(_runtimeArgs, _oneParamCtor)]
            [Benchmark(Description = "2020")]
            public object R_2020_CreateInstanceFromType_1()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(OneParamCtor), "hello!");
            }

            [BenchmarkCategory(_runtimeArgs, _twoParamsCtor)]
            [Benchmark(Description = "2020")]
            public object R_2020_CreateInstanceFromType_2()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(TwoParamCtor), "hello!", 123);
            }

            [BenchmarkCategory(_runtimeArgs, _threeParamsCtor)]
            [Benchmark(Description = "2020")]
            public object R_2020_CreateInstanceFromType_3()
            {
                return CreateInstanceFromType2020RuntimeArgs
                    .GetInstance(typeof(MultiCtor), "hello!", 123, DateTime.MinValue);
            }

            #endregion
        }

        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<CreateInstanceFromTypeBenchmark>();
        }
    }
}
