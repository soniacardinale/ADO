using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO
{
    public class ConnectedMode
    {
        const string connectionString = @"Persist Security Info=False; Integrated Security=True; Initial Catalog=CinemaDB; Server=WINAPHDFXGCXX6X\SQLEXPRESS";

        public static void Connected()
        {
            //Creare ua connessione
            //Metodo1:
            //SqlConnection connection = new SqlConnection();
            //connection.ConnectionString = connectionString;

            //Metodo2:
            //SqlConnection connection = new SqlConnection(connectionString);

            //Metodo3:
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Aprire una connessione
                connection.Open();

                //Creare un Command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Movies";

                //Creare parametri

                //Eseguire Command
                SqlDataReader reader = command.ExecuteReader();

                //Leggere i dati
                while (reader.Read())
                {
                    Console.WriteLine("{0} - {1} {2} {3}",
                        reader["ID"],
                        reader["Titolo"],
                        reader["Genere"],
                        reader["Durata"]);
                }
                //Chiudere la connessione
                reader.Close(); //chiudiamo il reader
                connection.Close();
            }


        }

        public static void ConnectedWithParameter()
        {
            //Creare una connessione
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Inserimento parametro da riga di comando
                Console.WriteLine("Genere del film: ");
                string Genere;
                Genere = Console.ReadLine();

                //Aprire la connessione
                connection.Open();

                //Creare il Command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Movies WHERE Genere= @Genere";

                //Creare Parametro
                SqlParameter genereParam = new SqlParameter();
                genereParam.ParameterName = "Genere";
                genereParam.Value = Genere;
                command.Parameters.Add(genereParam);

                //equivale a:
                //command.Parameters.AddWithValue("@Genere", Genere);

                //Eseguire Command
                SqlDataReader reader = command.ExecuteReader();

                //Lettura dei dati
                while (reader.Read())
                {
                    Console.WriteLine("{0} - {1} {2}",
                        reader["ID"],
                        reader["Titolo"],
                        reader["Genere"]);
                }

                //Chiudere connessione
                reader.Close(); //chiudiamo il reader
                connection.Close();



            }
        }

        public static void ConnectedStoredProcedure()
        {
            //Creare una connessione
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Aprire una connesione
                connection.Open();

                //Creare Command
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "stpGetActorBycachetRange";

                //Creare Parametri
                command.Parameters.AddWithValue("@min_cachet", 5000);
                command.Parameters.AddWithValue("@max_cachet", 9000);

                //Creare Valori di ritorno
                SqlParameter returnValue = new SqlParameter();
                returnValue.ParameterName = "@returnedCount";
                returnValue.SqlDbType = System.Data.SqlDbType.Int;

                returnValue.Direction = System.Data.ParameterDirection.Output;
                //Direction definisce se ho un valore di input o di output
                command.Parameters.Add(returnValue);


                //Eseguire il command
                SqlDataReader reader = command.ExecuteReader();

                //Visualizzazione dati
                while (reader.Read())
                {
                    Console.WriteLine("{0} - {1} {2}",
                        reader["FirstName"],
                        reader["LastName"],
                        reader["Cachet"]);
                }

                //reader.Close();

                //se mettessi:
                //command.ExecuteNonQuery();  //non vedrei la tabella

                Console.WriteLine("#Actors: {0}", command.Parameters["@returnedCount"].Value);

                //Chiudo la connessione
                connection.Close();
            }
        }

        public static void ConnectedScalar()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand scalarCommand = new SqlCommand();
                scalarCommand.Connection = connection;
                scalarCommand.CommandType = System.Data.CommandType.Text;
                scalarCommand.CommandText = "SELECT COUNT(*) FROM Movies";

               int count = (int) scalarCommand.ExecuteScalar(); //esegue e salva

                Console.WriteLine("Conteggio dei film: {0}", count);

                connection.Close();

               
            }
        }

    }






}
