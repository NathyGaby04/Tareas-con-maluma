// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

Console.WriteLine("Hello, World!");
var conexion = new Conexion();
conexion.Conexion_BD();
conexion.ObtenerMalumitass();
//conexion.InsertarMalumitass();
//conexion.ObtenerMalumitass();
//conexion.EliminarMalumita();
conexion.ActualizarMalumitass();
conexion.ObtenerMalumitass();
public class Maluma ()
{
    private int id = 0;
    private string nombre = "";
    private string descripcion = "";

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}

public class Conexion()
{
    private string key = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=Maluma_DB;uid=sa;pwd=1234;TrustServerCertificate=true;";

    public void Conexion_BD()
    {
        var sql_conexion = new SqlConnection(this.key);
        sql_conexion.Open();
        sql_conexion.Close();
        Console.WriteLine("Conexion exitosa >:v con maaluma uwu");
    }

    public void ObtenerMalumitass()
    {
        var sql_conexion = new SqlConnection(this.key);
        sql_conexion.Open();
        var consulta = "Proc_obtener_malumitas";
        var comando = new SqlCommand(consulta, sql_conexion);
        comando.CommandType = System.Data.CommandType.StoredProcedure;
        var adaptador = new SqlDataAdapter(comando);
        var set_de_datos = new DataSet();
        adaptador.Fill(set_de_datos);
        var lista_malumitas = new List<Maluma>();
        foreach (var elemento in set_de_datos.Tables[0].AsEnumerable())
        {
            lista_malumitas.Add(new Maluma()
            {
                Id = Convert.ToInt32(elemento.ItemArray[0].ToString()),
                Nombre = elemento.ItemArray[1].ToString(),
                Descripcion = elemento.ItemArray[2].ToString(),
            });
        }
        sql_conexion.Close();

        foreach (var malumita in lista_malumitas)
        {
            Console.WriteLine(malumita.Id.ToString() + "|" + malumita.Nombre + "|" + malumita.Descripcion);
        }
    }

    public void InsertarMalumitass()
    {
        var sql_conexion = new SqlConnection(this.key);
        sql_conexion.Open();

        var maluma = new Maluma()
        {
            Id = 0,
            Nombre = "Edgar",
            Descripcion = "Mi papá, mi papá se llama Edga",

        };
        var insertar = "Proc_insertar_malumitas";
        var comando = new SqlCommand(insertar, sql_conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@nombre", maluma.Nombre);
        comando.Parameters.AddWithValue("@descripcion", maluma.Descripcion);
        var resultado = comando.ExecuteNonQuery(); //se debe poner try y catch
        Console.WriteLine($"Filas afectadas: {resultado}");
        sql_conexion.Close();
    }

    public void EliminarMalumita ()
    {
        var sql_conexion = new SqlConnection(this.key);
        sql_conexion.Open();
        int malumaid = 1;
        var eliminar = "Proc_eliminar_malumitas";
        var comando = new SqlCommand(eliminar, sql_conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@MalumaID",malumaid);
        var resultado = comando.ExecuteNonQuery(); //se debe poner try y catch
        Console.WriteLine($"Filas afectadas: {resultado}");
        sql_conexion.Close();
    }

    public void ActualizarMalumitass()
    {

        var sql_conexion = new SqlConnection(this.key);
        sql_conexion.Open();
        int malumaid = 2;
        //string columna = "Descripcion";
        string dato = "ella es la profe de ingles";
        var actualizar = "Proc_actualizar_malumitas";
        var comando = new SqlCommand(actualizar, sql_conexion);
        comando.CommandType = CommandType.StoredProcedure;
        //comando.Parameters.AddWithValue("@Columna", columna);
        comando.Parameters.AddWithValue("@Dato", dato);
        comando.Parameters.AddWithValue("@MalumaID", malumaid);
        var resultado = comando.ExecuteNonQuery(); //se debe poner try y catch
        Console.WriteLine($"Filas afectadas: {resultado}");
        sql_conexion.Close();
    }

}

/*CREATE DATABASE Maluma_DB;
GO

USE Maluma_DB;
GO

CREATE TABLE Maluma(
MalumaID INT PRIMARY KEY NOT NULL IDENTITY (1,1),
Nombre VARCHAR(50) NOT NULL,
Descripcion VARCHAR(120)
);
GO

 USE Maluma_DB
GO

CREATE PROCEDURE Proc_obtener_malumitas
AS BEGIN 
	SELECT MalumaID,Nombre,Descripcion FROM Maluma;
END
GO

CREATE PROCEDURE Proc_insertar_malumitas
@nombre VARCHAR(50),
@descripcion VARCHAR(120)
AS BEGIN
	INSERT INTO Maluma (Nombre,Descripcion)
	VALUES (@nombre,@descripcion);
END
GO

CREATE PROCEDURE Proc_eliminar_malumitas
@MalumaID INT
AS BEGIN
	DELETE FROM Maluma 
	WHERE MalumaID = @MalumaID;
END
GO

ALTER PROCEDURE Proc_actualizar_malumitas
@Dato VARCHAR(120),
@MalumaID INT
AS BEGIN
	UPDATE Maluma
	SET Descripcion = @Dato
	WHERE MalumaID = @MalumaID;
END
GO

 */