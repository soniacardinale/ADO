using System;

namespace ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectedMode.Connected();
            ////ConnectedMode.ConnectedWithParameter();
            ////ConnectedMode.ConnectedStoredProcedure();
            //ConnectedMode.ConnectedScalar();

            Disconnected.DisconnectedMode();
            ConnectedMode.Connected();
        }
    }
}
