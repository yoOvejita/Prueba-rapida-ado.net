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
        DataSet telefonoDS = new DataSet();
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

                        reader.NextResult();//Lo podemos poner en WHILE
                        aux += "REGISTROS 2:\n";
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

        private void button9_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            string sql = "SELECT * FROM Telefono;";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataAdapter telefonoDA = new SqlDataAdapter();
                        telefonoDA.SelectCommand = cmd;

                        
                        telefonoDA.Fill(telefonoDS, "Telefono");
                    }
                    catch (SqlException ex)
                    {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
            
        }

        private void leer_dataset(object sender, EventArgs e)
        {
            //acaba de cerrarse la conexión.
            richTextBox2.Text = telefonoDS.DataSetName;
            string cadena = "";
            foreach (DataRow registro in telefonoDS.Tables["Telefono"].Rows)
            {
                cadena += $"{registro[0]}: {registro[1]} - telf.: {registro[2]}\n";
            }
            richTextBox2.Text += "\n" + cadena;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            string sql = "SELECT * FROM Telefono;";
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataAdapter telefonoDA = new SqlDataAdapter();
                        telefonoDA.SelectCommand = cmd;


                        telefonoDA.Fill(telefonoDS, "Telefono");
                    }
                    catch (SqlException ex)
                    {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
                sql = "SELECT * FROM Estudiante;";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        SqlDataAdapter estDA = new SqlDataAdapter();
                        estDA.SelectCommand = cmd;


                        estDA.Fill(telefonoDS, "Estudiante");
                    }
                    catch (SqlException ex)
                    {
                        //Mostrar alert
                        Console.WriteLine(ex.StackTrace);
                    }

                }
            }
        }

        private void cargar_data_set_2(object sender, EventArgs e)
        {
            //acaba de cerrarse la conexión.
            richTextBox3.Text = "";
            string cadena = "";
            foreach (DataRow registro in telefonoDS.Tables["Telefono"].Rows)
            {
                cadena += $"{registro[0]}: {registro[1]} - telf.: {registro[2]}\n";
            }
            cadena += "\n ============= \n";

            foreach (DataRow registro in telefonoDS.Tables["Estudiante"].Rows)
            {
                cadena += $"{registro[0]}: {registro[1]} - {registro[2]}\n";
            }
            richTextBox3.Text += "\n" + cadena;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                //Preparamos un comando listo para UPDATES (sql)
                SqlCommand cmd = new SqlCommand("UPDATE Telefono SET numero = @n WHERE idTelefono = @id;", con);
                //Iniciamos un DataAdapter normal
                SqlDataAdapter telefonoDA = new SqlDataAdapter("SELECT * FROM Telefono;", con);
                //Le agregamos la sentencia sql de update al DataAdapter
                telefonoDA.UpdateCommand = cmd;
                //Agregamos parámetros de update al DataAdapter
                telefonoDA.UpdateCommand.Parameters.Add("@n", SqlDbType.Int,9,"numero");
                SqlParameter param = telefonoDA.UpdateCommand.Parameters.Add("@id", SqlDbType.Int);
                param.SourceColumn = "idTelefono";
                param.SourceVersion = DataRowVersion.Original;

                //Creamos DataSet normalmente y en adelante podemos modificar campos.
                DataSet unDS = new DataSet();
                telefonoDA.Fill(unDS, "Telefono");
                //Rescatamos el registro en particular.
                DataRow telefonoRow = unDS.Tables["Telefono"].Rows[0];
                //Editamos algun campo del registro (row)
                telefonoRow["numero"] = 80022;
                //Actualizamos la base de datos
                telefonoDA.Update(unDS, "Telefono");
             }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = $"CREATE PROCEDURE insertar_estudiante (@ci INT, @nom VARCHAR(50), @ap VARCHAR(50), @fecha DATE) AS INSERT INTO Estudiante (ci, nombre, apellido, fecha_nac) VALUES (@ci, @nom, @ap, @fecha)";
                cmd.ExecuteNonQuery();

                //cmd.CommandText = "DELETE FROM prueba;";
                lblInfo.Text = $"Se creó el procedimiento almacenado";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("mat_cursada",con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                string texto = "";
                while (reader.Read())
                    texto += $"Estudiante: {reader[1]}, Materia: {reader[2]}, nota: {reader[3]}\n";
                richTextBox4.Text = texto;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("mat_cursada_by_ci", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@variable", int.Parse(txt_ci.Text)));
                SqlDataReader reader = cmd.ExecuteReader();

                string texto = "";
                while (reader.Read())
                    texto += $"Estudiante: {reader[1]}, Materia: {reader[2]}, nota: {reader[3]}\n";
                richTextBox4.Text = texto;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("mat_cursada_by_sigla_mincalif", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@sig", txt_sigla.Text));
                cmd.Parameters.Add(new SqlParameter("@nota", float.Parse(txt_nota.Text)));
                SqlDataReader reader = cmd.ExecuteReader();

                string texto = "";
                while (reader.Read())
                    texto += $"Estudiante: {reader[1]}, Materia: {reader[2]}, nota: {reader[3]}\n";
                richTextBox4.Text = texto;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insertar_estudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ci", int.Parse(textBox1.Text)));
                cmd.Parameters.Add(new SqlParameter("@nom", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("@ap", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("@fecha", textBox4.Text));
                int resultado =  cmd.ExecuteNonQuery();
                if(resultado > 0)
                richTextBox4.Text = "Exito";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string conexion = ConfigurationManager.ConnectionStrings["CadenaInstitutoX"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conexion))
            {
                //con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Telefono",conexion);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Telefono");

                //Creando el nuevo registro
                int idTelf = int.Parse(textBox7.Text);
                int codEst = int.Parse(textBox8.Text);
                int num = int.Parse(textBox9.Text);

                DataRow row = ds.Tables["Telefono"].NewRow();//Hemos creado una nueva fila independiente
                row["idTelefono"] = idTelf;
                row["codigoEst"] = codEst;
                row["numero"] = num;
                ds.Tables["Telefono"].Rows.Add(row);

                //Actualizando a la base de datos
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ds.Tables["Telefono"]);


                //extra
                
                foreach(DataTable tabla in ds.Tables)
                {
                    int c = tabla.Columns.Count;
                    foreach (DataRow fila in tabla.Rows)
                    {
                        //Console.WriteLine(fila["numero"].ToString());
                        for(int i = 0; i< c; i++)
                            Console.WriteLine(fila.ItemArray[i].ToString());
                    }
                }
            }
        }
    }
}
