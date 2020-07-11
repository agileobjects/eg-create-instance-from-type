# Creating an Instance of a Type at Runtime

This repo demonstrates various ways to create an instance of a type at runtime, and includes a 
[Benchmark.NET](https://benchmarkdotnet.org) project to measure and compare their performance. All code
is [MIT-licensed](https://github.com/agileobjects/eg-create-instance-from-type/blob/master/LICENSE).

## Scenarios

Two scenarios are considered:

1. You need to create an instance of a type, and you have the appropriately-typed arguments for that 
   type's constructor at [design-time](https://stackoverflow.com/questions/2621976/run-time-vs-design-time).
   This scenario allows the code to use a generic method to get the argument types, in order to find 
   a matching constructor.

2. You need to create an instance of a type, and you have the constructor arguments all typed as 
   `object`. This scenario requires the code to call `GetType()` on each argument, in order to find 
   a matching constructor.

For each scenario, parameterless, 1-, 2- and 3-parameter constructor objects are created. In the 
3-parameter case [the type](https://github.com/agileobjects/eg-create-instance-from-type/blob/master/CreateInstanceFromType.Tests/TestClasses/MultiCtor.cs)
has multiple constructors, and the code has to pick the correct one.

## Methods

The following methods are measured and compared:

1. Using `new`. This is used as the design-time [baseline](https://benchmarkdotnet.org/articles/features/baselines.html).

2. Using a [design-time method](https://github.com/agileobjects/eg-create-instance-from-type/blob/master/CreateInstanceFromType/CreateInstanceFromType2012.cs) 
   I wrote about [in 2012](https://agileobjects.co.uk/fast-csharp-expression-tree-create-instance-from-type-extension-method).

3. Using an updated [design-time method](https://github.com/agileobjects/eg-create-instance-from-type/blob/master/CreateInstanceFromType/CreateInstanceFromType2020DesignTimeArgs.cs)
   I wrote in 2020.

4. Using [`Activator.CreateInstance`](https://docs.microsoft.com/en-us/dotnet/api/system.activator.createinstance),
   a runtime method in the Base Class Libraries.

5. Using a [runtime method](https://github.com/agileobjects/eg-create-instance-from-type/blob/master/CreateInstanceFromType/CreateInstanceFromType2020RuntimeArgs.cs)
   I wrote in 2020.

## Results

Example benchmarking results for .NET Core 3.1 are shown below:

![Benchmarking Results](/Results.png)

To note:

1. My 2020 design-time code is between **1.5** and **2.7** times **faster** than my 2012 code.

2. My 2020 runtime code is between **6.6** and **6.9** times **faster** than `Activator.CreateInstance` for all 
   except the parameterless case. It's **1.2** times **slower** for parameterless constructors.

Further discussion can be found on [my blog](https://agileobjects.co.uk/create-instance-of-type-net-core).