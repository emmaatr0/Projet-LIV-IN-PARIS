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
        public string Nom { get; }
        public List<Noeud> Relations { get; }

        public Noeud(int id, string nom)
        {
            Id = id;
            Nom = nom;
            Relations = new List<Noeud>();
        }
        public void AjouterRelation(Noeud autreNoeud)
        {
            if (!Relations.Contains(autreNoeud))
            {
                Relations.Add(autreNoeud);
                autreNoeud.Relations.Add(this); // Relation réciproque
            }
        }
    }
}
