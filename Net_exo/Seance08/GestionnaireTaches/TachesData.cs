using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireTaches
{
    public class TachesData
    {
        private IList<Tache> placesList;

        public TachesData()
        {
            placesList = new List<Tache>();
        }

        public IList<Tache> PlacesList
        {
            get { return placesList; }
        }
    }

    public class Tache
    {
        private string _description;
        private int _priority;
        private DateTime _date;
        private bool _status;

        public Tache(string description, int priority, bool status, DateTime date)
        {
            _description = description;
            _priority = priority;
            _date = date;
            _status = status;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public DateTime Date
        {
            get { return _date; }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

    }
}
