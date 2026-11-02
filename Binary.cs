using System;
using System.IO;
using System.Xml.Serialization;

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
                int val;
                while (reader.BaseStream.Position < 
                       reader.BaseStream.Length)
                {
                    val = reader.ReadInt32();
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
            XmlSerializer serializer = new XmlSerializer(typeof(PassengerBaggage[]));
            
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, passengers);
            }
        }

        public static PassengerBaggage[] ReadBaggageBinaryFile(
            string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new PassengerBaggage[0];
            }

            XmlSerializer serializer = new XmlSerializer(typeof(PassengerBaggage[]));
           
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (PassengerBaggage[])serializer.Deserialize(fs);
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
