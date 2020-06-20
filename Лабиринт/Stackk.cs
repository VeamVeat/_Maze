using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лабиринт
{
    class Stackk
    {
        private List<Point> _item = new List<Point>();

        public int Count => _item.Count();

        //Добавление элемента в стэк
        public void Push(Point item)
        {
            Point element;
            if (_item.Count == 0)
                _item.Add(item);

            else
            {
                _item.Add(item);
                element = item;

                for (int i = _item.Count - 2; i >= 0; i--)
                    _item[i + 1] = _item[i];


                _item[0] = element;
            }
        }


        //удаление первого элемента в стэке

        public void Pop()
        {
            //Point element = 
            _item.RemoveAt(0);
          
        }

        //Очищение стэка
        public void ClearStackk()
        {
            _item.Clear();
        }
        List<Point> points;
        //Вывод стэка на экран
        public List<Point> Perebor()
        {
            points = new List<Point>();
            for (int i = 0; i < _item.Count; i++)
            {
                points.Add(_item[i]);
            }
            return points;
        }

        //Чтение первого элемента в стэке
        public Point Peek()
        {
            return _item[0];
        }
    }
}
