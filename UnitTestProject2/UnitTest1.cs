using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestParcoursProfondeur()
        {
            
            Graphe graphe = new Graphe(5);
            graphe.AjouterLien(1, 2);
            graphe.AjouterLien(1, 3);
            graphe.AjouterLien(2, 4);
            graphe.AjouterLien(3, 4);
            graphe.AjouterLien(4, 5);

            var visites = new List<int>();
            var expectedVisites = new List<int> { 1, 3, 4, 5, 2 }; /// L'ordre peut varier selon l'implémentation

            
            graphe.ParcoursProfondeur(1, visites);

            Assert.Equal(expectedVisites.Count, visites.Count);
            foreach (var sommet in expectedVisites)
            {
                Assert.Contains(sommet, visites);
            }
        }
        public void TestParcoursLargeur()
        {
            // Arrange
            Graphe graphe = new Graphe(5);
            graphe.AjouterLien(1, 2);
            graphe.AjouterLien(1, 3);
            graphe.AjouterLien(2, 4);
            graphe.AjouterLien(3, 4);
            graphe.AjouterLien(4, 5);

            var visites = new List<int>();
            var expectedVisites = new List<int> { 1, 2, 3, 4, 5 }; /// L'ordre peut varier selon l'implémentation

            // Act
            graphe.ParcoursLargeur(1, visites);

            
            Assert.Equal(expectedVisites.Count, visites.Count);
            foreach (var sommet in expectedVisites)
            {
                Assert.Contains(sommet, visites);
            }
        }
        public void TestEstUnCycle()
        {
            // Arrange
            int[,] matriceAdjacence = new int[,]
            {
            { 0, 1, 0, 0, 0 },
            { 1, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 1, 0, 1 },
            { 0, 0, 0, 1, 0 }
            };

            // Act
            bool result = EstUnCycle(matriceAdjacence);

            // Assert
            Assert.False(result); // Ce graphe n'a pas de cycle

            // Arrange
            int[,] matriceAdjacenceAvecCycle = new int[,]
            {
            { 0, 1, 0, 0, 1 },
            { 1, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 1, 0, 1 },
            { 1, 0, 0, 1, 0 }
            };

            // Act
            bool resultAvecCycle = EstUnCycle(matriceAdjacenceAvecCycle);

            // Assert
            Assert.True(resultAvecCycle); // Ce graphe a un cycle
        }
    }
}
