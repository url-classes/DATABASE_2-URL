using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Transacciones
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private MySqlTransaction transaction;
        private bool isTransactionActive = false;

        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=transacciones;user=root;password=holamundo123;");
            guardar.Enabled = false;
            RefreshDataGridView();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isTransactionActive)
            {
                try
                {
                    string getNextIdQuery = "SELECT IFNULL(MAX(idTelefono), 0) + 1 FROM Telefonos";
                    MySqlCommand getNextIdCmd = new MySqlCommand(getNextIdQuery, connection, transaction);
                    int nextIdTelefono = Convert.ToInt32(getNextIdCmd.ExecuteScalar());

                    string insertTelefonoQuery = "INSERT INTO Telefonos (idTelefono, Telefono) VALUES (@idTelefono, @Telefono)";
                    MySqlCommand insertTelefonoCmd = new MySqlCommand(insertTelefonoQuery, connection, transaction);
                    insertTelefonoCmd.Parameters.AddWithValue("@idTelefono", nextIdTelefono);
                    insertTelefonoCmd.Parameters.AddWithValue("@Telefono", textBox4.Text);
                    insertTelefonoCmd.ExecuteNonQuery();

                    string query = "INSERT INTO Clientes (Nombre, Apellido, Direccion, idTelefono) VALUES (@Nombre, @Apellido, @Direccion, @idTelefono)";
                    MySqlCommand cmd = new MySqlCommand(query, connection, transaction);
                    cmd.Parameters.AddWithValue("@Nombre", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Apellido", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Direccion", textBox3.Text);
                    cmd.Parameters.AddWithValue("@idTelefono", nextIdTelefono);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datos guardados correctamente.");
                    ClearTextBoxes();
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar datos: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Inicie una transacción primero.");
            }
        }

        private void transaccionn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                isTransactionActive = true;
                guardar.Enabled = true;
                MessageBox.Show("Transacción iniciada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar transacción: " + ex.Message);
            }
        }

        private void committ_Click(object sender, EventArgs e)
        {
            if (isTransactionActive)
            {
                try
                {
                    transaction.Commit();
                    isTransactionActive = false;
                    guardar.Enabled = false;
                    MessageBox.Show("Transacción confirmada.");
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al confirmar transacción: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna transacción activa.");
            }
        }

        private void rollbackk_Click(object sender, EventArgs e)
        {
            if (isTransactionActive)
            {
                try
                {
                    transaction.Rollback();
                    isTransactionActive = false;
                    guardar.Enabled = false;
                    MessageBox.Show("Transacción revertida.");
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al revertir transacción: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna transacción activa.");
            }
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void RefreshDataGridView()
        {
            try
            {
                string query = "SELECT c.id, c.Nombre, c.Apellido, c.Direccion, t.Telefono FROM clientes c inner join telefonos t  on c.idTelefono = t.idTelefono;";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar DataGridView: " + ex.Message);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["Nombre"].Value.ToString();
                textBox2.Text = row.Cells["Apellido"].Value.ToString();
                textBox3.Text = row.Cells["Direccion"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                if (textBox4.Text.Length < 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("¡Solo se permiten 8 dígitos!", "Advertencia", MessageBoxButtons.OK);
                }
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("¡Ingrese solo números!", "Advertencia", MessageBoxButtons.OK);
            }
        }
    }
}
