using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_rapida_ado.net
{
    public partial class Form1 : Form
    {
        //string conexion = @"Server=.\SQLDEVELOPERCQ;DataBase=Instituto X;User=sa;password=123456";
        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion)) {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = "CREATE TABLE "+txtNombreTabla.Text+"("+
                                    "id INT,"+
                                    "valor1 VARCHAR(20)," +
	                                "valor2 INT" +
                                    "); ";
                cmd.ExecuteNonQuery();
                
                //cmd.CommandText = "DELETE FROM prueba;";
                label1.Text = $"Se creó la tabla {txtNombreTabla.Text}";
            }
            //label1.Text = "Conexión exitosa.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $"DELETE FROM {txtTablaEliminable.Text};";
                cmd.ExecuteNonQuery();

                //cmd.CommandText = "DELETE FROM prueba;";
                label2.Text = $"Se eliminó contenido de la tabla {txtTablaEliminable.Text}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $"INSERT INTO {txtTablaAagregar.Text} VALUES {txtRegistros.Text};";
                int cant = cmd.ExecuteNonQuery();

                //cmd.CommandText = "DELETE FROM prueba;";
                label3.Text = $"Se insertaron {cant} registros en {txtTablaAagregar.Text}";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                string ci = txtPepeCI.Text;
                string nombre = txtPepeNombre.Text;
                string whats = txtPepeWhats.Text;
                cmd.CommandText = $"INSERT INTO Pepes VALUES ({ci}, '{nombre}', {whats});";
                int cant = cmd.ExecuteNonQuery();

                //cmd.CommandText = "DELETE FROM prueba;";
                label7.Text = $"Se insertaron {cant} registros en Pepes";

                cmd.Dispose();
            }
        }

        private void al_entrar_a_textbox(object sender, EventArgs e)
        {
            button1.Visible = true;
        }

        private void al_dejar(object sender, EventArgs e)
        {
            button1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            string insertar = 
                "INSERT INTO Estudiante VALUES (@ci, @nom, @ap, @fecha, @email, @dir);";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(insertar, con)) {

                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add("@ci", SqlDbType.Int);
                        cmd.Parameters["@ci"].Value = int.Parse(textBox1.Text);

                        cmd.Parameters.AddWithValue("@nom", textBox2.Text);
                        cmd.Parameters.AddWithValue("@ap", textBox3.Text);
                        cmd.Parameters.AddWithValue("@fecha", textBox4.Text);
                        cmd.Parameters.AddWithValue("@email", textBox5.Text);
                        cmd.Parameters.AddWithValue("@dir", textBox6.Text);

                        int cant = cmd.ExecuteNonQuery();

                        //cmd.CommandText = "DELETE FROM prueba;";
                        label7.Text = $"Se insertaron {cant} registros en Pepes";
                    }
                    catch (SqlException ex) {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            string sql = "SELECT * FROM Estudiante;";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();

                        //Cuando sabemos que contamos con un solo registro
                        //reader.Read();
                        //richTextBox1.Text = reader["apellido"].ToString();

                        //para n registros -> iteración
                        string aux = "REGISTROS:\n";
                        while (reader.Read())
                        {
                            aux += $"{reader["nombre"].ToString()} {reader["apellido"].ToString()}, {reader["ci"].ToString()}\n";
                        }

                        richTextBox1.Text = aux;
                    }
                    catch (SqlException ex)
                    {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            string sql = "SELECT * FROM Estudiante;SELECT * FROM desarrollo.materia;";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataReader reader = cmd.ExecuteReader();

                        //Cuando sabemos que contamos con un solo registro
                        //reader.Read();
                        //richTextBox1.Text = reader["apellido"].ToString();

                        //para n registros -> iteración
                        string aux = "REGISTROS:\n";
                        while (reader.Read())
                        {
                            aux += $"{reader["nombre"].ToString()} {reader["apellido"].ToString()}, {reader["ci"].ToString()}\n";
                        }

                        reader.NextResult();
                        aux = "REGISTROS 2:\n";
                        while (reader.Read())
                        {
                            aux += $"{reader["nombre"].ToString()}, {reader["sigla"].ToString()}\n";
                        }
                        richTextBox1.Text = aux;
                    }
                    catch (SqlException ex)
                    {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }
    }
}
