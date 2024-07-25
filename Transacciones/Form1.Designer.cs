namespace Transacciones
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            guardar = new Button();
            transaccionn = new Button();
            committ = new Button();
            rollbackk = new Button();
            dataGridView1 = new DataGridView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // guardar
            // 
            guardar.BackColor = Color.AntiqueWhite;
            guardar.Enabled = false;
            guardar.FlatStyle = FlatStyle.Flat;
            guardar.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            guardar.Image = Properties.Resources.save_i;
            guardar.Location = new Point(139, 461);
            guardar.Name = "guardar";
            guardar.Size = new Size(123, 109);
            guardar.TabIndex = 0;
            guardar.UseVisualStyleBackColor = false;
            guardar.Click += button1_Click_1;
            // 
            // transaccionn
            // 
            transaccionn.BackColor = Color.Goldenrod;
            transaccionn.FlatStyle = FlatStyle.Flat;
            transaccionn.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            transaccionn.Location = new Point(75, 629);
            transaccionn.Name = "transaccionn";
            transaccionn.Size = new Size(155, 84);
            transaccionn.TabIndex = 1;
            transaccionn.Text = "Transacción";
            transaccionn.UseVisualStyleBackColor = false;
            transaccionn.Click += transaccionn_Click;
            // 
            // committ
            // 
            committ.BackColor = Color.Goldenrod;
            committ.FlatStyle = FlatStyle.Flat;
            committ.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            committ.Location = new Point(279, 629);
            committ.Name = "committ";
            committ.Size = new Size(112, 84);
            committ.TabIndex = 2;
            committ.Text = "Commit";
            committ.UseVisualStyleBackColor = false;
            committ.Click += committ_Click;
            // 
            // rollbackk
            // 
            rollbackk.BackColor = Color.Goldenrod;
            rollbackk.FlatStyle = FlatStyle.Flat;
            rollbackk.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rollbackk.Location = new Point(450, 629);
            rollbackk.Name = "rollbackk";
            rollbackk.Size = new Size(114, 84);
            rollbackk.TabIndex = 3;
            rollbackk.Text = "Rollback";
            rollbackk.UseVisualStyleBackColor = false;
            rollbackk.Click += rollbackk_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(629, 115);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(585, 598);
            dataGridView1.TabIndex = 4;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Comic Sans MS", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(75, 140);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(489, 38);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Comic Sans MS", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(75, 229);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(489, 38);
            textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Comic Sans MS", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(75, 320);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(489, 38);
            textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Comic Sans MS", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox4.Location = new Point(75, 406);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(489, 38);
            textBox4.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(139, 49);
            label1.Name = "label1";
            label1.Size = new Size(276, 31);
            label1.TabIndex = 9;
            label1.Text = "Ingrese un nuevo cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Comic Sans MS", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(676, 66);
            label2.Name = "label2";
            label2.Size = new Size(451, 31);
            label2.TabIndex = 10;
            label2.Text = "Datos en tiempo real de la Transacción !!!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(78, 109);
            label3.Name = "label3";
            label3.Size = new Size(290, 28);
            label3.TabIndex = 11;
            label3.Text = "Ingrese el nombre del cliente:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(78, 198);
            label4.Name = "label4";
            label4.Size = new Size(292, 28);
            label4.TabIndex = 12;
            label4.Text = "Ingrese el apellido del cliente:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(78, 289);
            label5.Name = "label5";
            label5.Size = new Size(306, 28);
            label5.TabIndex = 13;
            label5.Text = "Ingrese la dirección del cliente:\r\n";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(78, 375);
            label6.Name = "label6";
            label6.Size = new Size(298, 28);
            label6.TabIndex = 14;
            label6.Text = "Ingrese el telefóno del cliente:";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(348, 461);
            button1.Name = "button1";
            button1.Size = new Size(127, 109);
            button1.TabIndex = 15;
            button1.Text = "Limpiar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AntiqueWhite;
            ClientSize = new Size(1226, 753);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(dataGridView1);
            Controls.Add(rollbackk);
            Controls.Add(committ);
            Controls.Add(transaccionn);
            Controls.Add(guardar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MODULO CLIENTES";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button guardar;
        private Button transaccionn;
        private Button committ;
        private Button rollbackk;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
    }
}
