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
                MessageBox.Show("Inicie una transacci�n primero.");
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
                MessageBox.Show("Transacci�n iniciada.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar transacci�n: " + ex.Message);
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
                    MessageBox.Show("Transacci�n confirmada.");
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al confirmar transacci�n: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna transacci�n activa.");
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
                    MessageBox.Show("Transacci�n revertida.");
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al revertir transacci�n: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna transacci�n activa.");
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
                string query = "SELECT * FROM Clientes";
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
    }
}
