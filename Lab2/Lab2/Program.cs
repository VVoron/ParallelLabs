using System.Numerics;

const int N = 10000000;

var labFunc = new LabFunctions();

var A = labFunc.CreateVector(N);
var B = labFunc.CreateVector(N);

labFunc.SumWithThreads(A, B, 1);
labFunc.SumWithThreads(A, B, 2);
labFunc.SumWithThreads(A, B, 4);

public class LabFunctions
{
    Random rand;
    public LabFunctions()
    {
        rand = new Random();
    }

    public List<int> CreateVector(int n)
    {
        List<int> vector = new List<int>();
        for (int i = 0; i < n; i++)
        {
            vector.Add(rand.Next(0, 100));
        }
        return vector;
    }

    public void SumWithThreads(List<int> fVector, List<int> sVector, int numThreads)
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Задача с кол-вом потоков - {numThreads}");

        var results = new int[fVector.Count];

        var numsForOneThread = fVector.Count / numThreads;

        var startTime = DateTime.Now;

        Parallel.For(0, numThreads, (i, x) =>
        {
            for (int index = i * numsForOneThread; index < (i + 1) * numsForOneThread; index++)
            {
                results[index] = fVector[index] + sVector[index];
            }
        });

        var endTime = DateTime.Now;

        Console.WriteLine($"Времени на выполнение: {(endTime - startTime).TotalMilliseconds} ms");
        Console.WriteLine($"Результат: {results.Sum()}");
    }
}