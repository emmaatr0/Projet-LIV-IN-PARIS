using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp4
namespace ConsoleApp4
{
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

