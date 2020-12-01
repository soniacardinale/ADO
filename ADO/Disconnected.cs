using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO
{
    public class Disconnected
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog=CinemaDB; Server=WINAPHDFXGCXX6X\SQLEXPRESS";

        public static void DisconnectedMode()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Costruzione Adapter
                SqlDataAdapter adapter = new SqlDataAdapter();

                //Creazione comandi da associare all'Adapter
                SqlCommand selectCommand = new SqlCommand();
                selectCommand.Connection = connection;
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Movies";

                SqlCommand insertCommand = new SqlCommand();
                insertCommand.Connection = connection;
                insertCommand.CommandType = System.Data.CommandType.Text;
                insertCommand.CommandText = "INSERT INTO Movies VALUES (@Titolo, @Genere, @Durata)"; //Non metto l'ID perchè è autoincrementale

                insertCommand.Parameters.Add("@Titolo", System.Data.SqlDbType.NVarChar,255, "Titolo");
                insertCommand.Parameters.Add("@Genere", System.Data.SqlDbType.NVarChar, 255, "Genere");
                insertCommand.Parameters.Add("Durata", System.Data.SqlDbType.Int, 500, "Durata");

                //... posso fare i comandi di UPDATE e DELETE

                //Associare i comandi all'Adapter
                adapter.SelectCommand = selectCommand;
                adapter.InsertCommand = insertCommand;

                DataSet dataset = new DataSet();

                try
                {
                    //scarica la tabella
                    connection.Open();
                    adapter.Fill(dataset, "Movies");

                    //Creazione Record
                    DataRow movie = dataset.Tables["Movies"].NewRow();
                    movie["Titolo"] = "V per Vendetta";
                    movie["Genere"] = "Azione";
                    movie["Durata"] = 125;

                    dataset.Tables["Movies"].Rows.Add(movie);

                    //Update del DB
                    adapter.Update(dataset, "Movies");
                      
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                }
                finally
                {
                    connection.Close();
                }
                    
            }
        }
    }
}
