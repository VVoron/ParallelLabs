﻿const int N = 10000000;

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

    public void SumWithThreads(List<int> fVector, List<int> sVector,int numThreads)
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Задача с кол-вом потоков - {numThreads}");

        var threadsList = new List<Thread>();

        var numsForOneThread = fVector.Count / numThreads;

        var results = new int[fVector.Count];

        var startTime = DateTime.Now;

        for (int i = 0; i < numThreads; i++)
        {
            int startIndex = i * numsForOneThread;
            int endIndex = (i + 1) * numsForOneThread;
            threadsList.Add(new Thread(() =>
            {
                for (int index = startIndex; index < endIndex; index++)
                    results[index] = fVector[index] + sVector[index];
            }));
            
            threadsList[i].Start();
        }

        foreach (var thread in threadsList)
            thread.Join();

        var endTime = DateTime.Now;

        Console.WriteLine($"Времени на выполнение: {(endTime - startTime).TotalMilliseconds} ms");
        Console.WriteLine($"Результат: {results.Sum()}");
    }
}