using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    
    /// <summary>
    /// Classe représentant un membre de l'association
    /// </summary>


    public class Noeud
    {
        public int Id { get; }
        public List<Noeud> Relations { get; }

        public Noeud(int id)
        {
            Id = id;
            Relations = new List<Noeud>();
        }

        /// <summary>
        /// Ajoute une relation si elle n'existe pas déjà
        /// </summary>
        /// <param name="autreNoeud"></param>
        public void AjouterRelation(Noeud autreNoeud)
        {
            if (!Relations.Contains(autreNoeud))
            {
                Relations.Add(autreNoeud);
            }
        }
    }
    }



