﻿using PruebasDePOO.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Tron.Objetos
{
    public class GestionImagenes
    {
        internal Grid grid;
        internal Random random;
        internal Image[] Images;
        internal int cantidadImg;
        internal int maxImages = 3;
        public int numImages = 0;
        internal Form VentanaPrincipal;
        internal PictureBox randomPictureBox;


        public GestionImagenes(Grid grid, Form VentanaPrincipal)
        {
            this.grid = grid;
            random = new Random();
            this.VentanaPrincipal = VentanaPrincipal;
        }

        public async void GenerarImagenes()
        {
            while (true)
            {
                Image randomImage = Images[random.Next(cantidadImg)];

                if (numImages < maxImages)
                {
                    PlaceRandomImage(randomImage);
                }

                await Task.Delay(500);
            }
        }

        private void PlaceRandomImage(Image image)
        {
            if (VentanaPrincipal.InvokeRequired)
            {
                // Invocar el método en el hilo principal si se llama desde un hilo secundario
                VentanaPrincipal.Invoke(new Action(() => PlaceRandomImage(image)));
            }
            else
            {
                // Crear y configurar PictureBox para la imagen aleatoria
                randomPictureBox = new PictureBox
                {
                    Image = image,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Visible = false // Inicia oculta
                };
                VentanaPrincipal.Controls.Add(randomPictureBox);

                // Colocar la imagen en una posición aleatoria
                Place();
            }
        }

        private void Place()
        {
            // Generar posiciones aleatorias dentro de la grid
            int randomColumn = random.Next(grid.columns);
            int randomRow = random.Next(grid.rows);

            // Navegar hasta la posición aleatoria en la grid
            FourNode currentNode = grid.GetHead();
            for (int i = 0; i < randomColumn; i++)
            {
                currentNode = currentNode.Right;
            }
            for (int j = 0; j < randomRow; j++)
            {
                currentNode = currentNode.Down;
            }

            if (currentNode.GetOcupante() == null)
            {
                currentNode.SetOcupante(this);
                currentNode.Imagen = randomPictureBox;
                // Colocar la imagen en la posición aleatoria
                randomPictureBox.Location = new Point(currentNode.X, currentNode.Y);
                randomPictureBox.Visible = true; // Hacerla visible
                numImages++;
            }
        }


    }
}
