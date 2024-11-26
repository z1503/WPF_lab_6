using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibrary
{
    public class Tourist
    {
        public string Name { get; set; }
        public double Money { get; private set; }
        public int LandmarksPerDay { get; set; }

        public Tourist(string name, double money, int landmarksPerDay)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя не может быть пустым.");

            if (money < 0)
                throw new ArgumentException("Сумма денег не может быть отрицательной.");

            if (landmarksPerDay <= 0)
                throw new ArgumentException("Количество достопримечательностей в день должно быть больше 0.");

            Name = name;
            Money = money;
            LandmarksPerDay = landmarksPerDay;
        }

        public void SpendMoney(double amount)
        {
            if (amount > Money)
                throw new InvalidOperationException("Недостаточно денег для посещения города.");

            Money -= amount;
        }
    }
}
