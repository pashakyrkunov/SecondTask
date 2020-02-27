using System;
using System.IO;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Pasha\OneDrive\Рабочий стол\input.txt";
            ReadFile(path);
        }

        public static void ReadFile(string pathIn)
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

                foreach (Transport element in arr)
                {
                    element.Display();
                }
            }
        }
    }

    abstract class Transport
    {   
        protected string model;
        protected string number;
        protected double speed;
        protected double capacity;

        public Transport(string model, string number, double speed, double capacity)
        {
            this.model = model;
            this.number = number;
            this.speed = speed;
            this.capacity = capacity;
        }

        public abstract void Display();
    }

    class PassengerCar : Transport
    {
        public PassengerCar(string model, string number, double speed, double capacity) : base(model, number, speed, capacity)
        {
        }

        public override void Display()
        {
            Console.Write("Passenger Car \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + "\n");
        }
           
    }

    class Motorcycle : Transport
    {
        private bool hasCarriage;

        public Motorcycle(string model, string number, double speed, double capacity, bool hasCarriage) : base(model, number, speed, capacity)
        {
            this.hasCarriage = hasCarriage;
            if (this.hasCarriage == false)
            {
                this.capacity = 0;
            }
        }

        public override void Display()
        {
            Console.WriteLine("Motorcycle \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + ", has carriage: " + hasCarriage);
        }
    }

    class Truck : Transport
    {
        private bool hasTrailer;
        public Truck(string model, string number, double speed, double capacity, bool hasTrailer) : base(model, number, speed, capacity)
        {
            this.hasTrailer = hasTrailer;
            if (this.hasTrailer == true)
            {
                this.capacity *= 2;
            }
        }

        public override void Display()
        {
            Console.WriteLine("Truck \n" + "model: " + model + ", number: " + number + ", speed: " + speed + ", capacity: " + capacity + ", has carriage: " + hasTrailer);
        }
    }
}
