namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Graphe association = new Graphe();
            association.AjouterNoeud(1, "Alice");
            association.AjouterNoeud(2, "Bob");
            association.AjouterNoeud(3, "Charlie");

            association.AjouterLien(1, 2);
            association.AjouterLien(2, 3);
            association.AjouterLien(1, 3);

            association.AfficherGraphe();
        }
    }
}
