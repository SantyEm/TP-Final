using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TP_Final.Datos;

namespace TP_Final
{
    public partial class InicioSesion : Form
    {
        private ConexionBD conexionBD; // Declarar la instancia de la clase ConexionBD

        public InicioSesion()
        {
            InitializeComponent();
            conexionBD = new ConexionBD();
            // Configurar la propiedad UseSystemPasswordChar del TextBox
            txtContrasena.UseSystemPasswordChar = true;
            
        }

        private void BTNIngresar_Click(object sender, EventArgs e)
        {

            string rolSeleccionado = (comboBoxRoles.SelectedItem != null) ? comboBoxRoles.SelectedItem.ToString() : string.Empty;// El operador ? verifica si comboBoxRoles.SelectedItem es null
            string nombreUsuario = txtUsuario.Text;
            string contraseña = txtContrasena.Text;

            if (string.IsNullOrEmpty(rolSeleccionado) || string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

     

            if (ValidarCredenciales(rolSeleccionado, nombreUsuario, contraseña))
            {
                // Continuar con el inicio de sesión exitoso
                // Abre el formulario "Interfaz" u otras acciones necesarias
                MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Interfaz formInterfaz = new Interfaz();
                formInterfaz.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Inicio de sesión fallido. Comprueba las credenciales.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCredenciales(string nombreRol, string nombreUsuario, string contraseña)
        {
            using (MySqlConnection conexion = new MySqlConnection(conexionBD.cadenaDeconexion))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT u.UsuarioID FROM usuario u INNER JOIN roles_usuario r ON u.RolID = r.IDRol WHERE u.NombreUsuario = @nombreUsuario AND u.Contraseña = @contraseña AND r.NombreRol = @nombreRol", conexion))
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);
                    cmd.Parameters.AddWithValue("@nombreRol", nombreRol);
                    conexion.Open();

                    int usuarioID = Convert.ToInt32(cmd.ExecuteScalar());

                    return usuarioID > 0;
                }
            }
        }

        private int ObtenerRolID(string nombreRol, string nombreUsuario, string contraseña)
        {
            int rolID = -1; // Valor predeterminado en caso de que no se encuentre el rol

            using (MySqlConnection conexion = new MySqlConnection(conexionBD.cadenaDeconexion))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT u.UsuarioID FROM usuario u INNER JOIN roles_usuario r ON u.RolID = r.IDRol WHERE u.NombreUsuario = @nombreUsuario AND u.Contraseña = @contraseña AND r.NombreRol = @nombreRol", conexion))
                {
                    cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);
                    cmd.Parameters.AddWithValue("@nombreRol", nombreRol); // Usar el parámetro pasado a la función
                    conexion.Open();

                    int usuarioID = Convert.ToInt32(cmd.ExecuteScalar());

                    if (usuarioID > 0)
                    {
                        // Si se encuentra un usuario con el nombre de usuario, contraseña y rol válidos, muestra "Inicio de sesión exitoso"
                        MessageBox.Show("Inicio de sesión exitoso.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Abre el formulario "Interfaz"
                        Interfaz formInterfaz = new Interfaz();
                        formInterfaz.Show();
                        this.Hide(); // Oculta el formulario actual (puedes cerrarlo si lo prefieres)
                    }
                    else
                    {
                        // Si no se encuentra ningún usuario con las credenciales válidas, muestra "Inicio de sesión fallido"
                        MessageBox.Show("Inicio de sesión fallido. Comprueba las credenciales.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                conexion.Close();
            }

            return rolID;
        }

        private void BTNIncio_Click(object sender, EventArgs e)
        {
            //string connectionString = "Server=127.0.0.1;Port=3306;User=root;Password=12345;Database=sys;SslMode=None";
            //using (MySqlConnection connection = new MySqlConnection(connectionString))
           // {
               // try
               // {
                   // connection.Open();
                   // MessageBox.Show("¡Conexión exitosa!", "Verificación de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
               // catch (Exception ex)
                //{
                   // MessageBox.Show("Error de conexión: " + ex.Message, "Verificación de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            //}
            if (MessageBox.Show("¿Desea salir de la aplicación?", "Confirmación de salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicación?", "Confirmación de salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUsuario_Enter_1(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Usuario")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuario_Leave_1(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Usuario";
                txtUsuario.ForeColor = Color.Silver;
            }
        }

        private void txtContrasena_Enter_1(object sender, EventArgs e)
        {
            if (txtContrasena.Text == "Contraseña")
            {
                txtContrasena.Text = "";
                txtContrasena.ForeColor = Color.LightGray;
                txtContrasena.UseSystemPasswordChar = true;

                // Ocultar los caracteres de la contraseña
                txtContrasena.PasswordChar = '*';
            }
        }

        private void txtContrasena_Leave_1(object sender, EventArgs e)
        {
            if (txtContrasena.Text == "")
            {
                txtContrasena.Text = "Contraseña";
                txtContrasena.ForeColor = Color.Silver;
                txtContrasena.UseSystemPasswordChar = false;

                // Mostrar los caracteres de la contraseña
                txtContrasena.PasswordChar = '\0'; // '\0' representa un carácter nulo
            }
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {
            // Iniciar un temporizador para actualizar la hora cada segundo
            Timer temporizador = new Timer();
            temporizador.Interval = 1000; // 1000 milisegundos = 1 segundo
            temporizador.Tick += Temporizador_Tick;
            temporizador.Start();

            // Mostrar la hora actual y el día en la etiqueta al cargar el formulario
            MostrarHoraYDiaActual();
        }

        private void Temporizador_Tick(object sender, EventArgs e)
        {
            // Actualizar la hora actual cada segundo
            MostrarHoraYDiaActual();
        }

        private void MostrarHoraYDiaActual()
        {
            DateTime horaActual = DateTime.Now;
            string hora = horaActual.ToString("HH:mm:ss"); // Formato de hora: horas:minutos:segundos
            string dia = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(horaActual.DayOfWeek);

            lblHoraYDia.Text = "La Hora es " + hora + " - Del dia " + dia;
        }
       
    }
}



