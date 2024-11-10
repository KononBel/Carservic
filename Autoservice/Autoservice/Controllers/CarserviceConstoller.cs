using Autoservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Controllers
{
    class СarserviceController
    {
        private Сarservice _carservice;

        private Random _random;

        private string[] _vehiclesNames;

        private int _temporaryCash;

        public СarserviceController(int vehiclesCount, List<Part> partsStorage)
        {
            _carservice = new Сarservice(partsStorage);
            _random = new Random(Guid.NewGuid().GetHashCode());

            _temporaryCash = 0;

            _vehiclesNames = new string[] { "Audi", "Ford", "Porsche", "Lada", "BMW",
                "Jeep", "Hummer", "Cadillac", "Lincoln", "Toyota" };

            CreateQueueVehicle(vehiclesCount);
        }

        public int VehicleQueueLength => _carservice.Vehicles.Count;

        public Vehicle TakeVehicleOnRepair()
        {
            return _carservice.GetFirstVehicle();
        }

        public void ShowQueueVehicle()
        {
            Console.WriteLine("В данный момент в очереди на ремонт находятся следующие автомобили:\n");

            foreach (Vehicle vehicle in _carservice.Vehicles)
                Console.WriteLine($"{vehicle.Name}");
        }

        public void ShowVehicleInformation(Vehicle vehicle)
        {
            Console.WriteLine($"\nТехническое состояние {vehicle.Name}: \n");

            int partNumber = 1;

            foreach (Part part in vehicle.Parts)
            {
                string brokenStatusName = (part.IsBroken) ? "Сломана" : "Целая";

                Console.WriteLine($"{partNumber++}. {part.Name}: {brokenStatusName}");
            }
        }

        public void RepairVehicle(Vehicle vehicle)
        {
            ShowVehicleInformation(vehicle);

            int partNumber = ConsoleReader.ReadInt("\nВведите номер детали, которую хотите заменить: ");
            
            if (partNumber > 0 && partNumber <= vehicle.Parts.Count)
            {
                Part part = vehicle.Parts[partNumber - 1];

                if (part != null)
                {
                    Part newPart = _carservice.GetAvailablePart(part.Name);
                    if (newPart != null)
                    {
                        if (part.IsBroken)
                            _temporaryCash += part.Cost + _carservice.RepairCost;

                        vehicle.Repair(part, newPart);
                        _carservice.RemovePart(newPart);

                        Console.WriteLine($"Деталь {part.Name} заменена в {vehicle.Name}");
                    }
                    else
                    {
                        Console.WriteLine($"Заменить {part.Name} не получится, эта деталь отсутсвует на складе.");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Такой детали в {vehicle.Name} не обнаружено.");
            }
        }

        public void CompleteRepair(Vehicle vehicle, ref bool isWork)
        {
            int forfeit = 0;

            foreach (Part part in vehicle.Parts)
            {
                if (part.IsBroken)
                    forfeit += part.Cost / 2;
            }

            Console.WriteLine($"\nВы завершили ремонт {vehicle.Name}.\n" +
                $"Выручка за ремонт составила: {_temporaryCash}. " +
                $"Штраф за неотремонтированные детали составил: {forfeit}");

            _carservice.AddMoney(_temporaryCash - forfeit);

            isWork = false;
        }

        public void BreakRepair(Vehicle vehicle, ref bool isWork)
        {
            int forfeit = 250;

            Console.WriteLine($"\nВы отказались от ремонта {vehicle.Name}.\n" +
                $"Штраф за отказ от ремонта составил: {forfeit}");

            _carservice.AddMoney(-forfeit);

            isWork = false;
        }

        public string RepairResult()
        {
            Console.WriteLine(_carservice.Money);

            if (_carservice.Money > 0)
                return "Вы молодец, продолжайте в том же духе!";
            else
                return "Вы никчемный предприниматель, идите учитесь, а не играйте в игрушки. Бездарь.";
        }

        private void CreateQueueVehicle(int vehiclesCount)
        {
            for (int i = 0; i < vehiclesCount; i++)
            {
                List<Part> parts = Parts.GetCarParts();

                string vehicleName = GetVehicleName();

                Vehicle newVehicle = new Vehicle(vehicleName, new List<Part>(parts));
                newVehicle.BreakDown();

                _carservice.AcceptBrokenVehicle(newVehicle);
            }
        }

        private string GetVehicleName()
        {
            int vehiclesNamesIndex = _random.Next(0, _vehiclesNames.Length);

            return _vehiclesNames[vehiclesNamesIndex];
        }
    }
}