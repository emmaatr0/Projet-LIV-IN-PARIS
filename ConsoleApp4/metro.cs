using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using OfficeOpenXml;
namespace ConsoleApp4
{
    public class MetroGraph : Graphe<string>
    {
        public MetroGraph() : base()
        {
            // Initialize any additional properties specific to MetroGraph if needed
        }

        public void ChargerMetro(string cheminFichier)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(cheminFichier)))
            {
                var feuilleArc = package.Workbook.Worksheets["Arcs"];

                for (int i = 2; i <= feuilleArc.Dimension.End.Row; i++)
                {
                    string station1 = feuilleArc.Cells[i, 1].Text; // "Station Id"
                    string station2 = feuilleArc.Cells[i, 4].Text; // "Suivant"

                    if (string.IsNullOrEmpty(station1) || string.IsNullOrEmpty(station2))
                        continue;

                    int temps = feuilleArc.Cells[i, 5].Value != null ? int.Parse(feuilleArc.Cells[i, 5].Text) : 0; // "Temps entre 2 stations"
                    int tempsChangement = feuilleArc.Cells[i, 6].Value != null ? int.Parse(feuilleArc.Cells[i, 6].Text) : 0; // "Temps de Changement"

                    if (!listeAdjacence.ContainsKey(station1))
                        listeAdjacence[station1] = new Noeud<string>(station1);
                    if (!listeAdjacence.ContainsKey(station2))
                        listeAdjacence[station2] = new Noeud<string>(station2);

                    AjouterLien(station1, station2, temps);

                    if (tempsChangement > 0)
                    {
                        AjouterLien(station1, station2, tempsChangement);
                    }
                }
            }
        }


        public void AfficherGraphe()
        {
            Console.WriteLine("\n🔹 Affichage du graphe du métro :");
            foreach (var station in listeAdjacence)
            {
                Console.Write($"{station.Key} -> ");
                foreach (var voisin in station.Value.Relations)
                {
                    Console.Write($"({voisin.Destination.Id}, {voisin.Poids} min) ");
                }
                Console.WriteLine();
            }
        }
    }    }