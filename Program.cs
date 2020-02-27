using System;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Pasha\OneDrive\Рабочий стол\input.txt";
            Transport[] array;
            try
            {
                array = ReadFile(path);
                foreach (Transport transport in array)
                {
                    transport.Display();
                }
                Serialize(array);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("File is not found");
            }
            catch(SerializationException)
            {
                Console.WriteLine("Some problems with serialization");
            }

        }

        public static void Serialize(Transport[] array)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Transport[]));
            using (FileStream fs = new FileStream("transport.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, array);
            }
        }
        public static Transport[] ReadFile(string pathIn)
        {
            using (StreamReader sr = new StreamReader(pathIn, System.Text.Encoding.Default))
            {

                int n = Convert.ToInt32(sr.ReadLine());
                int count = 0;
                Transport[] arr = new Transport[n];
                string line;
                string[] splitLine;
                while ((line = sr.ReadLine()) != null)
                {
                    splitLine = line.Split(" ");
                    switch(splitLine[0])
                    {
                        case "passengercar":
                            arr[count] = new PassengerCar(splitLine[1], splitLine[2], Convert.ToDouble(splitLine[3]), Convert.ToDouble(splitLine[4]));
                            count++;
                            break;
                        case "truck":
                            arr[count] = new Truck(splitLine[1], splitLine[2], Convert.ToDouble(splitLine[3]), Convert.ToDouble(splitLine[4]), bool.Parse(splitLine[5]));
                            count++;
                            break;
                        case "motorcycle":
                            arr[count] = new Motorcycle(splitLine[1], splitLine[2], Convert.ToDouble(splitLine[3]), Convert.ToDouble(splitLine[4]), bool.Parse(splitLine[5]));
                            count++;
                            break;
                    }
                }

                return arr;
            }
        }
    }
    [XmlInclude(typeof(PassengerCar))]
    [XmlInclude(typeof(Truck))]
    [XmlInclude(typeof(Motorcycle))]
    [Serializable]
    public abstract class Transport
    {
        public string model { get; set; }
        public string number { get; set; }
        public double speed { get; set; }
        public double capacity { get; set; }

        public Transport()
        {}

        public Transport(string model, string number, double speed, double capacity)
        {
            this.model = model;
            this.number = number;
            this.speed = speed;
            this.capacity = capacity;
        }

        public abstract void Display();
    }

    [Serializable]
    public class PassengerCar : Transport
    {
        public PassengerCar()
        {}

        public PassengerCar(string model, string number, double speed, double capacity) : base(model, number, speed, capacity)
        {} 

        public override void Display()
        {
            Trace.WriteLine("PassengerCar.Display was called");
            Console.Write("Passenger Car \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + "\n");
        }
           
    }

    [Serializable]
    public class Motorcycle : Transport
    {
        public bool hasCarriage { get; set; }
        public Motorcycle()
        { }

        public Motorcycle(string model, string number, double speed, double capacity, bool hasCarriage) : base(model, number, speed, capacity)
        {
            this.hasCarriage = hasCarriage;
            if (this.hasCarriage == false)
            {
                Trace.WriteLine("Motorcycle`s capacity was changed due to lack of carriage");
                this.capacity = 0;
            }
        }

        public override void Display()
        {
            Trace.WriteLine("Motorcycle.Display was called");
            Console.WriteLine("Motorcycle \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + ", has carriage: " + hasCarriage);
        }
    }

    [Serializable]
    public class Truck : Transport
    {
        public bool hasTrailer { get; set; }

        public Truck()
        { }

        public Truck(string model, string number, double speed, double capacity, bool hasTrailer) : base(model, number, speed, capacity)
        {
            this.hasTrailer = hasTrailer;
            if (this.hasTrailer == true)
            {
                Trace.WriteLine("Truck`s capacity was changed due to lack of trailer");
                this.capacity *= 2;
            }
        }

        public override void Display()
        {
            Trace.WriteLine("Truck.Display was called");
            Console.WriteLine("Truck \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + ", has carriage: " + hasTrailer);
        }
    }
}
