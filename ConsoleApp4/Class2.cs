using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Classe représentant le graphe
    public class Graphe
    {
        private Dictionary<int, Noeud> listeAdjacence; // Stocke les relations sous forme de liste
        private int[,] matriceAdjacence; // Stocke les relations sous forme de matrice
        private int taille; // Nombre total de membres

        public Graphe(int taille)
        {
            this.taille = taille; // Correction ici
            listeAdjacence = new Dictionary<int, Noeud>();
            matriceAdjacence = new int[taille + 1, taille + 1]; // +1 pour ignorer l'index 0
        }

        // Ajoute un nœud au graphe s'il n'existe pas déjà
        public void AjouterNoeud(int id)
        {
            if (!listeAdjacence.ContainsKey(id))
            {
                listeAdjacence[id] = new Noeud(id);
            }
        }

        public void AjouterLien(int id1, int id2)
        {
            AjouterNoeud(id1);
            AjouterNoeud(id2);

            // Ajouter les relations entre les nœuds
            listeAdjacence[id1].AjouterRelation(listeAdjacence[id2]);
            listeAdjacence[id2].AjouterRelation(listeAdjacence[id1]);

            // Mettre à jour la matrice d'adjacence
            matriceAdjacence[id1, id2] = 1;
            matriceAdjacence[id2, id1] = 1;
        }

        public void AfficherListeAdjacence()
        {
            Console.WriteLine("Liste d'adjacence:");
            foreach (var noeud in listeAdjacence)
            {
                Console.Write($"{noeud.Key}: ");
                foreach (var voisin in noeud.Value.Relations)
                {
                    Console.Write($"{voisin.Id} "); // Afficher l'identifiant du nœud
                }
                Console.WriteLine();
            }
        }

        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("Matrice d'adjacence:");
            for (int i = 1; i <= taille; i++)
            {
                for (int j = 1; j <= taille; j++)
                {
                    Console.Write(matriceAdjacence[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
    }

