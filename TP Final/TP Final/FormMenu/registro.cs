using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TP_Final.Datos;
using TP_Final.FormMenu;

namespace TP_Final.FormMenu
{

    public partial class registro : Form
    {
        // En el formulario con el botón de agregar
        private DatosAdicionales DatosAdicionales;
 
        private ConexionBD conexion = new ConexionBD();
        public registro()
        {
            InitializeComponent();
            CargarDatos(); // Llama a la función para cargar datos en el DataGridView al iniciar el formulario

            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
         

            DatosAdicionales = new DatosAdicionales(); // Inicialización de la variable

            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            dataGridViewDirecciones.CellDoubleClick += dataGridViewDirecciones_CellDoubleClick;
            dataGridViewCiudades.CellDoubleClick += dataGridViewCiudades_CellDoubleClick;
     


        }
        #region funciones formulario


        private void hideSubMenu()
        {
            panelRegistro.Visible = false;
        }

        private void showSubMenu()
        {
            hideSubMenu();
            panelRegistro.Visible = true;
        }

        private void BTNRegistro_Click(object sender, EventArgs e)
        {
            showSubMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
        #endregion

        private void CargarDatos()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();
                string consulta = "SELECT a.AdoptanteID, a.NombreCompleto, a.Edad, a.FechaNac, a.DNI, a.Email, a.Celular, a.Ocupacion, ec.EstadoCivil " +
                    "FROM adoptantes a " +
                    "LEFT JOIN estadosciviles ec ON a.EstadoCivilID = ec.EstadoCivilID " +
                    "LEFT JOIN adoptante_ciudades ac ON a.AdoptanteID = ac.AdoptanteID " +
                    "GROUP BY a.AdoptanteID, a.NombreCompleto, a.Edad, a.FechaNac, a.DNI, a.Email, a.Celular, a.Ocupacion, ec.EstadoCivil";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Enlaza los datos al DataGridView
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int adoptanteIDSeleccionado;

                if (dataGridView1.SelectedRows[0].Cells["AdoptanteID"].Value != null &&
                    int.TryParse(dataGridView1.SelectedRows[0].Cells["AdoptanteID"].Value.ToString(), out adoptanteIDSeleccionado))
                {
                    // El valor se pudo convertir a un entero con éxito
                    CargarCiudadesYDirecciones(adoptanteIDSeleccionado, conexion);

                    if (adoptanteIDSeleccionado > 0)
                    {
                        DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                        // Captura los valores de las celdas de la fila seleccionada y colócalos en los campos de entrada
                        txtNombreCompleto.Text = selectedRow.Cells["NombreCompleto"].Value.ToString();
                        txtDNI.Text = selectedRow.Cells["DNI"].Value.ToString();
                        txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                        txtCelular.Text = selectedRow.Cells["Celular"].Value.ToString();
                        txtTrabajo.Text = selectedRow.Cells["Ocupacion"].Value.ToString();

                        DateTime fechaNacimiento = (DateTime)selectedRow.Cells["FechaNac"].Value;
                        dateTimePickerFechaNacimiento.Value = fechaNacimiento;
                    }
                    else
                    {
                        MessageBox.Show("Error al seleccionar fila. Debe seleccionar un registro con datos.");
                    }
                }
                else
                {
                    // Mostrar un mensaje de error si no se pudo convertir el valor de AdoptanteID a un entero
                    MessageBox.Show("El valor de AdoptanteID no es válido.");
                }
            }
        }

        private void CargarCiudadesYDirecciones(int adoptanteID, ConexionBD conexion)
        {
            dataGridViewCiudades.Rows.Clear();
            dataGridViewDirecciones.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
            {
                conn.Open();

                // Consulta las ciudades y las agrega al DataGridView de direcciones
                string consultaCiudades = "SELECT Ciudad, AdoptanteID FROM adoptante_ciudades WHERE AdoptanteID = @AdoptanteID";
                MySqlCommand cmdCiudades = new MySqlCommand(consultaCiudades, conn);
                cmdCiudades.Parameters.AddWithValue("@AdoptanteID", adoptanteID);

                using (MySqlDataReader readerCiudades = cmdCiudades.ExecuteReader())
                {
                    // Eliminar columnas existentes (si las hubiera)
                    dataGridViewCiudades.Columns.Clear();

                    // Agregar columna "Ciudad" al DataGridView de direcciones
                    dataGridViewCiudades.Columns.Add("CiudadColumn", "Ciudad");

                    // Agregar columna "AdoptanteID" al DataGridView de direcciones
                    dataGridViewCiudades.Columns.Add("AdoptanteIDColumn", "AdoptanteID");

                    while (readerCiudades.Read())
                    {
                        string ciudad = readerCiudades.GetString("Ciudad");
                        string adoptanteIDValue = readerCiudades.GetString("AdoptanteID");
                        dataGridViewCiudades.Rows.Add(ciudad, adoptanteIDValue);
                    }
                }

                // Consulta las direcciones y las agrega al DataGridView de direcciones
                string consultaDirecciones = "SELECT Direccion, AdoptanteID FROM adoptante_direcciones WHERE AdoptanteID = @AdoptanteID";
                MySqlCommand cmdDirecciones = new MySqlCommand(consultaDirecciones, conn);
                cmdDirecciones.Parameters.AddWithValue("@AdoptanteID", adoptanteID);

                using (MySqlDataReader readerDirecciones = cmdDirecciones.ExecuteReader())
                {
                    // Eliminar columnas existentes (si las hubiera)
                    dataGridViewDirecciones.Columns.Clear();

                    // Agregar columna "Dirección" al DataGridView de direcciones
                    dataGridViewDirecciones.Columns.Add("DireccionColumn", "Dirección");

                    // Agregar columna "AdoptanteID" al DataGridView de direcciones
                    dataGridViewDirecciones.Columns.Add("AdoptanteIDColumn", "AdoptanteID");

                    while (readerDirecciones.Read())
                    {
                        string direccion = readerDirecciones.GetString("Direccion");
                        string adoptanteIDValue = readerDirecciones.GetString("AdoptanteID");
                        dataGridViewDirecciones.Rows.Add(direccion, adoptanteIDValue);
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                string nombreCompleto = txtNombreCompleto.Text;
                string dni = txtDNI.Text;
                string email = txtEmail.Text;
                string celular = txtCelular.Text;
                string trabajo = txtTrabajo.Text;
                DateTime fechaNacimiento = dateTimePickerFechaNacimiento.Value;

                // Verifica campos obligatorios vacíos
                if (string.IsNullOrEmpty(nombreCompleto) || string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(celular))
                {
                    MessageBox.Show("Los campos Nombre Completo, DNI, Email y Celular son obligatorios. Por favor, asegúrate de completarlos antes de guardar.", "Campos obligatorios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Calcula la edad en años
                TimeSpan edad = DateTime.Now - fechaNacimiento;
                int edadEnAños = edad.Days / 365;

                string estadoCivil = comboBoxEstadoCivil.SelectedItem.ToString();

                if (comboBoxEstadoCivil.SelectedIndex == 0)
                {
                    estadoCivil = "1";
                }
                else if (comboBoxEstadoCivil.SelectedIndex == 1)
                {
                    estadoCivil = "2";
                }
                else if (comboBoxEstadoCivil.SelectedIndex == 2)
                {
                    estadoCivil = "3";
                }
                else if (comboBoxEstadoCivil.SelectedIndex == 3)
                {
                    estadoCivil = "4";
                }

                // Inserta el adoptante
                string consultaInsertarAdoptante = "INSERT INTO adoptantes (NombreCompleto, Edad, FechaNac, DNI, Email, Celular, Ocupacion, EstadoCivilID) " +
                    "VALUES (@nombreCompleto, @edad, @fecha, @dni, @email, @celular, @ocupacion, @estadoCivilID)";

                MySqlCommand cmdInsertarAdoptante = new MySqlCommand(consultaInsertarAdoptante, conn);
                cmdInsertarAdoptante.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                cmdInsertarAdoptante.Parameters.AddWithValue("@edad", edadEnAños);
                cmdInsertarAdoptante.Parameters.AddWithValue("@fecha", fechaNacimiento);
                cmdInsertarAdoptante.Parameters.AddWithValue("@dni", dni);
                cmdInsertarAdoptante.Parameters.AddWithValue("@email", email);
                cmdInsertarAdoptante.Parameters.AddWithValue("@celular", celular);
                cmdInsertarAdoptante.Parameters.AddWithValue("@ocupacion", trabajo);
                cmdInsertarAdoptante.Parameters.AddWithValue("@estadoCivilID", estadoCivil);

                cmdInsertarAdoptante.ExecuteNonQuery();

                int adoptanteID = (int)cmdInsertarAdoptante.LastInsertedId;

                string ciudad = txtCiudad.Text; // Obtén la ciudad desde tu formulario
                string direccion = txtDireccion.Text; // Obtén la dirección desde tu formulario

                // Verifica campos obligatorios vacíos nuevamente
                if (string.IsNullOrEmpty(ciudad) || string.IsNullOrEmpty(direccion))
                {
                    MessageBox.Show("Los campos Ciudad y Dirección son obligatorios. Por favor, asegúrate de completarlos antes de guardar.", "Campos obligatorios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Inserta la ciudad
                string consultaInsertarCiudad = "INSERT INTO adoptante_ciudades (AdoptanteID, Ciudad) VALUES (@AdoptanteID, @Ciudad)";
                MySqlCommand cmdInsertarCiudad = new MySqlCommand(consultaInsertarCiudad, conn);
                cmdInsertarCiudad.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                cmdInsertarCiudad.Parameters.AddWithValue("@Ciudad", ciudad);

                cmdInsertarCiudad.ExecuteNonQuery();

                // Inserta la dirección
                string consultaInsertarDireccion = "INSERT INTO adoptante_direcciones (AdoptanteID, Direccion) VALUES (@AdoptanteID, @Direccion)";
                MySqlCommand cmdInsertarDireccion = new MySqlCommand(consultaInsertarDireccion, conn);
                cmdInsertarDireccion.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                cmdInsertarDireccion.Parameters.AddWithValue("@Direccion", direccion);

                cmdInsertarDireccion.ExecuteNonQuery();

        // Limpia los controles
        LimpiarControles();

        dataGridView1.Refresh();

        // Recarga los nombres de los adoptantes en el ComboBox
        DatosAdicionales.CargarNombresDeAdoptantesEnComboBox();

        // Vuelve a cargar las ciudades y direcciones
        CargarCiudadesYDirecciones(adoptanteID, conexion);
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al guardar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        }

        private void LimpiarControles()
        {
            txtNombreCompleto.Clear();
            txtCiudad.Clear();
            txtDNI.Clear();
            txtEmail.Clear();
            txtCelular.Clear();
            txtTrabajo.Clear();

        }

        private void btnAgregarCiudad_Click(object sender, EventArgs e)
        {
            // Validar que el campo de ciudad no esté vacío u otras validaciones necesarias
            if (string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                MessageBox.Show("Debes ingresar una ciudad válida.");
                return;
            }

            // Obtén el AdoptanteID seleccionado desde el DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un adoptante.");
                return;
            }

            int adoptanteID = (int)dataGridView1.SelectedRows[0].Cells["AdoptanteID"].Value;

            // Inserta la ciudad en la base de datos
            string ciudad = txtCiudad.Text; // Obtiene la ciudad desde el campo de entrada

            using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
            {
                conn.Open();

                string consultaInsertarCiudad = "INSERT INTO adoptante_ciudades (AdoptanteID, Ciudad) VALUES (@AdoptanteID, @Ciudad)";
                MySqlCommand cmdInsertarCiudad = new MySqlCommand(consultaInsertarCiudad, conn);
                cmdInsertarCiudad.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                cmdInsertarCiudad.Parameters.AddWithValue("@Ciudad", ciudad);

                cmdInsertarCiudad.ExecuteNonQuery();
            }

            // Limpia el campo de entrada de la ciudad
            txtCiudad.Text = "";

            // Recarga las ciudades
            CargarCiudadesYDirecciones(adoptanteID, conexion);
        }

        private void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            
            try
{
            // Validar que el campo de dirección no esté vacío u otras validaciones necesarias
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Debes ingresar una dirección válida.");
                return;
            }

            // Obtén el AdoptanteID seleccionado desde el DataGridView
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debes seleccionar un adoptante.");
                return;
            }

            int adoptanteID = (int)dataGridView1.SelectedRows[0].Cells["AdoptanteID"].Value;

            // Inserta la dirección en la base de datos
            string direccion = txtDireccion.Text; // Obtiene la dirección desde el campo de entrada

            using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
            {
                conn.Open();

                string consultaInsertarDireccion = "INSERT INTO adoptante_direcciones (AdoptanteID, Direccion) VALUES (@AdoptanteID, @Direccion)";
                MySqlCommand cmdInsertarDireccion = new MySqlCommand(consultaInsertarDireccion, conn);
                cmdInsertarDireccion.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                cmdInsertarDireccion.Parameters.AddWithValue("@Direccion", direccion);

                cmdInsertarDireccion.ExecuteNonQuery();
            }

            // Limpia el campo de entrada de la dirección
            txtDireccion.Text = "";

            // Recarga las direcciones
            CargarCiudadesYDirecciones(adoptanteID, conexion);
}
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la dirección: " + ex.Message);
            }
        }

        private void BTNsalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        
        private void btnEliminarR_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Obtener el ID del registro seleccionado en el DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int adoptanteID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AdoptanteID"].Value);

                    // Eliminar los registros relacionados en la tabla "adoptante_direcciones"
                    string consultaEliminarDirecciones = "DELETE FROM adoptante_direcciones WHERE AdoptanteID = @AdoptanteID";
                    MySqlCommand cmdEliminarDirecciones = new MySqlCommand(consultaEliminarDirecciones, conn);
                    cmdEliminarDirecciones.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                    conn.Open();
                    cmdEliminarDirecciones.ExecuteNonQuery();
                    conn.Close();

                    // Eliminar los registros relacionados en la tabla "adoptante_ciudades"
                    string consultaEliminarCiudades = "DELETE FROM adoptante_ciudades WHERE AdoptanteID = @AdoptanteID";
                    MySqlCommand cmdEliminarCiudades = new MySqlCommand(consultaEliminarCiudades, conn);
                    cmdEliminarCiudades.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                    conn.Open();
                    cmdEliminarCiudades.ExecuteNonQuery();
                    conn.Close();

                    // Eliminar el registro del adoptante en la tabla "adoptantes"
                    string consultaEliminarAdoptante = "DELETE FROM adoptantes WHERE AdoptanteID = @AdoptanteID";
                    MySqlCommand cmdEliminarAdoptante = new MySqlCommand(consultaEliminarAdoptante, conn);
                    cmdEliminarAdoptante.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                    conn.Open();
                    int filasAfectadas = cmdEliminarAdoptante.ExecuteNonQuery();
                    conn.Close();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Registro, direcciones y ciudades eliminadas correctamente.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Vuelve a cargar las ciudades y direcciones asociadas al adoptante
                        CargarCiudadesYDirecciones(adoptanteID, conexion);
                        CargarDatos(); // Vuelve a cargar los datos en el DataGridView después de eliminar el registro, las direcciones y las ciudades
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Selecciona un registro para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro, las direcciones y las ciudades: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fechaNacColumnIndex = 3; // Índice de la columna de fecha de nacimiento
            int edadColumnIndex = 2; // Índice de la columna de edad
            int nombreCompletoColumnIndex = 1; // Índice de la columna de NombreCompleto
            int dniColumnIndex = 4; // Índice de la columna de DNI
            int emailColumnIndex = 5; // Índice de la columna de Email
            int celularColumnIndex = 6; // Índice de la columna de Celular
            int ocupacionColumnIndex = 7; // Índice de la columna de Ocupacion
            int estadoCivilColumnIndex = 8; // Índice de la columna de EstadoCivil

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            DataGridViewColumn selectedColumn = dataGridView1.Columns[e.ColumnIndex];

            if (e.ColumnIndex == fechaNacColumnIndex)
            {
                string nuevaFecha = Microsoft.VisualBasic.Interaction.InputBox("Ingrese una nueva fecha de nacimiento", "Cambio de Fecha", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                DateTime fecha;
                if (DateTime.TryParse(nuevaFecha, out fecha))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fecha;

                    // Calcula la edad en años
                    TimeSpan edad = DateTime.Now - fecha;
                    int edadEnAños = edad.Days / 365;

                    // Actualiza la celda de edad
                    dataGridView1.Rows[e.RowIndex].Cells[edadColumnIndex].Value = edadEnAños;

                    int adoptanteID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value); // Obtén el ID del adoptante

                    // Actualizar la fecha de nacimiento y la edad en la base de datos
                    using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE adoptantes SET FechaNac = @nuevaFecha, Edad = @edadEnAños WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@nuevaFecha", fecha);
                        cmd.Parameters.AddWithValue("@edadEnAños", edadEnAños);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (e.ColumnIndex == nombreCompletoColumnIndex)
            {
                string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para NombreCompleto", "Editar NombreCompleto", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    // Actualizar el campo en la base de datos
                    int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value); // Obtén el ID del adoptante

                    using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE adoptantes SET NombreCompleto = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            else if (e.ColumnIndex == dniColumnIndex)
            {
                string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para DNI", "Editar DNI", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value);

                    using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE adoptantes SET DNI = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
                else if (e.ColumnIndex == emailColumnIndex)
    {
        string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
    string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para Email", "Editar Email", currentValue);

    if (!string.IsNullOrEmpty(newValue))
    {
        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

        int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value);

        using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
        {
            conn.Open();
            string consulta = "UPDATE adoptantes SET Email = @newValue WHERE AdoptanteID = @adoptanteID";
            MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@newValue", newValue);
            cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
            cmd.ExecuteNonQuery();
        }
    }
    }


    else if (e.ColumnIndex == celularColumnIndex)
    {
         string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
    string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para Celular", "Editar Celular", currentValue);

    if (!string.IsNullOrEmpty(newValue))
    {
        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

        int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value);

        using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
        {
            conn.Open();
            string consulta = "UPDATE adoptantes SET Celular = @newValue WHERE AdoptanteID = @adoptanteID";
            MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@newValue", newValue);
            cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
            cmd.ExecuteNonQuery();
        }
    }
    }
    else if (e.ColumnIndex == ocupacionColumnIndex)
{
    string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
    string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para Ocupación", "Editar Ocupación", currentValue);

    if (!string.IsNullOrEmpty(newValue))
    {
        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

        int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value);

        using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
        {
            conn.Open();
            string consulta = "UPDATE adoptantes SET Ocupacion = @newValue WHERE AdoptanteID = @adoptanteID";
            MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@newValue", newValue);
            cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
            cmd.ExecuteNonQuery();
        }
    }
}
            else if (e.ColumnIndex == estadoCivilColumnIndex)
            {
                string currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para Estado Civil", "Editar Estado Civil", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    // Verificar si el valor ingresado es válido
                    List<string> opcionesEstadoCivil = new List<string>() { "soltero(a)", "casado(a)", "divorciado(a)", "viudo(a)" };
                    if (opcionesEstadoCivil.Contains(newValue.ToLower()))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                        int adoptanteID = Convert.ToInt32(selectedRow.Cells[0].Value);

                        int estadoCivilID = opcionesEstadoCivil.IndexOf(newValue.ToLower()) + 1;

                        using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                        {
                            conn.Open();
                            string consulta = "UPDATE adoptantes SET EstadoCivilID = @estadoCivilID WHERE AdoptanteID = @adoptanteID";
                            MySqlCommand cmd = new MySqlCommand(consulta, conn);
                            cmd.Parameters.AddWithValue("@estadoCivilID", estadoCivilID);
                            cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("El valor ingresado para el estado civil no es válido. Por favor, elija una de las siguientes opciones: soltero(a), casado(a), divorciado(a), viudo(a).", "Error de estado civil", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            }

        private void dataGridViewDirecciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int direccionColumnIndex = 0; // Índice de la columna de dirección (0 para la primera columna)

            if (e.ColumnIndex == direccionColumnIndex)
            {
                DataGridViewRow selectedRow = dataGridViewDirecciones.Rows[e.RowIndex];

                string currentValue = selectedRow.Cells[direccionColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para la dirección", "Editar Dirección", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    int adoptanteID; // Variable para almacenar adoptanteID

                    // Obtener el valor de adoptanteID (reemplaza esto con tu forma de obtenerlo)
                    if (!int.TryParse(selectedRow.Cells["AdoptanteIDColumn"].Value.ToString(), out adoptanteID))
                    {
                        // Manejar el error si no se puede convertir a entero
                        MessageBox.Show("Error al obtener el valor de adoptanteID");
                        return;
                    }

                    using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();

                        // Actualizar la dirección en la base de datos
                        string updateDireccionQuery = "UPDATE adoptante_direcciones SET Direccion = @Direccion WHERE AdoptanteID = @AdoptanteID";
                        MySqlCommand cmdUpdateDireccion = new MySqlCommand(updateDireccionQuery, conn);
                        cmdUpdateDireccion.Parameters.AddWithValue("@Direccion", newValue);
                        cmdUpdateDireccion.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                        cmdUpdateDireccion.ExecuteNonQuery();

                        // Actualizar el valor en el DataGridView
                        selectedRow.Cells[direccionColumnIndex].Value = newValue;

                        // Mostrar un mensaje de éxito
                        MessageBox.Show("La dirección se ha actualizado correctamente.");
                    }
                }
            }
        }

        private void dataGridViewCiudades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ciudadColumnIndex = 0; // Índice de la columna de dirección (0 para la primera columna)

            if (e.ColumnIndex == ciudadColumnIndex)
            {
                DataGridViewRow selectedRow = dataGridViewDirecciones.Rows[e.RowIndex];

                string currentValue = selectedRow.Cells[ciudadColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para la ciudad", "Editar Ciudad", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    int adoptanteID; // Variable para almacenar adoptanteID

                    // Obtener el valor de adoptanteID (reemplaza esto con tu forma de obtenerlo)
                    if (!int.TryParse(selectedRow.Cells["AdoptanteIDColumn"].Value.ToString(), out adoptanteID))
                    {
                        // Manejar el error si no se puede convertir a entero
                        MessageBox.Show("Error al obtener el valor de adoptanteID");
                        return;
                    }

                    using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();

                        // Actualizar la dirección en la base de datos
                        string updateDireccionQuery = "UPDATE adoptante_Ciudad SET Ciudad = @Ciudad WHERE AdoptanteID = @AdoptanteID";
                        MySqlCommand cmdUpdateDireccion = new MySqlCommand(updateDireccionQuery, conn);
                        cmdUpdateDireccion.Parameters.AddWithValue("@Ciudad", newValue);
                        cmdUpdateDireccion.Parameters.AddWithValue("@AdoptanteID", adoptanteID);
                        cmdUpdateDireccion.ExecuteNonQuery();

                        // Actualizar el valor en el DataGridView
                        selectedRow.Cells[ciudadColumnIndex].Value = newValue;

                        // Mostrar un mensaje de éxito
                        MessageBox.Show("La dirección se ha actualizado correctamente.");
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBoxBuscar.Text.Trim();
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            string consulta = "SELECT AdoptanteID, NombreCompleto, FechaNac, Edad, DNI, Email, Celular, Ocupacion " +
                "FROM adoptantes WHERE ";

            // Verificar las opciones seleccionadas
            List<string> opcionesSeleccionadas = new List<string>();

            if (checkBoxNombreCompleto.Checked)
                opcionesSeleccionadas.Add("NombreCompleto LIKE @Criterio");

            if (checkBoxDNI.Checked)
                opcionesSeleccionadas.Add("DNI LIKE @Criterio");

            if (checkBoxEmail.Checked)
                opcionesSeleccionadas.Add("Email LIKE @Criterio");

            if (checkBoxCelular.Checked)
                opcionesSeleccionadas.Add("Celular LIKE @Criterio");

            // Construir la parte de la consulta para las opciones seleccionadas
            consulta += string.Join(" OR ", opcionesSeleccionadas);

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
                    // Actualizar el DataGridView con los resultados de la búsqueda
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        
        
    }
}

      

        

       

