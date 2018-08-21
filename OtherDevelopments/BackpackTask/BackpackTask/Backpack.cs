using System.Collections.Generic;

namespace BackpackTask
{
    class Backpack
    {
        private List<Item> bestItems = null;

        private double maxW;

        private double bestPrice;

        public Backpack(double _maxW)
        {
            maxW = _maxW;
        }

        //создание всех наборов перестановок значений
        public void MakeAllSets(List<Item> items)
        {
            if (items.Count > 0)
                CheckSet(items);

            for (int i = 0; i < items.Count; i++)
            {
                List<Item> newSet = new List<Item>(items);

                newSet.RemoveAt(i);

                MakeAllSets(newSet);
            }

        }

        //проверка, является ли данный набор лучшим решением задачи
        private void CheckSet(List<Item> items)
        {
            if (bestItems == null)
            {
                if (CalcWeigth(items) <= maxW)
                {
                    bestItems = items;
                    bestPrice = CalcPrice(items);
                }
            }
            else
            {
                if(CalcWeigth(items) <= maxW && CalcPrice(items) > bestPrice)
                {
                    bestItems = items;
                    bestPrice = CalcPrice(items);
                }
            }
        }

        //вычисляет общий вес набора предметов
        private double CalcWeigth(List<Item> items)
        {
            double sumW = 0;

            foreach(Item i in items)
            {
                sumW += i.weigth;
            }

            return sumW;
        }

        //вычисляет общую стоимость набора предметов
        private double CalcPrice(List<Item> items)
        {
            double sumPrice = 0;

            foreach (Item i in items)
            {
                sumPrice += i.price;
            }

            return sumPrice;
        }

        //возвращает решение задачи (набор предметов)
        public List<Item> GetBestSet()
        {
            return bestItems;
        }
    }

}