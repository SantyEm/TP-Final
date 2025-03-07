﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP_Final.FormMenu;

namespace TP_Final
{
    public partial class Interfaz : Form
    {
        public Interfaz()
        {
            InitializeComponent();
        }

        #region Funcionalidades del formulario
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        private void Interfaz_SizeChanged(object sender, EventArgs e)
        {
            // Ajusta el alto del panelMenu para que coincida con la altura del formulario
            panelMenu.Height = Height;

            // Ajusta el ancho del panelMenu para que llene el espacio restante
            panelMenu.Width = Width - panelMenu.Left;

            // Asegúrate de que el ancho no sea menor que un valor mínimo (por ejemplo, 200)
            if (panelMenu.Width < 200)
            {
                panelMenu.Width = 200;
            }
        }


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        int lx, ly;
        int sw, sh;

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
           //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        #endregion

        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }

        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["busqueda"] == null)
                BTNbusqueda.BackColor = Color.FromArgb(4, 41, 68);

            if (Application.OpenForms["registro"] == null)
                BTNregistro.BackColor = Color.FromArgb(4, 41, 68);

            if (Application.OpenForms["seguimientos"] == null)
                BTNseguimiento.BackColor = Color.FromArgb(4, 41, 68);

            if (Application.OpenForms["acercaDe"] == null)
                BTNacercaDe.BackColor = Color.FromArgb(4, 41, 68);

            if (Application.OpenForms["DatosAdicionales"] == null)
                BTNdatosAdicionales.BackColor = Color.FromArgb(4, 41, 68);
        }

        private void BTNacercaDe_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<acercaDe>();
            BTNacercaDe.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void BTNseguimiento_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<seguimientos>();
            BTNseguimiento.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormulario<busqueda>();
            BTNbusqueda.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void BTNregistro_Click_1(object sender, EventArgs e)
        {
            AbrirFormulario<registro>();
            BTNregistro.BackColor = Color.FromArgb(12, 61, 92);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormulario<DatosAdicionales>();
            BTNdatosAdicionales.BackColor = Color.FromArgb(12, 61, 92);
        }
    
    }
}
    