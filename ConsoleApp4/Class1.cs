using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    using System;
    using System.Collections.Generic;

    // Classe représentant un membre de l'association


    public class Noeud
    {
        public int Id { get; }
        public List<Noeud> Relations { get; }

        public Noeud(int id)
        {
            Id = id;
            Relations = new List<Noeud>();
        }

        // Ajoute une relation si elle n'existe pas déjà
        public void AjouterRelation(Noeud autreNoeud)
        {
            if (!Relations.Contains(autreNoeud))
            {
                Relations.Add(autreNoeud);
            }
        }
    }
    }



