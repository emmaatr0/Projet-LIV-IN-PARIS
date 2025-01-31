namespace ConsoleApp4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string cheminFichier = "C:\\Users\\olivi\\Desktop\\soc-karate.mtx"; // Chemin du fichier

            // Lire le fichier et extraire les informations
            var (taille, liens) = LireFichierGraphe(cheminFichier);

            // Créer le graphe
            var graphe = new Graphe(taille);

            // Ajouter les liens au graphe
            foreach (var (id1, id2) in liens)
            {
                graphe.AjouterLien(id1, id2);
            }

            // Afficher les représentations du graphe
            graphe.AfficherListeAdjacence();
            graphe.AfficherMatriceAdjacence();
        }

        public static (int taille, List<(int, int)> liens) LireFichierGraphe(string cheminFichier)
        {
            var liens = new List<(int, int)>();
            int taille = 0;

            using (var reader = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = reader.ReadLine()) != null)
                {
                    // Ignorer les commentaires et les lignes vides
                    if (ligne.StartsWith("%") || string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }

                    // Lire la ligne contenant la taille et le nombre de liens
                    if (taille == 0)
                    {
                        var parts = ligne.Split(' ');
                        taille = int.Parse(parts[0]); // La taille est le premier nombre
                        continue;
                    }

                    // Lire les paires de nœuds représentant les liens
                    var ids = ligne.Split(' ');
                    int id1 = int.Parse(ids[0]);
                    int id2 = int.Parse(ids[1]);
                    liens.Add((id1, id2));
                }
            }

            return (taille, liens);
        }
    }
}
