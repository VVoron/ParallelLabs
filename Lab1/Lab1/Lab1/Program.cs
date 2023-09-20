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

    public void SumWithThreads(List<int> fVector, List<int> sVector,int numThreads)
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Задача с кол-вом потоков - {numThreads}");

        var threadsList = new List<Thread>();

        var numsForOneThread = fVector.Count / numThreads;

        var results = new int[numThreads];

        var startTime = DateTime.Now;

        for (int i = 0; i < numThreads; i++)
        {
            int localIndex = i; // Создаем локальную копию переменной i
            threadsList.Add(new Thread(() =>
            {
                results[localIndex] += Sum(fVector, sVector, numsForOneThread, localIndex);
            }));
            threadsList[i].Start();
        }

        foreach (var thread in threadsList)
            thread.Join();

        var endTime = DateTime.Now;

        Console.WriteLine($"Времени на выполнение: {(endTime - startTime).TotalMilliseconds} ms");
        Console.WriteLine($"Результат: {results.Sum()}");
    }

    private int Sum(List<int> fVector, List<int> sVector, int num, int threadIndex)
    {
        int startIndex = num * threadIndex;
        int endIndex = startIndex + num;

        int sum = 0;

        for (int i = startIndex; i < endIndex; i++)
            sum += fVector[i] + sVector[i];

        Console.WriteLine($"Поток {threadIndex} завершил свою работу");

        return sum;
    }

}