using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Transacciones
{
    public partial class Ventana2 : Form
    {
        private MySqlConnection connection;
        private MySqlTransaction transaction;
        private bool isTransactionActive = false;
        public event Action OnDataChanged;

        public Ventana2()
        {
            InitializeComponent();
            connection = new MySqlConnection("server=localhost;database=transacciones2;user=root;password=holamundo123;");
            guardar.Enabled = false;
            RefreshDataGridView();
            MostrarNivelAislamientoActual();


        }
        private void NotifyDataChanged()
        {
            OnDataChanged?.Invoke();
        }
        private void MostrarNivelAislamientoActual()
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT @@transaction_isolation;", connection);
                string nivelAislamiento = cmd.ExecuteScalar().ToString();

                switch (nivelAislamiento.ToUpper())
                {
                    case "READ-UNCOMMITTED":
                        label9.Text = "Nivel de aislamiento actual: Lecturas no comprometidas";
                        break;
                    case "READ-COMMITTED":
                        label9.Text = "Nivel de aislamiento actual: Lecturas comprometidas";
                        break;
                    case "REPEATABLE-READ":
                        label9.Text = "Nivel de aislamiento actual: Lecturas repetibles";
                        break;
                    case "SERIALIZABLE":
                        label9.Text = "Nivel de aislamiento actual: Serializable";
                        break;
                    default:
                        label9.Text = "Nivel de aislamiento actual: Desconocido";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el nivel de aislamiento: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (isTransactionActive)
            {
                try
                {
                    // Insertar el cliente primero
                    string insertClientQuery = "INSERT INTO Clientes (Nombre, Apellido, Direccion) VALUES (@Nombre, @Apellido, @Direccion)";
                    MySqlCommand insertClientCmd = new MySqlCommand(insertClientQuery, connection, transaction);
                    insertClientCmd.Parameters.AddWithValue("@Nombre", textBox1.Text);
                    insertClientCmd.Parameters.AddWithValue("@Apellido", textBox2.Text);
                    insertClientCmd.Parameters.AddWithValue("@Direccion", textBox3.Text);
                    insertClientCmd.ExecuteNonQuery();

                    // Obtener el id del cliente insertado
                    int clientId = (int)insertClientCmd.LastInsertedId;

                    // Insertar el tel�fono
                    string insertPhoneQuery = "INSERT INTO Telefonos (Telefono, Clientes_id) VALUES (@Telefono, @Clientes_id)";
                    MySqlCommand insertPhoneCmd = new MySqlCommand(insertPhoneQuery, connection, transaction);
                    insertPhoneCmd.Parameters.AddWithValue("@Telefono", textBox4.Text);
                    insertPhoneCmd.Parameters.AddWithValue("@Clientes_id", clientId);
                    insertPhoneCmd.ExecuteNonQuery();

                    MessageBox.Show("Datos guardados correctamente.");
                    ClearTextBoxes();
                    RefreshDataGridView();
                    NotifyDataChanged();
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
                IsolationLevel isolationLevel = GetIsolationLevel();

                // Abrir la conexi�n
                connection.Open();

                // Establecer el nivel de aislamiento para la sesi�n
                MySqlCommand setIsolationLevelCmd = new MySqlCommand($"SET SESSION TRANSACTION ISOLATION LEVEL {GetIsolationLevelAsString(isolationLevel)};", connection);
                setIsolationLevelCmd.ExecuteNonQuery();

                // Iniciar la transacci�n con el nivel de aislamiento especificado
                transaction = connection.BeginTransaction(isolationLevel);
                isTransactionActive = true;
                guardar.Enabled = true;
                label9.Text = "Nivel de aislamiento: " + comboBox1.SelectedItem.ToString();
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
                    NotifyDataChanged();
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
                    NotifyDataChanged();
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
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
        }

        public void RefreshDataGridView()
        {
            try
            {
                string query = "SELECT c.id, c.Nombre, c.Apellido, c.Direccion, t.Telefono FROM Clientes c LEFT JOIN Telefonos t ON c.id = t.Clientes_id;";
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
                textBox1.Tag = row.Cells["id"].Value.ToString(); // Guardar el ID del cliente en el Tag del TextBox
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
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
                    MessageBox.Show("�Solo se permiten 8 d�gitos!", "Advertencia", MessageBoxButtons.OK);
                }
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("�Ingrese solo n�meros!", "Advertencia", MessageBoxButtons.OK);
            }
        }

        private void agregartelefono_Click(object sender, EventArgs e)
        {
            if (isTransactionActive)
            {
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    try
                    {
                        // Obtener el ID del cliente del TextBox1.Tag
                        int clientId = int.Parse(textBox1.Tag.ToString());

                        // Insertar el tel�fono
                        string insertPhoneQuery = "INSERT INTO Telefonos (Telefono, Clientes_id) VALUES (@Telefono, @Clientes_id)";
                        MySqlCommand insertPhoneCmd = new MySqlCommand(insertPhoneQuery, connection, transaction);
                        insertPhoneCmd.Parameters.AddWithValue("@Telefono", textBox4.Text);
                        insertPhoneCmd.Parameters.AddWithValue("@Clientes_id", clientId);
                        insertPhoneCmd.ExecuteNonQuery();

                        MessageBox.Show("Tel�fono agregado correctamente.");
                        textBox4.Clear();
                        RefreshDataGridView();
                        NotifyDataChanged();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al agregar tel�fono: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un n�mero de tel�fono.");
                }
            }
            else
            {
                MessageBox.Show("Inicie una transacci�n primero.");
            }
        }
        private IsolationLevel GetIsolationLevel()
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Lecturas no comprometidas":
                    return IsolationLevel.ReadUncommitted;
                case "Lecturas comprometidas":
                    return IsolationLevel.ReadCommitted;
                case "Lecturas repetibles":
                    return IsolationLevel.RepeatableRead;
                case "Serializable":
                    return IsolationLevel.Serializable;
                default:
                    return IsolationLevel.RepeatableRead;
            }
        }
        private string GetIsolationLevelAsString(IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case IsolationLevel.ReadUncommitted:
                    return "READ UNCOMMITTED";
                case IsolationLevel.ReadCommitted:
                    return "READ COMMITTED";
                case IsolationLevel.RepeatableRead:
                    return "REPEATABLE READ";
                case IsolationLevel.Serializable:
                    return "SERIALIZABLE";
                default:
                    return "REPEATABLE READ"; // Valor por defecto
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label9.Text = "Nivel de aislamiento: " + comboBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }
}
