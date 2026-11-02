using System;
using System.IO;

namespace Lab3
{
    public struct BaggageItem
    {
        public string Name;
        public double Weight;
    }

    public struct PassengerBaggage
    {
        public string PassengerName;
        public BaggageItem[] Items;
    }
    public static class BinaryTask
    {
        public static void WriteUserBinaryFile(
            string filePath, int[] numbers)
        {
            using (BinaryWriter writer = 
                new BinaryWriter(File.Open(
                    filePath, FileMode.Create)))
            {
                foreach (int num in numbers)
                {
                    writer.Write(num);
                }
            }
        }

        public static void PrintBinaryFile(
            string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(
                    "Файл не найден!");
                return;
            }

            using (BinaryReader reader = 
                new BinaryReader(File.Open(
                    filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < 
                       reader.BaseStream.Length)
                {
                    int val = reader.ReadInt32();
                    Console.Write($"{val} ");
                }
                Console.WriteLine();
            }
        }

        public static int CountOppositePairs(
            string filePath)
        {
            if (!File.Exists(filePath)) 
                return 0;

            int[] numbers;
            using (BinaryReader reader = 
                new BinaryReader(File.Open(
                    filePath, FileMode.Open)))
            {
                int count = (int)(
                    reader.BaseStream.Length / 
                    sizeof(int));
                numbers = new int[count];
                for (int i = 0; i < count; i++)
                {
                    numbers[i] = 
                        reader.ReadInt32();
                }
            }

            int pairCount = 0;
            bool[] counted = 
                new bool[numbers.Length];

            for (int i = 0; i < 
                 numbers.Length; i++)
            {
                if (counted[i]) continue;

                for (int j = i + 1; j < 
                     numbers.Length; j++)
                {
                    if (!counted[j] && 
                        numbers[i] + 
                        numbers[j] == 0 && 
                        numbers[i] != 0)
                    {
                        pairCount++;
                        counted[j] = true;
                        break;
                    }
                }
            }

            return pairCount;
        }

        public static void WriteBaggageBinaryFile(
            string filePath, 
            PassengerBaggage[] passengers)
        {
            using (BinaryWriter writer = 
                new BinaryWriter(
                    File.Open(
                        filePath, 
                        FileMode.Create)))
            {
                writer.Write(passengers.Length);

                for (int i = 0; 
                     i < passengers.Length; 
                     i++)
                {
                    writer.Write(
                        passengers[i].PassengerName);
                    
                    writer.Write(
                        passengers[i].Items.Length);

                    for (int j = 0; 
                         j < passengers[i].Items.Length; 
                         j++)
                    {
                        writer.Write(
                            passengers[i].Items[j].Name);
                        writer.Write(
                            passengers[i].Items[j].Weight);
                    }
                }
            }
        }

        public static PassengerBaggage[] ReadBaggageBinaryFile(
            string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new PassengerBaggage[0];
            }

            using (BinaryReader reader = 
                new BinaryReader(
                    File.Open(
                        filePath, 
                        FileMode.Open)))
            {
                int passengerCount = 
                    reader.ReadInt32();
                PassengerBaggage[] passengers = 
                    new PassengerBaggage[passengerCount];

                for (int i = 0; 
                     i < passengerCount; 
                     i++)
                {
                    passengers[i].PassengerName = 
                        reader.ReadString();

                    int itemCount = 
                        reader.ReadInt32();
                    passengers[i].Items = 
                        new BaggageItem[itemCount];

                    for (int j = 0; 
                         j < itemCount; 
                         j++)
                    {
                        passengers[i].Items[j].Name = 
                            reader.ReadString();
                        passengers[i].Items[j].Weight = 
                            reader.ReadDouble();
                    }
                }

                return passengers;
            }
        }

        public static void CalculateBaggageDifference(
            string filePath, 
            out double maxWeight, 
            out double minWeight)
        {
            PassengerBaggage[] passengers = 
                ReadBaggageBinaryFile(filePath);

            maxWeight = double.MinValue;
            minWeight = double.MaxValue;

            if (passengers.Length == 0)
            {
                maxWeight = 0;
                minWeight = 0;
                return;
            }

            for (int i = 0; 
                 i < passengers.Length; 
                 i++)
            {
                BaggageItem[] items = 
                    passengers[i].Items;

                if (items == null) 
                {
                    continue;
                }

                for (int j = 0; 
                     j < items.Length; 
                     j++)
                {
                    double currentWeight = 
                        items[j].Weight;

                    if (currentWeight > maxWeight)
                    {
                        maxWeight = currentWeight;
                    }

                    if (currentWeight < minWeight)
                    {
                        minWeight = currentWeight;
                    }
                }
            }

            if (maxWeight == double.MinValue) 
            {
                maxWeight = 0;
            }
            if (minWeight == double.MaxValue) 
            {
                minWeight = 0;
            }
        }
    }
}