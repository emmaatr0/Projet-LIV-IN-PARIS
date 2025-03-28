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
    /// Classe représentant le graphe
    public class Graphe<T>

    {
        public Dictionary<T, Noeud<T> >listeAdjacence; /// Stocke les relations sous forme de liste
        public Dictionary<T, Dictionary<T, int>> matriceAdjacence; /// Stocke les relations sous forme de matrice
        public List<Noeud<T>> ListeSommets { get; set; }


        public Graphe()
        {
            ListeSommets = new List<Noeud<T>>();
            
            listeAdjacence = new Dictionary<T, Noeud<T>>();
            matriceAdjacence = new Dictionary<T, Dictionary<T, int>>(); /// +1 pour ignorer l'index 0
        }

        /// Ajoute un nœud au graphe s'il n'existe pas déjà
        public void AjouterNoeud(T id)
        {
            if (!listeAdjacence.ContainsKey(id))
            {
                listeAdjacence[id] = new Noeud<T>(id);
                matriceAdjacence[id] = new Dictionary<T, int>();

            }
        }

        public void AjouterLien(T idSource, T idDestination, int poids = 1)
        {
            // Ensure both nodes exist in the adjacency list
            if (listeAdjacence.ContainsKey(idSource) && listeAdjacence.ContainsKey(idDestination))
            {
                // Add the relation to the adjacency list
                listeAdjacence[idSource].AjouterRelation(listeAdjacence[idDestination], poids);

                // Ensure the outer dictionary exists for idSource
                if (!matriceAdjacence.ContainsKey(idSource))
                {
                    matriceAdjacence[idSource] = new Dictionary<T, int>();
                }

                // Ensure the inner dictionary exists for idDestination
                if (!matriceAdjacence[idSource].ContainsKey(idDestination))
                {
                    matriceAdjacence[idSource][idDestination] = 0; // Initialize with a default value
                }

                // Set the weight for the edge
                matriceAdjacence[idSource][idDestination] = poids;
            }
            else
            {
                throw new ArgumentException("Both idSource and idDestination must exist in the graph.");
            }
        }


        public void AfficherListeAdjacence()
        {
            Console.WriteLine("Liste d'adjacence:");
            foreach (var noeud in listeAdjacence)
            {
                Console.Write($"{noeud.Key}: ");
                foreach (var voisin in noeud.Value.Relations)
                {
                    Console.Write($"{voisin.Destination.Id} ");
                }
                Console.WriteLine();
            }
        }

        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("Matrice d'adjacence:");
            foreach (var ligne in matriceAdjacence)
            {
                Console.Write(ligne.Key + " : ");
                foreach (var colonne in matriceAdjacence)
                {
                    Console.Write(matriceAdjacence[ligne.Key].ContainsKey(colonne.Key) ? matriceAdjacence[ligne.Key][colonne.Key] + " " : "0 ");
                }
                Console.WriteLine();
            }
        }
        public void ParcoursLargeur(T sommetDepart)
        {
            if (!listeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"Le sommet {sommetDepart} n'existe pas dans le graphe.");
                return;
            }

            var file = new Queue<Noeud<T>>();
            var visites = new HashSet<T>();

            file.Enqueue(listeAdjacence[sommetDepart]);
            visites.Add(sommetDepart);

            Console.WriteLine($"Parcours en largeur depuis le sommet {sommetDepart}:");

            while (file.Count > 0)
            {
                Noeud<T> courant = file.Dequeue();
                Console.Write(courant.Id + " ");

                foreach (var lien in courant.Relations)
                {
                    if (!visites.Contains(lien.Destination.Id))
                    {
                        file.Enqueue(lien.Destination);
                        visites.Add(lien.Destination.Id);
                    }
                }
            }
            Console.WriteLine();
        }
        public void ParcoursProfondeur(T sommetDepart)
        {
            if (!listeAdjacence.ContainsKey(sommetDepart))
            {
                Console.WriteLine($"Le sommet {sommetDepart} n'existe pas dans le graphe.");
                return;
            }

            var pile = new Stack<Noeud<T>>();
            var visites = new HashSet<T>();

            pile.Push(listeAdjacence[sommetDepart]);
            visites.Add(sommetDepart);

            Console.WriteLine($"Parcours en profondeur depuis le sommet {sommetDepart}:");

            while (pile.Count > 0)
            {
                Noeud<T> courant = pile.Pop();
                Console.Write(courant.Id + " ");

                foreach (var lien in courant.Relations)
                {
                    if (!visites.Contains(lien.Destination.Id))
                    {
                        pile.Push(lien.Destination);
                        visites.Add(lien.Destination.Id);
                    }
                }
            }
            Console.WriteLine();
        }
        public bool ExisteChemin(Noeud<T> sommetDepart, Noeud<T> sommetDestination)
        {
            var fileDePropagation = new Queue<Noeud<T>>();  // File pour la propagation BFS
            var sommetsVisites = new HashSet<T>();  // Set pour marquer les sommets visités

            fileDePropagation.Enqueue(sommetDepart);  // Enfiler le sommet de départ
            sommetsVisites.Add(sommetDepart.Id);

            while (fileDePropagation.Count > 0)
            {
                var sommetCourant = fileDePropagation.Dequeue();  // Défilement du sommet à explorer

                // Exploration des voisins du sommet courant
                foreach (var lien in sommetCourant.Relations)
                {
                    var voisin = lien.Destination;

                    // Si le voisin n'a pas encore été visité, on l'ajoute à la file
                    if (!sommetsVisites.Contains(voisin.Id))
                    {
                        if (voisin.Equals(sommetDestination))
                        {
                            return true;  // Si on atteint le sommet destination, retourner true
                        }

                        fileDePropagation.Enqueue(voisin);  // Ajouter le voisin à la file pour exploration
                        sommetsVisites.Add(voisin.Id);  // Marquer ce voisin comme visité
                    }
                }
            }

            // Aucun chemin trouvé entre le sommet de départ et la destination
            return false;
        }

        public bool EstUnCycle(List<Noeud<T>> listeSommets)
        {
            foreach (var sommet in listeSommets)
            {
                // Vérifie si un sommet est relié à lui-même via un chemin
                if (ExisteChemin(sommet, sommet))
                {
                    return true;  // Cycle trouvé
                }
            }
            return false;  // Aucun cycle trouvé
        }

        public bool EstConnexe()
        {
            if (listeAdjacence.Count == 0)
                return false; // Un graphe vide n'est pas connexe

            // Prendre un sommet quelconque, ici le premier de la liste
            var premierSommet = listeAdjacence.Keys.First();

            // Liste des sommets visités et pile pour la recherche en profondeur
            var visites = new HashSet<T>(); // Utilisation d'un HashSet pour optimiser la recherche
            var pile = new Stack<Noeud<T>>();

            // Commencer le parcours à partir du premier sommet
            pile.Push(listeAdjacence[premierSommet]);
            visites.Add(premierSommet);

            while (pile.Count > 0)
            {
                var noeudActuel = pile.Pop(); // Récupérer le sommet actuel

                foreach (var lien in noeudActuel.Relations)
                {
                    // Si le voisin n'a pas été visité, on l'ajoute à la pile et à la liste des visites
                    if (!visites.Contains(lien.Destination.Id))
                    {
                        visites.Add(lien.Destination.Id);
                        pile.Push(lien.Destination); // Ajouter à la pile pour explorer ensuite
                    }
                }
            }

            // Si tous les sommets ont été visités, le graphe est connexe
            return visites.Count == listeAdjacence.Count;
        }

        public void VisualiserGraphe(string cheminFichier)
        {
            int largeur = 800;
            int hauteur = 600;
            Bitmap bitmap = new Bitmap(largeur, hauteur);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            Random random = new Random();
            Dictionary<T, Point> positions = new Dictionary<T, Point>();

            foreach (var noeud in listeAdjacence.Keys)
            {
                int x = random.Next(50, largeur - 50);
                int y = random.Next(50, hauteur - 50);
                positions[noeud] = new Point(x, y);
            }

            Pen pen = new Pen(Color.Black, 2);
            foreach (var noeud in listeAdjacence)
            {
                foreach (var lien in noeud.Value.Relations)
                {
                    Point p1 = positions[noeud.Key];
                    Point p2 = positions[lien.Destination.Id];
                    graphics.DrawLine(pen, p1, p2);
                }
            }

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

        // Algorithme de Dijkstra
        public (Dictionary<T, int>, Dictionary<T, T>) Dijkstra(T noeudDepart)
        {
            // Initialisation
            var distances = new Dictionary<T, int>();
            var precedent = new Dictionary<T, T>();
            var noeudsRestants = new HashSet<T>();

            foreach (var noeud in listeAdjacence.Keys)
            {
                distances[noeud] = int.MaxValue;
                precedent[noeud] = default(T); 
                noeudsRestants.Add(noeud);
            }

            distances[noeudDepart] = 0;

            while (noeudsRestants.Count > 0)
            {
                // Trouve le nœud avec la distance minimale
                T noeudCourant = TrouverMin(noeudsRestants, distances);
                if (noeudCourant == null)
                {
                    break; // Exit the loop if no valid node is found
                }
                noeudsRestants.Remove(noeudCourant);


                // Mise à jour des distances des voisins
                foreach (var lien in listeAdjacence[noeudCourant].Relations)
                {
                    T voisin = lien.Destination.Id;
                    int cout = lien.Poids;
                    if (noeudsRestants.Contains(voisin))
                    {
                        int nouvelleDistance = distances[noeudCourant] + cout;
                        if (nouvelleDistance < distances[voisin])
                        {
                            distances[voisin] = nouvelleDistance;
                            precedent[voisin] = noeudCourant;
                        }
                    }
                }
            }

            return (distances, precedent);
        }
        // Trouve le nœud avec la distance minimale dans noeudsRestants
        private T TrouverMin(HashSet<T> noeudsRestants, Dictionary<T, int> distances)
        {
            T noeudMin = default;
            int distanceMin = int.MaxValue;

            foreach (var noeud in noeudsRestants)
            {
                if (distances[noeud] < distanceMin)
                {
                    distanceMin = distances[noeud];
                    noeudMin = noeud;
                }
            }

            return noeudMin;
        }

        // Reconstruit le chemin le plus court
        public (List<T>, int) ReconstruireChemin(T noeudDepart, T noeudDestination, Dictionary<T, T> precedent, Dictionary<T, int> distances)
        {
            List<T> chemin = new List<T>();
            T etape = noeudDestination;
            int tempsTotal = distances[noeudDestination];

            while (etape != null)
            {
                chemin.Insert(0, etape);
                if (etape.Equals(noeudDepart))
                    break;
                etape = precedent[etape];
            }

            return (chemin, tempsTotal);
        }
    }
}






