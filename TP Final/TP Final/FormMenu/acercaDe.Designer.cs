﻿namespace TP_Final.FormMenu
{
    partial class acercaDe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(acercaDe));
            this.BTNSalir = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // BTNSalir
            // 
            this.BTNSalir.Font = new System.Drawing.Font("Open Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTNSalir.Location = new System.Drawing.Point(186, 66);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(115, 34);
            this.BTNSalir.TabIndex = 0;
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(108, 106);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1133, 428);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // acercaDe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.BTNSalir);
            this.Name = "acercaDe";
            this.Text = "acercaDe";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}