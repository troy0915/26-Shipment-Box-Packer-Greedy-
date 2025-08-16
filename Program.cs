using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Box
{
    public double Capacity { get; }
    public List<double> Items { get; } = new List<double>();
    public double CurrentWeight => Items.Sum();

    public Box(double capacity)
    {
        Capacity = capacity;
    }

    public bool TryAdd(double weight)
    {
        if (CurrentWeight + weight <= Capacity)
        {
            Items.Add(weight);
            return true;
        }
        return false;
    }

    public double FillRatio => CurrentWeight / Capacity;
}


namespace _26__Shipment_Box_Packer__Greedy_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var itemWeights = new List<double> { 4.5, 2, 6, 3.2, 5, 1.5, 2.8, 4 };
            double boxCapacity = 10;
            string strategy = "bestfit"; 

            var boxes = PackItems(itemWeights, boxCapacity, strategy);

            Console.WriteLine($"Strategy: {strategy}");
            Console.WriteLine($"Box Capacity: {boxCapacity}");
            Console.WriteLine();

            double totalPacked = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                var box = boxes[i];
                totalPacked += box.CurrentWeight;
                Console.WriteLine($"Box {i + 1}: Items = {string.Join(", ", box.Items)} | Total = {box.CurrentWeight} | Fill Ratio = {box.FillRatio:P1}");
            }

            Console.WriteLine($"\nTotal weight packed: {totalPacked}");
            Console.WriteLine($"Sum of item weights: {itemWeights.Sum()}");
        }

        public static List<Box> PackItems(List<double> items, double capacity, string strategy)
        {
            var boxes = new List<Box>();

            foreach (var item in items)
            {
                Box chosenBox = null;

                switch (strategy.ToLower())
                {
                    case "firstfit":
                        chosenBox = boxes.FirstOrDefault(b => b.TryAdd(item));
                        if (chosenBox == null)
                        {
                            chosenBox = new Box(capacity);
                            chosenBox.TryAdd(item);
                            boxes.Add(chosenBox);
                        }
                        break;

                    case "bestfit":
                        chosenBox = boxes
                            .Where(b => b.CurrentWeight + item <= capacity)
                            .OrderBy(b => capacity - (b.CurrentWeight + item))
                            .FirstOrDefault();

                        if (chosenBox == null)
                        {
                            chosenBox = new Box(capacity);
                            chosenBox.TryAdd(item);
                            boxes.Add(chosenBox);
                        }
                        else
                        {
                            chosenBox.TryAdd(item);
                        }
                        break;

                    default:
                        throw new ArgumentException("Unknown strategy. Use 'firstfit' or 'bestfit'.");
                }
            }

            return boxes;
        }
    }
}




