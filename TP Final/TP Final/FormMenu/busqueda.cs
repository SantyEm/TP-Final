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
    public partial class busqueda : Form
    {
        public busqueda()
        {
            InitializeComponent();
            personalizarDiseño();
            CargarDatosGatos();
            CargarDatosPerros();
            // Ajustar el tamaño de la fuente de las pestañas
            Font newFont = new Font(tabControl1.Font.FontFamily, 12, FontStyle.Regular);
            tabControl1.Font = newFont;

            // Ajustar el tamaño de los encabezados de las pestañas
            tabControl1.ItemSize = new Size(120, 40);

            tabControl1.SelectedIndex = 0; // Establece la pestaña activa
            tabControl1.SelectedTab.BorderStyle = BorderStyle.FixedSingle;
        }

       
        #region funciones formulario
   

        private void personalizarDiseño()
        {
            panelGatos.Visible = false;
            panelPerros.Visible = false;
        }


        private void hideSubMenu()
        {
            if (panelGatos.Visible == true)
            {
                panelGatos.Visible = false;
            }
            if (panelPerros.Visible == true)
            {
                panelPerros.Visible = true;
            }
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;

        }

        private void btnGatos_Click(object sender, EventArgs e)
        {
            showSubMenu(panelGatos);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPerros);
            hideSubMenu();

        }

        #endregion

        #region Funciones de gatos
        private void CargarDatosGatos()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();
                string consulta = "SELECT g.IDGatito, g.Nombre, g.Edad, g.Color, s.Sexo, g.Descripcion, e.Estado, g.Raza " +
                    "FROM gatitos_disponibles g " +
                    "INNER JOIN sexo_mascotas s ON g.IDSexo = s.IDSexo " +
                    "INNER JOIN estado_mascotas e ON g.IDEstado = e.IDEstado";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Renombrar las columnas para mostrar los nombres en lugar de los IDs
                table.Columns["Sexo"].ColumnName = "Sexo";
                table.Columns["Estado"].ColumnName = "Estado";

                // Enlaza los datos al DataGridView
                dgvGatos.DataSource = table;
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

        private void BTNSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardarD_Click(object sender, EventArgs e)
        {
           
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Obtener los valores de los campos de entrada
                string nombre = txtNombre.Text;
                int edad = (int)numericUpDownEdad.Value;
                string color = txtColor.Text;
                string descripcion = txtDescripcion.Text;
                string raza = txtRaza.Text;

                // Obtener el valor del sexo seleccionado
                string idSexo = string.Empty;

                if (comboBoxSexo.SelectedIndex == 0)
                {
                    idSexo = "1";
                }
                else if (comboBoxSexo.SelectedIndex == 1)
                {
                    idSexo = "2";
                }
                else if (comboBoxSexo.SelectedIndex == 2)
                {
                    idSexo = "3";
                }

                // Obtener el valor del estado seleccionado
                string idEstado = string.Empty;

                if (comboBoxEstado.SelectedIndex == 0)
                {
                    idEstado = "1";
                }
                else if (comboBoxEstado.SelectedIndex == 1)
                {
                    idEstado = "2";
                }
                else if (comboBoxEstado.SelectedIndex == 2)
                {
                    idEstado = "3";
                }
                else if (comboBoxEstado.SelectedIndex == 3)
                {
                    idEstado = "4";
                }
                else if (comboBoxEstado.SelectedIndex == 4)
                {
                    idEstado = "5";
                }
                else if (comboBoxEstado.SelectedIndex == 5)
                {
                    idEstado = "6";
                }

                // Verificar campos obligatorios vacíos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(raza))
                {
                    MessageBox.Show("Los campos Nombre, Color, Descripción y Raza son obligatorios. Por favor, asegúrate de completarlos antes de guardar.", "Campos obligatorios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del evento sin realizar la inserción
                }

                // Consulta de inserción
                string consulta = "INSERT INTO gatitos_disponibles (Nombre, Edad, Color, IDSexo, Descripcion, IDEstado, Raza) " +
                    "VALUES (@Nombre, @Edad, @Color, @IDSexo, @Descripcion, @IDEstado, @Raza)";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);

                // Parámetros de la consulta
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Edad", edad);
                cmd.Parameters.AddWithValue("@Color", color);
                cmd.Parameters.AddWithValue("@IDSexo", idSexo);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@IDEstado", idEstado);
                cmd.Parameters.AddWithValue("@Raza", raza);

                // Ejecutar la consulta
                cmd.ExecuteNonQuery();

                MessageBox.Show("Los datos se han guardado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
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

            // Construir la consulta SQL dinámicamente basada en el criterio de búsqueda
            string consulta = "SELECT g.IDGatito, g.Nombre, g.Edad, g.Color, s.Sexo, g.Descripcion, e.Estado, g.Raza " +
                "FROM gatitos_disponibles g " +
                "INNER JOIN sexo_mascotas s ON g.IDSexo = s.IDSexo " +
                "INNER JOIN estado_mascotas e ON g.IDEstado = e.IDEstado " +
                "WHERE ";

            // Verificar las opciones seleccionadas
            List<string> opcionesSeleccionadas = new List<string>();

            // Establecer los campos importantes
            bool campoEstado = checkBoxEstado.Checked;
            bool campoColor = checkBoxColor.Checked;
            bool campoRaza = checkBoxRaza.Checked;
            bool campoEdad = checkBoxEdad.Checked;

            if (campoEstado)
            {
                opcionesSeleccionadas.Add("e.Estado LIKE @Criterio");
            }

            if (campoColor)
            {
                opcionesSeleccionadas.Add("g.Color LIKE @Criterio");
            }

            if (campoRaza)
            {
                opcionesSeleccionadas.Add("g.Raza LIKE @Criterio");
            }

            if (campoEdad)
            {
                opcionesSeleccionadas.Add("g.Edad = @Edad");
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

            // Agregar parámetro para la búsqueda por edad
            int edad;
            if (campoEdad && int.TryParse(criterioBusqueda, out edad))
            {
                cmd.Parameters.AddWithValue("@Edad", edad);
            }

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
                    dgvGatos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
    }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
        CargarDatosGatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);
            // Obtener el ID del registro seleccionado en el DataGridView
            if (dgvGatos.SelectedRows.Count > 0)
            {
                int idGatito = Convert.ToInt32(dgvGatos.SelectedRows[0].Cells["IDGatito"].Value);

                // Eliminar los registros relacionados en la tabla "adopciones_gatos"
                string consultaEliminarAdopciones = "DELETE FROM adopciones_gatos WHERE IDGatito = @IDGatito";
                MySqlCommand cmdEliminarAdopciones = new MySqlCommand(consultaEliminarAdopciones, conn);
                cmdEliminarAdopciones.Parameters.AddWithValue("@IDGatito", idGatito);
                conn.Open();
                cmdEliminarAdopciones.ExecuteNonQuery();

                // Construir la consulta SQL para eliminar el registro en la tabla "gatitos_disponibles"
                string consultaEliminarGatito = "DELETE FROM gatitos_disponibles WHERE IDGatito = @IDGatito";
                MySqlCommand cmdEliminarGatito = new MySqlCommand(consultaEliminarGatito, conn);
                cmdEliminarGatito.Parameters.AddWithValue("@IDGatito", idGatito);
                int filasAfectadas = cmdEliminarGatito.ExecuteNonQuery();
                conn.Close();

                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Aquí puedes realizar alguna acción adicional después de eliminar el registro
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

        #endregion

        #region Funciones de perros
        private void CargarDatosPerros()
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();
                string consulta = "SELECT p.IDPerro, p.Nombre, p.Edad, p.Color, s.Sexo, p.Descripcion, e.Estado, p.Raza " +
                    "FROM perros_disponibles p " +
                    "INNER JOIN sexo_mascotas s ON p.IDSexo = s.IDSexo " +
                    "INNER JOIN estado_mascotas e ON p.IDEstado = e.IDEstado";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Renombrar las columnas para mostrar los nombres en lugar de los IDs
                table.Columns["Sexo"].ColumnName = "Sexo";
                table.Columns["Estado"].ColumnName = "Estado";

                // Enlaza los datos al DataGridView
                dgvPerros.DataSource = table;
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
        private void btnNuevoP_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            try
            {
                conn.Open();

                // Obtener los valores de los campos de entrada
                string nombre = txtNombreP.Text;
                int edad = (int)numericUpDownEdadP.Value;
                string color = txtColorP.Text;
                string descripcion = txtDescripcionP.Text;
                string raza = txtRazaP.Text;

                // Obtener el valor del sexo seleccionado
                int idSexo = comboBoxSexoP.SelectedIndex + 1;

                // Obtener el valor del estado seleccionado
                int idEstado = comboBoxEstadoP.SelectedIndex + 1;

                // Verificar campos obligatorios vacíos
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(color) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(raza))
                {
                    MessageBox.Show("Los campos Nombre, Color, Descripción y Raza son obligatorios. Por favor, asegúrate de completarlos antes de guardar.", "Campos obligatorios vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del evento sin realizar la inserción
                }

                // Consulta de inserción
                string consulta = "INSERT INTO perros_disponibles (Nombre, Edad, Color, IDSexo, Descripcion, IDEstado, Raza) " +
                    "VALUES (@Nombre, @Edad, @Color, @IDSexo, @Descripcion, @IDEstado, @Raza)";

                MySqlCommand cmd = new MySqlCommand(consulta, conn);

                // Parámetros de la consulta
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Edad", edad);
                cmd.Parameters.AddWithValue("@Color", color);
                cmd.Parameters.AddWithValue("@IDSexo", idSexo);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                cmd.Parameters.AddWithValue("@IDEstado", idEstado);
                cmd.Parameters.AddWithValue("@Raza", raza);

                // Ejecutar la consulta
                cmd.ExecuteNonQuery();

                MessageBox.Show("Los datos se han guardado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnRestaurarP_Click(object sender, EventArgs e)
        {
            CargarDatosPerros();
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            // Obtener el ID del perro seleccionado en el DataGridView
            if (dgvPerros.SelectedRows.Count > 0)
            {
                int idPerro = Convert.ToInt32(dgvPerros.SelectedRows[0].Cells["IDPerro"].Value);

                // Eliminar los registros relacionados en la tabla "adopciones_perros"
                string consultaEliminarAdopciones = "DELETE FROM adopciones_perros WHERE IDPerro = @IDPerro";
                MySqlCommand cmdEliminarAdopciones = new MySqlCommand(consultaEliminarAdopciones, conn);
                cmdEliminarAdopciones.Parameters.AddWithValue("@IDPerro", idPerro);

                // Construir la consulta SQL para eliminar el perro en la tabla "perros_disponibles"
                string consultaEliminarPerro = "DELETE FROM perros_disponibles WHERE IDPerro = @IDPerro";
                MySqlCommand cmdEliminarPerro = new MySqlCommand(consultaEliminarPerro, conn);
                cmdEliminarPerro.Parameters.AddWithValue("@IDPerro", idPerro);

                try
                {
                    conn.Open();

                    // Eliminar los registros relacionados en la tabla "adopciones_perros"
                    cmdEliminarAdopciones.ExecuteNonQuery();

                    // Eliminar el perro en la tabla "perros_disponibles"
                    int filasAfectadas = cmdEliminarPerro.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("El perro se ha eliminado correctamente.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Aquí puedes realizar alguna acción adicional después de eliminar el perro

                        // Actualizar el DataGridView después de eliminar el perro
                        // ...
                        // Código para actualizar el DataGridView con los perros restantes
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el perro.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el perro: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un perro para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarP_Click(object sender, EventArgs e)
        {
            string criterioBusqueda = txtBoxBuscarP.Text.Trim();
            ConexionBD conexion = new ConexionBD();
            MySqlConnection conn = new MySqlConnection(conexion.cadenaDeconexion);

            // Construir la consulta SQL dinámicamente basada en el criterio de búsqueda
            string consulta = "SELECT p.IDPerro, p.Nombre, p.Edad, p.Color, s.Sexo, p.Descripcion, e.Estado, p.Raza " +
                "FROM perros_disponibles p " +
                "INNER JOIN sexo_mascotas s ON p.IDSexo = s.IDSexo " +
                "INNER JOIN estado_mascotas e ON p.IDEstado = e.IDEstado " +
                "WHERE ";

            // Verificar las opciones seleccionadas
            List<string> opcionesSeleccionadas = new List<string>();

            // Establecer los campos importantes
            bool campoEstado = checkBoxEstado.Checked;
            bool campoColor = checkBoxColor.Checked;
            bool campoRaza = checkBoxRaza.Checked;
            bool campoEdad = checkBoxEdad.Checked;

            if (campoEstado)
            {
                opcionesSeleccionadas.Add("e.Estado LIKE @Criterio");
            }

            if (campoColor)
            {
                opcionesSeleccionadas.Add("p.Color LIKE @Criterio");
            }

            if (campoRaza)
            {
                opcionesSeleccionadas.Add("p.Raza LIKE @Criterio");
            }

            if (campoEdad)
            {
                opcionesSeleccionadas.Add("p.Edad = @Edad");
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

            // Agregar parámetro para la búsqueda por edad
            int edad;
            if (campoEdad && int.TryParse(criterioBusqueda, out edad))
            {
                cmd.Parameters.AddWithValue("@Edad", edad);
            }

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
                    dgvPerros.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion
 
    }
}
