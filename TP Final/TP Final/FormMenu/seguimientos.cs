using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using TP_Final.Datos;
using TP_Final.FormMenu;

namespace TP_Final.FormMenu
{
    public partial class seguimientos : Form
    {

        private Dictionary<int, string> adoptantesDictionary = new Dictionary<int, string>();
        public ComboBox ComboBoxNombresAdoptantes { get; set; }

        private Dictionary<int, string> gatosDictionary = new Dictionary<int, string>();
        public ComboBox ComboBoxNombresGatos { get; set; }

        private Dictionary<int, string> perrosDictionary = new Dictionary<int, string>();
        public ComboBox ComboBoxNombresPerros { get; set; }

        public seguimientos()
        {
            InitializeComponent();
            CargarNombresDeAdoptantesEnComboBox();
            CargarNombresDeGatosEnComboBox();
            CargarNombresDePerrosEnComboBox();

            ActualizarDataGridViewGatos();
            ActualizarDataGridViewPerros();

            comboBoxTipoMascota.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNombresAdoptantes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNombresGatos.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNombresPerros.DropDownStyle = ComboBoxStyle.DropDownList;

        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public ComboboxItem(string text, string value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void BTNSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarNombresDeAdoptantesEnComboBox()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Consulta SQL para obtener los nombres y IDs de los adoptantes
                string consulta = "SELECT AdoptanteID, NombreCompleto FROM adoptantes";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Limpia el ComboBox
                comboBoxNombresAdoptantes.Items.Clear();
                adoptantesDictionary.Clear();

                // Llena el ComboBox y el diccionario
                while (reader.Read())
                {
                    int adoptanteID = reader.GetInt32("AdoptanteID");
                    string nombre = reader.GetString("NombreCompleto");

                    adoptantesDictionary.Add(adoptanteID, nombre);
                    comboBoxNombresAdoptantes.Items.Add(nombre);
                }

                // Cierra la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los nombres de adoptantes: " + ex.Message);
            }
        }

        public void CargarNombresDeGatosEnComboBox()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Consulta SQL para obtener los nombres y IDs de los gatos
                string consulta = "SELECT IDGatito, Nombre FROM gatitos_disponibles";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Limpia el ComboBox de gatos
                comboBoxNombresGatos.Items.Clear();
                gatosDictionary.Clear();

                // Llena el ComboBox de gatos y el diccionario
                while (reader.Read())
                {
                    int gatoID = reader.GetInt32("IDGatito");
                    string nombre = reader.GetString("Nombre");

                    gatosDictionary.Add(gatoID, nombre);
                    comboBoxNombresGatos.Items.Add(nombre);
                }

                // Cierra la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los nombres de los gatos: " + ex.Message);
            }
        }

        public void CargarNombresDePerrosEnComboBox()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Consulta SQL para obtener los nombres y IDs de los perros
                string consulta = "SELECT IDPerro, Nombre FROM perros_disponibles";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Limpia el ComboBox de perros
                comboBoxNombresPerros.Items.Clear();
                perrosDictionary.Clear();

                // Llena el ComboBox de perros y el diccionario
                while (reader.Read())
                {
                    int perroID = reader.GetInt32("IDPerro");
                    string nombre = reader.GetString("Nombre");

                    perrosDictionary.Add(perroID, nombre);
                    comboBoxNombresPerros.Items.Add(nombre);
                }

                // Cierra la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los nombres de los perros: " + ex.Message);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del adoptante seleccionado
            int adoptanteID = ObtenerIDAdoptanteSeleccionado();

            // Obtener el ID del gato seleccionado (si hay uno seleccionado)
            int gatoID = ObtenerIDGatoSeleccionado();

            // Obtener el ID del perro seleccionado (si hay uno seleccionado)
            int perroID = ObtenerIDPerroSeleccionado();

            // Verificar si se ha seleccionado un adoptante
            if (adoptanteID == -1)
            {
                MessageBox.Show("Debe seleccionar un adoptante.");
                return;
            }

            // Verificar si se ha seleccionado un gato o un perro (no ambos)
            if (gatoID != -1 && perroID != -1)
            {
                MessageBox.Show("Debe seleccionar solo un gato o un perro, no ambos.");
                return;
            }

            // Verificar si no se ha seleccionado ni gato ni perro
            if (gatoID == -1 && perroID == -1)
            {
                MessageBox.Show("Debe seleccionar un gato o un perro.");
                return;
            }

            // Guardar la adopción en la base de datos
            if (gatoID != -1)
            {
                InsertarAdopcionGato(adoptanteID, gatoID);
                MessageBox.Show("Adopción de gato guardada exitosamente.");
            }
            else if (perroID != -1)
            {
                InsertarAdopcionPerro(adoptanteID, perroID);
                MessageBox.Show("Adopción de perro guardada exitosamente.");
            }
        }

        private int ObtenerIDAdoptanteSeleccionado()
        {
            if (comboBoxNombresAdoptantes.SelectedItem != null)
            {
                string nombre = comboBoxNombresAdoptantes.SelectedItem.ToString();
                if (adoptantesDictionary.ContainsValue(nombre))
                {
                    return adoptantesDictionary.FirstOrDefault(x => x.Value == nombre).Key;
                }
            }

            return -1; // Valor de retorno para indicar que no se ha seleccionado ningún adoptante
        }

        private int ObtenerIDGatoSeleccionado()
        {
            if (comboBoxNombresGatos.SelectedItem != null)
            {
                string nombre = comboBoxNombresGatos.SelectedItem.ToString();
                if (gatosDictionary.ContainsValue(nombre))
                {
                    return gatosDictionary.FirstOrDefault(x => x.Value == nombre).Key;
                }
            }

            return -1; // Valor de retorno para indicar que no se ha seleccionado ningún gato
        }

        private int ObtenerIDPerroSeleccionado()
        {
            if (comboBoxNombresPerros.SelectedItem != null)
            {
                string nombre = comboBoxNombresPerros.SelectedItem.ToString();
                if (perrosDictionary.ContainsValue(nombre))
                {
                    return perrosDictionary.FirstOrDefault(x => x.Value == nombre).Key;
                }
            }

            return -1; // Valor de retorno para indicar que no se ha seleccionado ningún perro
        }

        private void InsertarAdopcionGato(int adoptanteID, int gatoID)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Consulta para insertar la adopción de gato
                string insertQuery = @"
            INSERT INTO adopciones_gatos (IDAdoptante, IDGatito, FechaAdopcion)
            VALUES (@IDAdoptante, @IDGatito, CURRENT_DATE())";

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@IDAdoptante", adoptanteID);
                insertCommand.Parameters.AddWithValue("@IDGatito", gatoID);
                insertCommand.ExecuteNonQuery();

                // Consulta para actualizar el estado del gato a "Adoptado"
                string updateQuery = @"
            UPDATE gatitos_disponibles
            SET IDEstado = @IDEstado
            WHERE IDGatito = @IDGatito";

                MySqlCommand updateCommand = new MySqlCommand(updateQuery, conn);
                updateCommand.Parameters.AddWithValue("@IDEstado", 2); // ID del estado "Adoptado"
                updateCommand.Parameters.AddWithValue("@IDGatito", gatoID);
                updateCommand.ExecuteNonQuery();

                MessageBox.Show("Adopción de gato guardada exitosamente.");

                // Actualizar el DataGridView de gatos llamando al método existente
                ActualizarDataGridViewGatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la adopción de gato: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void InsertarAdopcionPerro(int adoptanteID, int perroID)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Consulta para insertar la adopción de perro
                string insertQuery = @"
            INSERT INTO adopciones_perros (IDAdoptante, IDPerro, FechaAdopcion)
            VALUES (@IDAdoptante, @IDPerro, CURRENT_DATE())";

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, conn);
                insertCommand.Parameters.AddWithValue("@IDAdoptante", adoptanteID);
                insertCommand.Parameters.AddWithValue("@IDPerro", perroID);
                insertCommand.ExecuteNonQuery();

                // Consulta para actualizar el estado del perro a "Adoptado"
                string updateQuery = @"
            UPDATE perros_disponibles
            SET IDEstado = @IDEstado
            WHERE IDPerro = @IDPerro";

                MySqlCommand updateCommand = new MySqlCommand(updateQuery, conn);
                updateCommand.Parameters.AddWithValue("@IDEstado", 2); // ID del estado "Adoptado"
                updateCommand.Parameters.AddWithValue("@IDPerro", perroID);
                updateCommand.ExecuteNonQuery();

                MessageBox.Show("Adopción de perro guardada exitosamente.");

                // Actualizar el DataGridView de perros llamando al método existente
                ActualizarDataGridViewPerros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la adopción de perro: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ActualizarDataGridViewGatos()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Consulta para obtener los datos de adopciones de gatos
                string query = @"
            SELECT ag.IDAdopcion, a.NombreCompleto AS Adoptante, gd.Nombre AS Gatito, ag.FechaAdopcion
            FROM adopciones_gatos ag
            INNER JOIN adoptantes a ON ag.IDAdoptante = a.AdoptanteID
            INNER JOIN gatitos_disponibles gd ON ag.IDGatito = gd.IDGatito";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Asignar los datos al DataGridView de gatos
                dataGridViewGatos.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el DataGridView de gatos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ActualizarDataGridViewPerros()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Consulta para obtener los datos de adopciones de perros
                string query = @"
            SELECT ap.IDAdopcionPerro, a.NombreCompleto AS Adoptante, pd.Nombre AS Perro, ap.FechaAdopcion
            FROM adopciones_perros ap
            INNER JOIN adoptantes a ON ap.IDAdoptante = a.AdoptanteID
            INNER JOIN perros_disponibles pd ON ap.IDPerro = pd.IDPerro";

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Asignar los datos al DataGridView de perros
                dataGridViewPerros.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el DataGridView de perros: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        
     
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBoxBuscar.Text.Trim();
            string tipoMascota = comboBoxTipoMascota.Text.Trim();

            if (string.IsNullOrEmpty(tipoMascota))
            {
                MessageBox.Show("Por favor, selecciona el tipo de mascota en el ComboBox.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tipoMascota == "Adopciones de Gatos")
            {
                ConexionBD conexion = new ConexionBD();
                MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

                string consulta = "SELECT ag.IDAdopcion, a.NombreCompleto AS Adoptante, gd.Nombre AS Gatito, ag.FechaAdopcion " +
                         "FROM adopciones_gatos ag " +
                         "INNER JOIN adoptantes a ON ag.IDAdoptante = a.AdoptanteID " +
                         "INNER JOIN gatitos_disponibles gd ON ag.IDGatito = gd.IDGatito " +
                         "WHERE ";

                if (chkBoxAdoptante.Checked)
                {
                    consulta += "a.NombreCompleto LIKE @Criterio OR ";
                }
                if (chkBoxNombreMas.Checked)
                {
                    consulta += "gd.Nombre LIKE @Criterio OR ";
                }
                if (chkBoxFechaAdopcion.Checked)
                {
                    consulta += "ag.FechaAdopcion LIKE @Criterio OR ";
                }

                consulta = consulta.TrimEnd(" OR ".ToCharArray()); // Eliminar el último "OR" innecesario

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Criterio", "%" + criterioBusqueda + "%");

                try
                {
                    // Ejecutar la consulta y obtener los resultados
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se encontraron resultados
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Aquí puedes realizar alguna acción adicional si no se encontraron resultados
                    }
                    else
                    {
                        // Actualizar el DataGridView con los resultados de la búsqueda de gatos
                        dataGridViewGatos.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tipoMascota == "Adopciones de Perros")
            {
                ConexionBD conexion = new ConexionBD();
                MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

                string consulta = "SELECT ap.IDAdopcionPerro, a.NombreCompleto AS Adoptante, pd.Nombre AS Perro, ap.FechaAdopcion " +
                  "FROM adopciones_perros ap " +
                  "INNER JOIN adoptantes a ON ap.IDAdoptante = a.AdoptanteID " +
                  "INNER JOIN perros_disponibles pd ON ap.IDPerro = pd.IDPerro " +
                  "WHERE ";

                if (chkBoxAdoptante.Checked)
                {
                    consulta += "a.NombreCompleto LIKE @Criterio OR ";
                }
                if (chkBoxNombreMas.Checked)
                {
                    consulta += "pd.Nombre LIKE @Criterio OR ";
                }
                if (chkBoxFechaAdopcion.Checked)
                {
                    consulta += "ap.FechaAdopcion LIKE @Criterio OR ";
                }

                consulta = consulta.TrimEnd(" OR ".ToCharArray()); // Eliminar el último "OR" innecesario

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                cmd.Parameters.AddWithValue("@Criterio", "%" + criterioBusqueda + "%");

                try
                {
                    // Ejecutar la consulta y obtener los resultados
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Verificar si se encontraron resultados
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron resultados.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Aquí puedes realizar alguna acción adicional si no se encontraron resultados
                    }
                    else
                    {
                        // Actualizar el DataGridView con los resultados de la búsqueda de adopciones de perros
                        dataGridViewPerros.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona el tipo de mascota (Adopciones de Gatos o Adopciones de Perros) en el ComboBox.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRestaurarG_Click(object sender, EventArgs e)
        {
            string tipoMascota = comboBoxTipoMascota.Text.Trim();

            if (string.IsNullOrEmpty(tipoMascota))
            {
                MessageBox.Show("Por favor, selecciona el tipo de mascota en el ComboBox.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tipoMascota == "Adopciones de Gatos")
            {
                ActualizarDataGridViewGatos();
            }
            else if (tipoMascota == "Adopciones de Perros")
            {
                ActualizarDataGridViewPerros();
            }
            else
            {
                MessageBox.Show("Tipo de mascota inválido. Por favor, selecciona una opción válida en el ComboBox.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        }
    }

