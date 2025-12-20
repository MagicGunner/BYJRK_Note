<Query Kind="Statements">
  <NuGetReference>BenchmarkDotNet</NuGetReference>
  <Namespace>BenchmarkDotNet.Attributes</Namespace>
  <Namespace>BenchmarkDotNet.Engines</Namespace>
  <Namespace>BenchmarkDotNet.Running</Namespace>
</Query>

BenchmarkRunner.Run<ListRangeBenchmarker>();

public class ListRangeBenchmarker() {
	private List<int> testList = Enumerable.Range(1, 100).ToList();
	
	private Consumer comsumer = new();
	
	[Benchmark]
	public void SkipTake() {
		testList.Skip(50).Take(10).Consume(comsumer);
	}
	
	[Benchmark]
	public void GetRange(){
		testList.GetRange(50, 10);
	}

}