using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafo
{
    public class RutinaConfiguracion : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private List<PointPlainObject> puntos = new List<PointPlainObject>();
        private List<LinePlainObject> lineas = new List<LinePlainObject>();
        private LinePlainObject linea = new LinePlainObject();
        private int num;

        public List<PointPlainObject> Puntos
        {
            get { return puntos; }
            set { puntos = value; NotifyPropertyChanged("Puntos"); }
        }
        public List<LinePlainObject> Lineas
        {
            get { return lineas; }
            set { lineas = value; NotifyPropertyChanged("Lineas"); }
        }
        public LinePlainObject Linea
        {
            get { return linea; }
            set { linea = value; NotifyPropertyChanged("Linea"); }
        }

        public int Num
        {
            get { return num; }
            set { num = value; addRemovePoints(); NotifyPropertyChanged("Num"); }
        }

        public RutinaConfiguracion() { num = 0; }
        public RutinaConfiguracion(int n) { num = n; }

        public void addRemovePoints()
        {
            Random r = new Random();
            if (Puntos.Count <= num)
            {
                int c = Puntos.Count;
                for (int i = 0; i < num - c; i++)
                {
                    while (true)
                    {
                        int x = r.Next(0, 50);
                        int y = r.Next(0, 50);
                        if (Puntos.Where(p => p.x == x && p.y == y).ToList().Count == 0)
                        {
                            Puntos.Add(new PointPlainObject() { id = Puntos.Count+1, x = x, y = y });
                            break;
                        }
                    }
                }
            }
            else
                while (Puntos.Count > num)
                {
                    var p = Puntos[Puntos.Count - 1];
                    if (Lineas != null && Lineas.Count > 0)
                    {
                        var coincide = Lineas.Where(l => l.id1 == p.id || l.id2 == p.id).Select(l => l).ToList();
                        foreach (LinePlainObject _l in coincide)
                            Lineas.Remove(_l);
                    }
                    Puntos.Remove(p);
                }
            num = num;
        }
    }
    public class PointPlainObject
    {
        public int x { get; set; }
        public int y { get; set; }
        public int id { get; set; }
        public string display { get { return id + ":" + "(" + x + "," + y + ")"; } }
    }
    public class LinePlainObject
    {
        public string otro { get; set; }
        public int id1 { get { return uno.id; } }
        public int id2 { get { return dos.id; } }
        private PointPlainObject _uno = new PointPlainObject();
        private PointPlainObject _dos = new PointPlainObject();

        public PointPlainObject dos
        {
            get { return _dos; }
            set { _dos = value; }
        }
        public PointPlainObject uno
        {
            get { return _uno; }
            set { _uno = value; }
        }
    }
}
