using System;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Data
{
  public class MyFiles : IMyFiles
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;

    public MyFiles(IOptions<Secrets.ConnectionStrings> ConnectionStrings)
    {
        _connStr = ConnectionStrings;
    }

    public Inserted Insert(Form data)
    {
      int id = 0;
      string guid = string.Empty;
      
      using (SqlConnection Con = new SqlConnection(_connStr.Value.SqlServer))
      {
        using (SqlCommand Cmd = new SqlCommand())
        {
          Cmd.CommandType = CommandType.StoredProcedure;
          Cmd.Connection = Con;
          Cmd.CommandText = "[sp_FILE_ADD]";

          Cmd.Parameters.AddWithValue("@NAME", data.Name);
          Cmd.Parameters.AddWithValue("@EMAIL", data.Email);
          Cmd.Parameters.AddWithValue("@FAIL", data.Fail);
          Cmd.Parameters.AddWithValue("@FILENAME", data.FileName);

          Con.Open();

          using (SqlDataReader MyDR = Cmd.ExecuteReader())
          {
            if (MyDR.HasRows)
            {
              MyDR.Read();

              id = MyDR.GetInt32(0);
              guid = MyDR.GetGuid(1).ToString();
            }
          }
        }
      }

      return new Inserted(id, guid);
    }

    public Inserted GetFileInfo(int id)
    {
      string guid = string.Empty;
      string name = string.Empty;
      string email = string.Empty;
      bool fail = false;

      using (SqlConnection Con = new SqlConnection(_connStr.Value.SqlServer))
      {
        using (SqlCommand Cmd = new SqlCommand())
        {
          Cmd.CommandType = CommandType.StoredProcedure;
          Cmd.Connection = Con;
          Cmd.CommandText = "[sp_FILE_INFO]";

          Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));

          Con.Open();

          using (SqlDataReader MyDR = Cmd.ExecuteReader())
          {
            MyDR.Read();

            guid = MyDR.GetGuid(0).ToString();
            name = MyDR.GetString(1);
            email = MyDR.GetString(2);
            fail = MyDR.GetBoolean(3);
          }
        }
      }

      return new Inserted(id, guid, name, email, fail);
    }
  }
}
