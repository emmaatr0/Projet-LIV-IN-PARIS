using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Classe représentant le graphe
    public class Graphe
    {
        private Dictionary<int, Noeud> noeuds;

        public Graphe()
        {
            noeuds = new Dictionary<int, Noeud>();
        }

        public void AjouterNoeud(int id, string nom)
        {
            if (!noeuds.ContainsKey(id))
            {
                noeuds[id] = new Noeud(id, nom);
            }
        }

        public void AjouterLien(int id1, int id2)
        {
            if (noeuds.ContainsKey(id1) && noeuds.ContainsKey(id2))
            {
                noeuds[id1].AjouterRelation(noeuds[id2]);
            }
        }

        public void AfficherGraphe()
        {
            foreach (var noeud in noeuds.Values)
            {
                Console.Write($"Membre {noeud.Nom} (ID {noeud.Id}) est en relation avec : ");
                foreach (var relation in noeud.Relations)
                {
                    Console.Write($"{relation.Nom} (ID {relation.Id}), ");
                }
                Console.WriteLine();
            }
        }

    }
}

