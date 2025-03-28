using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp4;

namespace ConsoleApp4
{


    public class Noeud<T> 
    {
        public T Id { get; }
        public List<Lien<T>> Relations { get; }

        public Noeud(T id)
        {
            Id = id;
            Relations = new List<Lien<T>>();
        }

        public void AjouterRelation(Noeud<T> autreNoeud, int poids = 1)
        {
            if (!Relations.Any(l => l.Destination.Equals(autreNoeud)))
            {
                Relations.Add(new Lien<T>(this, autreNoeud, poids));
            }
        }
    }

    public class Lien<T>
    {
        public Noeud<T> Source { get; }
        public Noeud<T> Destination { get; }
        public int Poids { get; }

        public Lien(Noeud<T> source, Noeud<T> destination, int poids)
        {
            Source = source;
            Destination = destination;
            Poids = poids;
        }


    }
}



