using System;
using System.Collections.Generic;

namespace First_Fit
{
    public class MemoryBlock
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public bool IsAllocated { get; set; }
        public int? ProcessId { get; set; }

        public MemoryBlock(int id, int size)
        {
            Id = id;
            Size = size;
            IsAllocated = false;
            ProcessId = null;
        }

        public override string ToString()
        {
            return $"Block {Id} | Size: {Size} | Allocated: {IsAllocated} | Process: {ProcessId}";
        }
    }

    public class Process
    {
        public int Id { get; set; }
        public int Size { get; set; }

        public Process(int id, int size)
        {
            Id = id;
            Size = size;
        }

        public override string ToString()
        {
            return $"Process {Id} | Size: {Size}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<MemoryBlock> memoryBlocks = new List<MemoryBlock>();
            int blockCount = GetValidInteger("Enter the number of memory blocks: ");

            for (int i = 1; i <= blockCount; i++)
            {
                int size = GetValidInteger($"Enter size of memory block {i}: ");
                memoryBlocks.Add(new MemoryBlock(i, size));
            }

            List<Process> processes = new List<Process>();
            int processCount = GetValidInteger("Enter the number of processes: ");

            for (int i = 1; i <= processCount; i++)
            {
                int size = GetValidInteger($"Enter size of process {i}: ");
                processes.Add(new Process(i, size));
            }

            Console.WriteLine("\nInitial Memory Blocks:");
            PrintMemoryBlocks(memoryBlocks);

            Console.WriteLine("\nAllocating Processes using First Fit Algorithm:\n");

            foreach (var process in processes)
            {
                bool allocated = false;
                foreach (var block in memoryBlocks)
                {
                    if (!block.IsAllocated && block.Size >= process.Size)
                    {
                        block.IsAllocated = true;
                        block.ProcessId = process.Id;
                        allocated = true;
                        Console.WriteLine($"Process {process.Id} allocated to Block {block.Id}");
                        break;
                    }
                }

                if (!allocated)
                {
                    Console.WriteLine($"Process {process.Id} could not be allocated (Insufficient memory).");
                }
            }

            Console.WriteLine("\nFinal Memory Blocks:");
            PrintMemoryBlocks(memoryBlocks);
        }

        static int GetValidInteger(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value) && value >= 0)
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }

        static void PrintMemoryBlocks(List<MemoryBlock> blocks)
        {
            foreach (var block in blocks)
            {
                Console.WriteLine(block);
            }
        }
    }
}
