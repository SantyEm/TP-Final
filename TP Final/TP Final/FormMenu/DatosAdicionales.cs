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
using TP_Final.Datos;

namespace TP_Final.FormMenu
{

    public partial class DatosAdicionales : Form
    {
        // Define el diccionario aquí para que sea accesible en toda la clase
        private Dictionary<int, string> adoptantesDictionary = new Dictionary<int, string>();
         public ComboBox ComboBoxNombresAdoptantes { get; set; }
        public DatosAdicionales()
        {
            InitializeComponent();
            CargarDatosEnDataGridView();
            CargarNombresDeAdoptantesEnComboBox();
            CargarMascotasEnDataGridView();

            // Inicializa el ComboBox
            comboBoxNombresAdoptantes.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNombresAdoptantes.SelectedIndexChanged += ComboBoxNombresAdoptantes_SelectedIndexChanged;

            dgvDatosAdicionales.CellDoubleClick += dgvDatosAdicionales_CellContentDoubleClick;

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

        private void CargarDatosEnDataGridView()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Consulta SQL para combinar las tablas otros_datos_adoptante y adoptantes
                string consulta = "SELECT adoptantes.NombreCompleto, otros_datos_adoptante.* " +
                                  "FROM otros_datos_adoptante " +
                                  "INNER JOIN adoptantes ON otros_datos_adoptante.AdoptanteID = adoptantes.AdoptanteID";

                // Crea un adaptador de datos
                MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conn);

                // Crea un objeto DataTable para almacenar los datos
                DataTable tablaDatos = new DataTable();

                // Llena el DataTable con los datos de la consulta
                adaptador.Fill(tablaDatos);

                // Asigna el DataTable como origen de datos del DataGridView
                dgvDatosAdicionales.AutoGenerateColumns = true;  // Asegúrate de que esta propiedad esté en true si no has definido manualmente las columnas del DataGridView
                dgvDatosAdicionales.DataSource = tablaDatos;  // Asegúrate de que el nombre del DataGridView sea el correcto

                // Cierra la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void CargarMascotasEnDataGridView()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Consulta SQL para seleccionar los registros de la tabla mascotas_adoptante_adicionales relacionados con un Adoptante específico
                string consulta = "SELECT * FROM mascotas_adoptante_adicionales WHERE AdoptanteID = @AdoptanteID";

                // Crea un adaptador de datos
                MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conn);

                // Crea un objeto DataTable para almacenar los datos
                DataTable tablaMascotas = new DataTable();

                // Reemplaza "idDelAdoptante" con el valor del Adoptante específico
                int idDelAdoptante = 1; // Aquí debes colocar el valor correcto

                // Añade el parámetro para el AdoptanteID
                adaptador.SelectCommand.Parameters.AddWithValue("@AdoptanteID", idDelAdoptante);

                // Llena el DataTable con los datos de la tabla
                adaptador.Fill(tablaMascotas);

                // Asigna el DataTable como origen de datos del DataGridView
                dgvMascotasAdicinales.DataSource = tablaMascotas; // Asegúrate de que el nombre del DataGridView sea el correcto

                // Cierra la conexión
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las mascotas: " + ex.Message);
            }
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

        private void ComboBoxNombresAdoptantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxNombresAdoptantes.SelectedIndex;

            if (selectedIndex >= 0)
            {
                try
                {
                    ConexionBD conexion = new ConexionBD();
                    MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

                    conn.Open();

                    // Obtén el nombre seleccionado
                    string nombreSeleccionado = comboBoxNombresAdoptantes.SelectedItem.ToString();

                    // Consulta SQL para obtener el ID del adoptante seleccionado
                    string consultaIdAdoptante = "SELECT AdoptanteID FROM adoptantes WHERE NombreCompleto = @Nombre";

                    MySqlCommand cmdIdAdoptante = new MySqlCommand(consultaIdAdoptante, conn);
                    cmdIdAdoptante.Parameters.AddWithValue("@Nombre", nombreSeleccionado);

                    int adoptanteId = Convert.ToInt32(cmdIdAdoptante.ExecuteScalar());

                    // Consulta SQL para obtener los registros filtrados por el ID del adoptante seleccionado
                    string consultaRegistros = "SELECT MascotaID, AdoptanteID, Especie, Edad FROM mascotas_adoptante_adicionales WHERE AdoptanteID = @AdoptanteID";

                    MySqlCommand cmdRegistros = new MySqlCommand(consultaRegistros, conn);
                    cmdRegistros.Parameters.AddWithValue("@AdoptanteID", adoptanteId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdRegistros);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvMascotasAdicinales.DataSource = dt;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos de la tabla: " + ex.Message);
                }

                btnGuardar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        { // Asegúrate de que un nombre esté seleccionado en el ComboBox

            ConexionBD conexion = new ConexionBD();
 
            if (comboBoxNombresAdoptantes.SelectedIndex >= 0)
            {
                // Obtén el nombre seleccionado
                string nombreSeleccionado = comboBoxNombresAdoptantes.SelectedItem.ToString();

                // Obtén los datos ingresados en los campos de entrada
                string razon = txtRazon.Text;
                string responsable = txtResponsable.Text;
                string espacio = txtEspacio.Text;
                string dormir = txtDormir.Text;
                string tiempoSolo = txtTiempoSolo.Text;

                // Realiza la inserción de los datos en la tabla correspondiente
                using (MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion))
                {
                    conn.Open();

                    // Consulta SQL para insertar los datos en la tabla
                    string consultaInsertarDatos = "INSERT INTO otros_datos_adoptante (RazonesAdoptarMascota, ResponsabilidadGastos, EspacioCasa, DondeDormirMascota, TiempoSolo) VALUES (@razon, @responsable, @espacio, @dormir, @tiempoSolo)";

                    MySqlCommand cmdInsertarDatos = new MySqlCommand(consultaInsertarDatos, conn);
                    cmdInsertarDatos.Parameters.AddWithValue("@razon", razon);
                    cmdInsertarDatos.Parameters.AddWithValue("@responsable", responsable);
                    cmdInsertarDatos.Parameters.AddWithValue("@espacio", espacio);
                    cmdInsertarDatos.Parameters.AddWithValue("@dormir", dormir);
                    cmdInsertarDatos.Parameters.AddWithValue("@tiempoSolo", tiempoSolo);

                    cmdInsertarDatos.ExecuteNonQuery();
                }

                // Luego, puedes mostrar un mensaje al usuario para confirmar que los datos se han guardado con éxito.
                MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpia los campos de entrada para permitir al usuario ingresar nuevos datos.
                LimpiarCamposDeEntrada();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un nombre de adoptante en el ComboBox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
}
        private void btnAgregarMascota_Click(object sender, EventArgs e)
        {
             // Obtén el ID del adoptante seleccionado en el ComboBox
    int adoptanteID = 0;
    if (comboBoxNombresAdoptantes.SelectedIndex >= 0)
    {
        adoptanteID = adoptantesDictionary.FirstOrDefault(x => x.Value == comboBoxNombresAdoptantes.SelectedItem.ToString()).Key;
    }

    // Verifica que se haya seleccionado un adoptante válido
    if (adoptanteID > 0)
    {
        // Obtén los valores de especie y edad ingresados en los campos de entrada (TextBox)
        string especie = txtEspecie.Text;
        int edad = (int)numEdad.Value;

        // Realiza la inserción del nuevo registro en la base de datos
        try
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            // Abre la conexión a la base de datos
            conn.Open();

            // Consulta SQL para insertar el nuevo registro en la tabla
            string consulta = "INSERT INTO mascotas_adoptante_adicionales (AdoptanteID, Especie, Edad) VALUES (@adoptanteID, @especie, @edad)";

            MySqlCommand cmd = new MySqlCommand(consulta, conn);
            cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
            cmd.Parameters.AddWithValue("@especie", especie);
            cmd.Parameters.AddWithValue("@edad", edad);
            cmd.ExecuteNonQuery();

            // Cierra la conexión
            conn.Close();

            // Muestra un mensaje de éxito
            MessageBox.Show("Mascota agregada correctamente");

            // Limpia los campos de entrada (TextBox) para futuras inserciones
            txtEspecie.Text = "";
            // Limpia el valor del NumericUpDown
            numEdad.ResetText();

            // Deshabilita el botón de agregar
            btnGuardar.Enabled = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al agregar la mascota: " + ex.Message);
        }
    }
    else
    {
        MessageBox.Show("Selecciona un adoptante válido");
    }
        }

    private void LimpiarCamposDeEntrada()
    {
    // Limpia los campos de entrada para permitir al usuario ingresar nuevos datos.
    txtRazon.Clear();
    txtResponsable.Clear();
    txtEspacio.Clear();
    txtDormir.Clear();
    txtTiempoSolo.Clear();
    comboBoxNombresAdoptantes.SelectedIndex = -1; // Deselecciona cualquier nombre seleccionado en el ComboBox
    }

        private void BTNsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatosAdicionales_Load(object sender, EventArgs e)
        {

        }

        private void dgvDatosAdicionales_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int adoptanteIDColumnIndex = 0; // Índice de la columna AdoptanteID
            int razonesAdoptarColumnIndex = 2; // Índice de la columna RazonesAdoptarMascota
            int responsabilidadGastosColumnIndex = 3; // Índice de la columna ResponsabilidadGastos
            int espacioCasaColumnIndex = 4; // Índice de la columna EspacioCasa
            int dondeDormirColumnIndex = 5; // Índice de la columna DondeDormirMascota
            int tiempoSoloColumnIndex = 6; // Índice de la columna TiempoSolo

            DataGridViewRow selectedRow = dgvDatosAdicionales.Rows[e.RowIndex];
    DataGridViewColumn selectedColumn = dgvDatosAdicionales.Columns[e.ColumnIndex];

    ConexionBD conexion = new ConexionBD();
    MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

    try
    {
        conn.Open();

        if (e.ColumnIndex == razonesAdoptarColumnIndex)
        {
            string currentValue = selectedRow.Cells[e.ColumnIndex].Value.ToString();
            string newValue = Microsoft.VisualBasic.Interaction.InputBox("Editar RazonesAdoptarMascota", "Ingrese el nuevo valor para RazonesAdoptarMascota", currentValue);

            if (!string.IsNullOrEmpty(newValue))
            {
                selectedRow.Cells[e.ColumnIndex].Value = newValue;

                int adoptanteID;
                if (int.TryParse(selectedRow.Cells[adoptanteIDColumnIndex].Value.ToString(), out adoptanteID))
                {
                    string consulta = "UPDATE otros_datos_adoptante SET RazonesAdoptarMascota = @newValue WHERE AdoptanteID = @adoptanteID";
                    MySqlCommand cmd = new MySqlCommand(consulta, conn);
                    cmd.Parameters.AddWithValue("@newValue", newValue);
                    cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

            else if (e.ColumnIndex == responsabilidadGastosColumnIndex)
            {
                string currentValue = dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para ResponsabilidadGastos", "Editar ResponsabilidadGastos", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    int adoptanteID;
                    if (int.TryParse(selectedRow.Cells[adoptanteIDColumnIndex].Value.ToString(), out adoptanteID))
                    {
                        using (MySqlConnection connection = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE otros_datos_adoptante SET ResponsabilidadGastos = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                    }
                }
            }
            else if (e.ColumnIndex == espacioCasaColumnIndex)
            {
                string currentValue = dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para EspacioCasa", "Editar EspacioCasa", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    int adoptanteID = Convert.ToInt32(selectedRow.Cells[adoptanteIDColumnIndex].Value);

                    using (MySqlConnection connection = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE otros_datos_adoptante SET EspacioCasa = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else if (e.ColumnIndex == dondeDormirColumnIndex)
            {
                string currentValue = dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para DondeDormirMascota", "Editar DondeDormirMascota", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    int adoptanteID = Convert.ToInt32(selectedRow.Cells[adoptanteIDColumnIndex].Value);

                    using (MySqlConnection connection = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE otros_datos_adoptante SET DondeDormirMascota = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
              }

             }

            else if (e.ColumnIndex == tiempoSoloColumnIndex)
            {
                string currentValue = dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                string newValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo valor para TiempoSolo", "Editar TiempoSolo", currentValue);

                if (!string.IsNullOrEmpty(newValue))
                {
                    dgvDatosAdicionales.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;

                    int adoptanteID = Convert.ToInt32(selectedRow.Cells[adoptanteIDColumnIndex].Value);

                    using (MySqlConnection connection = new MySqlConnection(conexion.cadenaDeconexion))
                    {
                        conn.Open();
                        string consulta = "UPDATE otros_datos_adoptante SET TiempoSolo = @newValue WHERE AdoptanteID = @adoptanteID";
                        MySqlCommand cmd = new MySqlCommand(consulta, conn);
                        cmd.Parameters.AddWithValue("@newValue", newValue);
                        cmd.Parameters.AddWithValue("@adoptanteID", adoptanteID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
    }
        catch (Exception ex)
    {
        MessageBox.Show("Error al actualizar los datos adicionales: " + ex.Message);
    }
    finally
    {
        conn.Close();
    }


            }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBoxBuscar.Text.Trim();
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            string consulta = "SELECT AdoptanteID, RazonesAdoptarMascota, ResponsabilidadGastos, EspacioCasa, DondeDormirMascota, TiempoSolo " +
                "FROM adoptantes WHERE ";

            // Verificar las opciones seleccionadas
            List<string> opcionesSeleccionadas = new List<string>();

            // Establecer los campos importantes
            // Obtener el estado de los checkboxes
            bool espacioCasa = checkBoxEspacioCasa.Checked;
            bool razonesAdoptarMascota = checkBoxRazonesAdoptarMascota.Checked;

            // Utilizar el estado de los checkboxes en tu lógica
            if (espacioCasa)
            {
                opcionesSeleccionadas.Add("EspacioCasa LIKE @Criterio");
            }

            if (razonesAdoptarMascota)
            {
                opcionesSeleccionadas.Add("RazonesAdoptarMascota LIKE @Criterio");
            }

            // Construir la parte de la consulta para las opciones seleccionadas
            if (opcionesSeleccionadas.Count > 0)
            {
                consulta += "(" + string.Join(" OR ", opcionesSeleccionadas) + ")";
            }
            else
            {
                MessageBox.Show("Debes seleccionar al menos un criterio de búsqueda.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

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
                    dgvDatosAdicionales.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            CargarDatosEnDataGridView();
        }
}

}          
