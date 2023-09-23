using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace be.ipl.domaine;

    internal class Actor : Person
    {

        private readonly int _sizeInCentimeter;
        private List<Movie> _movies;

        public int SizeInCentimeter
        {
            get { return _sizeInCentimeter; }
        }

        // to redefine a properties -> virtual in parent class + override in class child (here)
        public override string Name
        {
            get { return base.Name.ToUpper(); }
        }

        public Actor(string name, string firstname, DateTime birthDate, int sizeInCentimeter) : base(name, firstname, birthDate)
        {
            this._sizeInCentimeter = sizeInCentimeter;
            this._movies = new List<Movie>();
        }

        public override String ToString()
        {
            return "Actor [name = " + Name + ", firstname = " + Firstname + ", sizeInCentimeter = " + _sizeInCentimeter + ", birthdate = " + BirthDate + "]";
        }

        /**
	        * 
	        * @return list of movies in which the actor has played
	        */
        public IEnumerator<Movie> Movies()
        {
            return _movies.GetEnumerator();
        }

        /**
	        * add movie to the list of movies in which the actor has played
	        * @param movie
	        * @return false if the movie is null or already present
	        */
        public bool AddMovie(Movie movie)
        {
            if ((movie == null) || _movies.Contains(movie))
                return false;

            if (!movie.ContainsActor(this))
                movie.AddActor(this);

            _movies.Add(movie);

            return true;
        }

        public bool ContainsMovie(Movie movie)
        {
            return _movies.Contains(movie);
        }
    }
