﻿using PruebasDePOO.Nodes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto1_Tron
{
    public class Estela
    {
        private LinkedList<PictureBox> segmentosEstela; // Lista de segmentos que conforman la estela
        private Form ventanaPrincipal;
        private int longitudMaxima; // Longitud máxima de la estela

        public Estela(Form ventanaPrincipal, int longitudInicial = 3)
        {
            this.ventanaPrincipal = ventanaPrincipal;
            segmentosEstela = new LinkedList<PictureBox>();
            longitudMaxima = longitudInicial; // Valor inicial de longitud máxima
        }

        // Nueva función para manejar la estela
        public void ManejarEstela(FourNode nuevaPosicion)
        {
            // Agregar un nuevo segmento de estela
            AgregarSegmento(nuevaPosicion);

            // Verificar si la longitud de la estela supera la longitud máxima permitida
            while (segmentosEstela.Count > longitudMaxima)
            {
                // Eliminar el segmento más antiguo de la estela
                RemoverSegmentoAntiguo(nuevaPosicion);
            }
        }

        // Método para agregar un nuevo segmento a la estela
        private void AgregarSegmento(FourNode posicion)
        {
            PictureBox nuevoSegmento = new PictureBox
            {
                Size = new Size(10, 10), // Tamaño del segmento de la estela
                BackColor = Color.Blue,  // Color del segmento
                Location = new Point(posicion.X, posicion.Y) // Posición basada en el nodo
            };

            segmentosEstela.AddLast(nuevoSegmento);
            ventanaPrincipal.Controls.Add(nuevoSegmento); // Agregar el segmento al formulario
            posicion.SetOcupante(this);
        }

        // Método para remover el segmento más antiguo de la estela
        private void RemoverSegmentoAntiguo(FourNode posicion)
        {
            if (segmentosEstela.Count > 0)
            {
                PictureBox segmentoAntiguo = segmentosEstela.First.Value;
                ventanaPrincipal.Controls.Remove(segmentoAntiguo); // Remover del formulario
                segmentosEstela.RemoveFirst(); // Remover de la lista
                segmentoAntiguo.Dispose(); // Liberar recursos
                //posicion.SetOcupante(null);
            }
        }

        // Método para incrementar la longitud máxima de la estela
        public void IncrementarLongitud(int cantidad)
        {
            longitudMaxima += cantidad;
        }

    }

}
