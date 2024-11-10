using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.Models
{
    class Сarservice
    {
        private List<Part> _partsStorage;
        private Queue<Vehicle> _vehicles;

        private int _money;

        public Сarservice(List<Part> partsStorage)
        {
            _partsStorage = new List<Part>(partsStorage);
            _vehicles = new Queue<Vehicle>();
            _money = 0;
        }

        public List<Vehicle> Vehicles => new List<Vehicle>(_vehicles);

        public int Money => _money;
        public int RepairCost => 100;

        public void AcceptBrokenVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
                _vehicles.Enqueue(vehicle);
        }

        public Vehicle GetFirstVehicle()
        {
            return _vehicles.Dequeue();
        }

        public Part GetAvailablePart(string partName)
        {
            return _partsStorage.Find(part => part.Name == partName);
        }

        public void RemovePart(Part part)
        {
            _partsStorage.Remove(part);
        }

        public void AddMoney(int money)
        {
            _money += money;
        }
    }
}
