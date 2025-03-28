namespace ConsoleApp4;
    using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

public class Program
{
    public static void Main(string[] args)
    {
        //        string cheminFichier = "C:\\Users\\olivi\\Desktop\\Semestre 2\\soc-karate.mtx"; // Chemin du fichier

        //        /// Lire le fichier et extraire les informations
        //        var (taille, liens) = LireFichierGraphe(cheminFichier);

        //    /// Créer le graphe
        //    var graphe = new Graphe<int>(taille);  


        //    /// Ajouter les liens au graphe
        //    foreach (var (id1, id2) in liens)
        //        {
        //            graphe.AjouterLien(id1, id2);
        //        }

        //        /// Afficher les représentations du graphe
        //        graphe.AfficherListeAdjacence();
        //        graphe.AfficherMatriceAdjacence();

        //        /// Lancer le parcours en largeur depuis un sommet donné : sommetDepart
        //      Random random = new Random();
        //    int nombreAleatoire = random.Next(1, 100);
        //    int sommetDepart = nombreAleatoire ; 
        //        graphe.ParcoursLargeur(sommetDepart);

        //        /// Lancer le parcours en profondeur depuis un sommet donné : sommetDepart

        //        graphe.ParcoursProfondeur(sommetDepart);
        //        /// Le graphe est t-il connexe ?
        //        if (graphe.EstConnexe())
        //        {
        //            Console.WriteLine("Le graphe est connexe, en effet à partir d'un sommet de départ quelconque, tous les autres sommets du graphe on été explorées.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Le graphe n'est pas connexe, en effet à partir d'un sommet de départ quelconque, pas tous les autres sommets du graphe on été explorées.");
        //        }
        //    /// le graphe contient un cylcle ? 
        //    if (graphe.EstUnCycle(graphe.ListeSommets))

        //        {
        //        Console.WriteLine("Le graphe contient un cycle.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Le graphe est acyclique.");
        //        }

        //    graphe.VisualiserGraphe("C:\\Users\\olivi\\Desktop\\graphe.png");
        //    Console.WriteLine("Le graphe a été visualisé et sauvegardé sous 'graphe.png'.");
        //}

        //    public static (int taille, List<(int, int)> liens) LireFichierGraphe(string cheminFichier)
        //    {
        //        var liens = new List<(int, int)>();
        //        int taille = 0;

        //        using (var reader = new StreamReader(cheminFichier))
        //        {
        //            string ligne;
        //            while ((ligne = reader.ReadLine()) != null)
        //            {
        //                /// Ignorer les commentaires et les lignes vides
        //                if (ligne.StartsWith("%") )
        //                {
        //                    continue;
        //                }

        //                /// Lire la ligne contenant la taille et le nombre de liens
        //                if (taille == 0)
        //                {
        //                    var parts = ligne.Split(' ');
        //                    taille = int.Parse(parts[0]); /// La taille est le premier nombre
        //                    continue;
        //                }

        //                /// Lire les paires de nœuds représentant les liens
        //                var ids = ligne.Split(' ');
        //                int id1 = int.Parse(ids[0]);
        //                int id2 = int.Parse(ids[1]);
        //                liens.Add((id1, id2));
        //            }
        //        }

        //        return (taille, liens);
        //    }


        string cheminFichier = "C:\\Users\\olivi\\Desktop\\Projet Algortihme\\MetroParis.xlsx";
        if (!File.Exists(cheminFichier))
        {
            Console.WriteLine("Erreur : Le fichier Excel n'a pas été trouvé !");
            return;
        }
        MetroGraph metroGraph = new MetroGraph();
        metroGraph.ChargerMetro(cheminFichier);
        metroGraph.AfficherGraphe();

       

        string cheminImage = "C:\\Users\\olivi\\Desktop\\graphe.png";
        metroGraph.VisualiserGraphe(cheminImage);
        Console.WriteLine($"Graphe du métro visualisé et sauvegardé sous {cheminImage}.");

    }
}

