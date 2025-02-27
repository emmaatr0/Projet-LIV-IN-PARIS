using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApp4
{
    // Classe représentant le graphe
    public class Graphe
    {
        private Dictionary<int, Noeud> listeAdjacence; // Stocke les relations sous forme de liste
        public int[,] matriceAdjacence; // Stocke les relations sous forme de matrice
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
        public void ParcoursLargeur(int sommetDepart)
        {
            if (!listeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"Le sommet {sommetDepart} n'existe pas dans le graphe.");
                return;
            }

            var file = new Queue<Noeud>(); // File pour gérer les niveaux
            var visites = new List<int>(); // Liste pour stocker les sommets visités

            file.Enqueue(listeAdjacence[sommetDepart]); // Ajouter le sommet de départ
            visites.Add(sommetDepart); // Marquer comme visité

            Console.WriteLine($"Parcours en largeur depuis le sommet {sommetDepart}:");

            while (file.Count > 0)
            {
                Noeud courant = file.Dequeue(); // Extraire le sommet en tête de file
               

                foreach (Noeud voisin in courant.Relations) // Parcourir les voisins
                {
                    if (!visites.Contains(voisin.Id))
                    {
                        file.Enqueue(voisin); // Ajouter à la file
                        visites.Add(voisin.Id); // Marquer comme visité
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Ordre de visite des sommets pour le parcours en largeur est : " + string.Join(", ", visites));
        }
        public void ParcoursProfondeur(int sommetDepart)
        {
            if (!listeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"Le sommet {sommetDepart} n'existe pas dans le graphe.");
                return;
            }

            var pile = new Stack<Noeud>(); // Pile pour gérer les sommets à visiter
            var visites = new List<int>(); // Liste pour stocker les sommets visités

            pile.Push(listeAdjacence[sommetDepart]); // Ajouter le sommet de départ
            visites.Add(sommetDepart); // Marquer comme visité

            Console.WriteLine($"Parcours en profondeur depuis le sommet {sommetDepart}:");

            while (pile.Count > 0)
            {
                Noeud courant = pile.Pop(); // Extraire le sommet en tête de pile
               

                foreach (Noeud voisin in courant.Relations) // Parcourir les voisins
                {
                    if (!visites.Contains(voisin.Id))
                    {
                        pile.Push(voisin); // Ajouter à la pile
                        visites.Add(voisin.Id); // Marquer comme visité
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Ordre de visite des sommets pour le parcours en profondeur est : " + string.Join(", ", visites));
        }
        public bool EstConnexe() // par un parcours en profondeur 
        {
            if (listeAdjacence.Count == 0)
                return false; // Un graphe vide n'est pas connexe

            var premierSommet = listeAdjacence.Keys.First(); // Prendre un sommet quelconque
            var visites = new List<int>();
            var pile = new Stack<int>();

            pile.Push(premierSommet);
            visites.Add(premierSommet);

            while (pile.Count > 0)
            {
                int sommet = pile.Pop(); // Récupérer le sommet en haut de la pile

                foreach (var voisin in listeAdjacence[sommet].Relations)
                {
                    if (!visites.Contains(voisin.Id))
                    {
                        visites.Add(voisin.Id);
                        pile.Push(voisin.Id); // Ajouter à la pile pour explorer ensuite
                    }
                }
            }

            // Si tous les sommets ont été visités, le graphe est connexe
            return visites.Count == listeAdjacence.Count;
        }

        public bool ExisteChemin(int[,] matriceAdjacence, int sommetDepart, int sommetDestination)
        {
            int nombreSommets = matriceAdjacence.GetLength(0); // Nombre total de sommets
            var fileDePropagation = new Queue<int>();  // File pour la propagation BFS
            var sommetsVisites = new bool[nombreSommets];    // Tableau pour marquer les sommets visités
            fileDePropagation.Enqueue(sommetDepart);              // Enfiler le sommet de départ

            while (fileDePropagation.Count > 0)
            {
                int sommetCourant = fileDePropagation.Dequeue(); // Défilement du sommet à explorer
                sommetsVisites[sommetCourant] = true; // Marquer le sommet comme visité

                // Exploration des voisins du sommet courant
                for (int i = 0; i < nombreSommets; i++)
                {
                    if (matriceAdjacence[sommetCourant, i] > 0 && !sommetsVisites[i])
                    {
                        fileDePropagation.Enqueue(i);   // Ajouter le voisin à la file pour exploration
                        sommetsVisites[i] = true;  // Marquer ce voisin comme visité
                    }

                    // Si on atteint le sommet destination, retourner true
                    if (matriceAdjacence[sommetCourant, i] > 0 && i == sommetDestination)
                    {
                        return true;
                    }
                }
            }

            // Aucun chemin trouvé entre le sommet de départ et la destination
            return false;
        }
        public bool EstUnCycle(int[,] matriceAdjacence)
        {
            int nombreSommets = matriceAdjacence.GetLength(0); // Nombre total de sommets
            for (int i = 0; i < nombreSommets; i++)
            {
                // Vérifie si un sommet est relié à lui-même
                if (ExisteChemin(matriceAdjacence, i, i))
                {
                    return true; // Cycle trouvé
                }
            }
            return false; // Aucun cycle trouvé
        }

        public void VisualiserGraphe(string cheminFichier)
        {
            int largeur = 800;
            int hauteur = 600;
            Bitmap bitmap = new Bitmap(largeur, hauteur);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            Random random = new Random();
            Dictionary<int, Point> positions = new Dictionary<int, Point>();

            // Générer des positions aléatoires pour chaque noeud
            foreach (var noeud in listeAdjacence.Keys)
            {
                int x = random.Next(50, largeur - 50);
                int y = random.Next(50, hauteur - 50);
                positions[noeud] = new Point(x, y);
            }

            // Dessiner les liens
            Pen pen = new Pen(Color.Black, 2);
            foreach (var noeud in listeAdjacence)
            {
                foreach (var voisin in noeud.Value.Relations)
                {
                    Point p1 = positions[noeud.Key];
                    Point p2 = positions[voisin.Id];
                    graphics.DrawLine(pen, p1, p2);
                }
            }

            // Dessiner les noeuds
            Brush brush = new SolidBrush(Color.Red);
            foreach (var noeud in listeAdjacence.Keys)
            {
                Point p = positions[noeud];
                graphics.FillEllipse(brush, p.X - 10, p.Y - 10, 20, 20);
                graphics.DrawString(noeud.ToString(), new Font("Arial", 12), Brushes.Black, p.X + 10, p.Y + 10);
            }

            bitmap.Save(cheminFichier, ImageFormat.Png);
            graphics.Dispose();
            bitmap.Dispose();
        }
    }
}


