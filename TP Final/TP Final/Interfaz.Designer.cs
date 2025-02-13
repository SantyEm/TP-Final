namespace TP_Final
{
    partial class Interfaz
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interfaz));
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panelformularios = new System.Windows.Forms.Panel();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRol = new System.Windows.Forms.Label();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.panelBarraTitulo = new System.Windows.Forms.Panel();
            this.btnRestaurar = new System.Windows.Forms.PictureBox();
            this.btnMinimizar = new System.Windows.Forms.PictureBox();
            this.btnMaximizar = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BTNdatosAdicionales = new System.Windows.Forms.Button();
            this.BTNacercaDe = new System.Windows.Forms.Button();
            this.BTNseguimiento = new System.Windows.Forms.Button();
            this.BTNbusqueda = new System.Windows.Forms.Button();
            this.BTNregistro = new System.Windows.Forms.Button();
            this.panelContenedor.SuspendLayout();
            this.panelformularios.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelContenedor.Controls.Add(this.panelformularios);
            this.panelContenedor.Controls.Add(this.panelMenu);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 0);
            this.panelContenedor.MinimumSize = new System.Drawing.Size(650, 650);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(1366, 768);
            this.panelContenedor.TabIndex = 7;
            // 
            // panelformularios
            // 
            this.panelformularios.BackColor = System.Drawing.SystemColors.Control;
            this.panelformularios.Controls.Add(this.pictureBox1);
            this.panelformularios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelformularios.Location = new System.Drawing.Point(0, 88);
            this.panelformularios.Name = "panelformularios";
            this.panelformularios.Size = new System.Drawing.Size(1366, 680);
            this.panelformularios.TabIndex = 8;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(41)))), ((int)(((byte)(68)))));
            this.panelMenu.Controls.Add(this.BTNdatosAdicionales);
            this.panelMenu.Controls.Add(this.BTNacercaDe);
            this.panelMenu.Controls.Add(this.BTNseguimiento);
            this.panelMenu.Controls.Add(this.BTNbusqueda);
            this.panelMenu.Controls.Add(this.label2);
            this.panelMenu.Controls.Add(this.label1);
            this.panelMenu.Controls.Add(this.labelRol);
            this.panelMenu.Controls.Add(this.labelUsuario);
            this.panelMenu.Controls.Add(this.BTNregistro);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(1366, 88);
            this.panelMenu.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(1149, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Administrador";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(1149, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Admin";
            // 
            // labelRol
            // 
            this.labelRol.AutoSize = true;
            this.labelRol.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelRol.Location = new System.Drawing.Point(1094, 68);
            this.labelRol.Name = "labelRol";
            this.labelRol.Size = new System.Drawing.Size(26, 13);
            this.labelRol.TabIndex = 7;
            this.labelRol.Text = "Rol:";
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelUsuario.Location = new System.Drawing.Point(1094, 38);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(49, 13);
            this.labelUsuario.TabIndex = 1;
            this.labelUsuario.Text = "Usuario: ";
            // 
            // panelBarraTitulo
            // 
            this.panelBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(93)))), ((int)(((byte)(142)))));
            this.panelBarraTitulo.Controls.Add(this.btnRestaurar);
            this.panelBarraTitulo.Controls.Add(this.btnMinimizar);
            this.panelBarraTitulo.Controls.Add(this.btnMaximizar);
            this.panelBarraTitulo.Controls.Add(this.btnCerrar);
            this.panelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelBarraTitulo.Margin = new System.Windows.Forms.Padding(2);
            this.panelBarraTitulo.Name = "panelBarraTitulo";
            this.panelBarraTitulo.Size = new System.Drawing.Size(1366, 32);
            this.panelBarraTitulo.TabIndex = 7;
            this.panelBarraTitulo.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBarraTitulo_Paint);
            this.panelBarraTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelBarraTitulo_MouseMove);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestaurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestaurar.Image = ((System.Drawing.Image)(resources.GetObject("btnRestaurar.Image")));
            this.btnRestaurar.Location = new System.Drawing.Point(1328, 9);
            this.btnRestaurar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(12, 13);
            this.btnRestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.TabStop = false;
            this.btnRestaurar.Visible = false;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimizar.Image")));
            this.btnMinimizar.Location = new System.Drawing.Point(1312, 9);
            this.btnMinimizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(12, 13);
            this.btnMinimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.TabStop = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click_1);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximizar.Image")));
            this.btnMaximizar.Location = new System.Drawing.Point(1328, 9);
            this.btnMaximizar.Margin = new System.Windows.Forms.Padding(2);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(12, 13);
            this.btnMaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximizar.TabIndex = 1;
            this.btnMaximizar.TabStop = false;
            this.btnMaximizar.Click += new System.EventHandler(this.btnMaximizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(1344, 9);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(12, 13);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(375, 96);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(661, 424);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BTNdatosAdicionales
            // 
            this.BTNdatosAdicionales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNdatosAdicionales.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTNdatosAdicionales.FlatAppearance.BorderSize = 0;
            this.BTNdatosAdicionales.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.BTNdatosAdicionales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.BTNdatosAdicionales.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTNdatosAdicionales.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNdatosAdicionales.ForeColor = System.Drawing.Color.Gainsboro;
            this.BTNdatosAdicionales.Image = global::TP_Final.Properties.Resources.DatosAdicionales_Icon;
            this.BTNdatosAdicionales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNdatosAdicionales.Location = new System.Drawing.Point(180, 36);
            this.BTNdatosAdicionales.Margin = new System.Windows.Forms.Padding(2);
            this.BTNdatosAdicionales.Name = "BTNdatosAdicionales";
            this.BTNdatosAdicionales.Size = new System.Drawing.Size(174, 48);
            this.BTNdatosAdicionales.TabIndex = 10;
            this.BTNdatosAdicionales.Text = "Solicitantes Datos Adicionales\r\n";
            this.BTNdatosAdicionales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNdatosAdicionales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNdatosAdicionales.UseVisualStyleBackColor = true;
            this.BTNdatosAdicionales.Click += new System.EventHandler(this.button1_Click);
            // 
            // BTNacercaDe
            // 
            this.BTNacercaDe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BTNacercaDe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNacercaDe.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTNacercaDe.FlatAppearance.BorderSize = 0;
            this.BTNacercaDe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.BTNacercaDe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.BTNacercaDe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTNacercaDe.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNacercaDe.ForeColor = System.Drawing.Color.Gainsboro;
            this.BTNacercaDe.Image = global::TP_Final.Properties.Resources.AcercaDe_Icon;
            this.BTNacercaDe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNacercaDe.Location = new System.Drawing.Point(714, 36);
            this.BTNacercaDe.Margin = new System.Windows.Forms.Padding(2);
            this.BTNacercaDe.Name = "BTNacercaDe";
            this.BTNacercaDe.Size = new System.Drawing.Size(174, 48);
            this.BTNacercaDe.TabIndex = 6;
            this.BTNacercaDe.Text = "Acerca De";
            this.BTNacercaDe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNacercaDe.UseVisualStyleBackColor = true;
            this.BTNacercaDe.Click += new System.EventHandler(this.BTNacercaDe_Click_1);
            // 
            // BTNseguimiento
            // 
            this.BTNseguimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNseguimiento.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTNseguimiento.FlatAppearance.BorderSize = 0;
            this.BTNseguimiento.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.BTNseguimiento.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.BTNseguimiento.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTNseguimiento.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNseguimiento.ForeColor = System.Drawing.Color.Gainsboro;
            this.BTNseguimiento.Image = global::TP_Final.Properties.Resources.seguimiento_Icon;
            this.BTNseguimiento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNseguimiento.Location = new System.Drawing.Point(536, 36);
            this.BTNseguimiento.Margin = new System.Windows.Forms.Padding(2);
            this.BTNseguimiento.Name = "BTNseguimiento";
            this.BTNseguimiento.Size = new System.Drawing.Size(174, 47);
            this.BTNseguimiento.TabIndex = 3;
            this.BTNseguimiento.Text = "Seguimiento de Adopciones";
            this.BTNseguimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNseguimiento.UseVisualStyleBackColor = true;
            this.BTNseguimiento.Click += new System.EventHandler(this.BTNseguimiento_Click_1);
            // 
            // BTNbusqueda
            // 
            this.BTNbusqueda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNbusqueda.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTNbusqueda.FlatAppearance.BorderSize = 0;
            this.BTNbusqueda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.BTNbusqueda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.BTNbusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTNbusqueda.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNbusqueda.ForeColor = System.Drawing.Color.Gainsboro;
            this.BTNbusqueda.Image = global::TP_Final.Properties.Resources.AdoPet_blanco_icon;
            this.BTNbusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNbusqueda.Location = new System.Drawing.Point(358, 36);
            this.BTNbusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.BTNbusqueda.Name = "BTNbusqueda";
            this.BTNbusqueda.Size = new System.Drawing.Size(174, 48);
            this.BTNbusqueda.TabIndex = 2;
            this.BTNbusqueda.Text = "Búscar y Ver Gatos/Perros";
            this.BTNbusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNbusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNbusqueda.UseVisualStyleBackColor = true;
            this.BTNbusqueda.Click += new System.EventHandler(this.button3_Click);
            // 
            // BTNregistro
            // 
            this.BTNregistro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTNregistro.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BTNregistro.FlatAppearance.BorderSize = 0;
            this.BTNregistro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.BTNregistro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.BTNregistro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BTNregistro.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNregistro.ForeColor = System.Drawing.Color.Gainsboro;
            this.BTNregistro.Image = global::TP_Final.Properties.Resources.Solicitantes_Icon;
            this.BTNregistro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNregistro.Location = new System.Drawing.Point(2, 36);
            this.BTNregistro.Margin = new System.Windows.Forms.Padding(2);
            this.BTNregistro.Name = "BTNregistro";
            this.BTNregistro.Size = new System.Drawing.Size(174, 46);
            this.BTNregistro.TabIndex = 1;
            this.BTNregistro.Text = "Solicitantes Datos Personales\r\n";
            this.BTNregistro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BTNregistro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTNregistro.UseVisualStyleBackColor = true;
            this.BTNregistro.Click += new System.EventHandler(this.BTNregistro_Click_1);
            // 
            // Interfaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.panelBarraTitulo);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(650, 650);
            this.Name = "Interfaz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interfaz";
            this.panelContenedor.ResumeLayout(false);
            this.panelformularios.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelBarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnRestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button BTNbusqueda;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button BTNregistro;
        private System.Windows.Forms.Panel panelBarraTitulo;
        private System.Windows.Forms.PictureBox btnRestaurar;
        private System.Windows.Forms.PictureBox btnMinimizar;
        private System.Windows.Forms.PictureBox btnMaximizar;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.Button BTNacercaDe;
        private System.Windows.Forms.Button BTNseguimiento;
        private System.Windows.Forms.Panel panelformularios;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRol;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Button BTNdatosAdicionales;
    }
}