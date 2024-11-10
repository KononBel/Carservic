using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autoservice.Models;
using Autoservice.Controllers;

namespace Autoservice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutoserviceGame autoserviceGame = new AutoserviceGame(5, 5);

            autoserviceGame.Main();

            while (autoserviceGame.VehicleQueueLength > 0)
                autoserviceGame.Repair();

            autoserviceGame.PrintResults();
        }
    }

    class AutoserviceGame
    {
        private СarserviceController _сarserviceController;

        public AutoserviceGame(int vehiclesCount, int partstorageCountEch)
        {
            List<Part> partsStorage = Parts.GetCarParts(partstorageCountEch);
            _сarserviceController = new СarserviceController(vehiclesCount, partsStorage);
        }

        public int VehicleQueueLength => _сarserviceController.VehicleQueueLength;

        public void Main()
        {
            Console.WriteLine("Добро пожаловать в игру \"Автосерис\"! \n" +
                "Ваша задача ремонтировать автомобили, за что вы будете получать денежное вознаграждение.\n");

            _сarserviceController.ShowQueueVehicle();

            Console.WriteLine("\nНажмите любую клавишу, чтобы начать ремонтировать автомобили.");

            Console.ReadKey();
        }

        public void Repair()
        {
            Vehicle vehicle = _сarserviceController.TakeVehicleOnRepair();

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Console.WriteLine($"Необходимо починить {vehicle.Name}. Выберите действие:\n");

                const string GoRepair = "1";
                const string CompleteRepair = "2";
                const string BreakRepair = "3";

                string choice;

                Console.WriteLine($"{GoRepair}. Приступить к ремонту");
                Console.WriteLine($"{CompleteRepair}. Завершить ремонт");
                Console.WriteLine($"{BreakRepair}. Отказаться от ремонта");
                Console.Write("\nСделайте ваш выбор: ");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case GoRepair:
                        _сarserviceController.RepairVehicle(vehicle);
                        break;

                    case CompleteRepair:
                        _сarserviceController.CompleteRepair(vehicle, ref isWork);
                        break;

                    case BreakRepair:
                        _сarserviceController.BreakRepair(vehicle, ref isWork);
                        break;

                    default:
                        Console.WriteLine("Такого варианта нет! Повторите попытку!");
                        break;
                }

                Console.ReadKey();
            }
        }

        public void PrintResults()
        {
            Console.Write($"\nИгра завершена! Баланс автосервиса составил: ");
            Console.WriteLine(_сarserviceController.RepairResult());
        }
    }

    static class ConsoleReader
    {
        public static int ReadInt(string printMessage)
        {
            int value;

            do
            {
                Console.Write(printMessage);
            }
            while (int.TryParse(Console.ReadLine(), out value) == false);

            return value;
        }

        public static string ReadString(string printMessage)
        {
            string value;

            do
            {
                Console.Write(printMessage);
                value = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(value) == true);

            return value;
        }
    }
}