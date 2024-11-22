using Microsoft.Data.SqlClient;
using System.Collections;

namespace mecca17.Models
{
    public class get_claims
    {
        public ArrayList user_id { get; set; } = new ArrayList();
        public ArrayList username { get; set; } = new ArrayList();
        public ArrayList email { get; set; } = new ArrayList();

        public ArrayList module { get; set; } = new ArrayList();

        public ArrayList id { get; set; } = new ArrayList();
        public List<string> Id { get; set; } = new List<string>();
        public ArrayList hours_worked { get; set; } = new ArrayList();

        public ArrayList rate { get; set; } = new ArrayList();

        public ArrayList description { get; set; } = new ArrayList();

        public ArrayList total { get; set; } = new ArrayList();

        public ArrayList status { get; set; } = new ArrayList();
        public ArrayList isApproved { get; set; } = new ArrayList();
        public ArrayList filename { get; set; } = new ArrayList();
        public bool? IsApproved { get; set; }
        public string ids { get; set; }
        public string on { get; set; }

        //public string FeedBack { get; set; }

        public ArrayList FeedBack { get; set; } = new ArrayList();

        connection connect = new connection();


        //constructor
        public get_claims()
        {
            string connectionString = "Data Source = (localdb)\\mecca17; Initial Catalog = OUTIS89; Integrated Security = True; Trust Server Certificate = True";
            string emails = Gets_email();


            try
            {

                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("select * from active", connects))
                    {
                        using (SqlDataReader getmail = prepare.ExecuteReader())

                        {
                            if (getmail.HasRows)
                            {

                                //check all, but get one
                                while (getmail.Read())
                                {
                                    //then get it
                                    // hold_email = getmail["email"].ToString();

                                }
                            }
                            getmail.Close();

                        }
                    }

                    connects.Close();
                }

            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);

            }
        }

        //get email
        public string Gets_email()
        {
            string hold_email = "";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();
                    string sql = "SELECT * FROM LecturerClaims ";
                    using (SqlCommand prepare = new SqlCommand(sql, connects))
                    {
                        using (SqlDataReader reader = prepare.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hold_email = reader["email"].ToString();
                                id.Add(reader["id"]?.ToString() ?? "");
                                username.Add(reader["username"].ToString());
                                email.Add(reader["email"]?.ToString() ?? "");
                                module.Add(reader["module"]?.ToString() ?? "");
                                hours_worked.Add(reader["hours_worked"]?.ToString() ?? "");
                                rate.Add(reader["rate"]?.ToString() ?? "");
                                description.Add(reader["description"]?.ToString() ?? "");
                                total.Add(reader["total"]?.ToString() ?? "");
                                status.Add(reader["status"]?.ToString() ?? "");

                                // Check if 'filename' column exists
                                if (reader.GetSchemaTable().Columns.Contains("filename"))
                                {
                                    filename.Add(reader["filename"].ToString());
                                }
                                else
                                {
                                    filename.Add("N/A"); // or handle accordingly
                                }
                            }
                        }
                    }

                    connects.Close();
                }
            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                hold_email = error.Message;
            }
            return hold_email;
        }
        public bool DeleteLecturerClaim(int id)
        {
            // SQL query to delete the claims from the database
            using (SqlConnection con = new SqlConnection(connect.Connecting()))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM LecturerClaims WHERE id = @id", con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Return true if a row was deleted
                }
            }
        }


        public void CreateRecord()
        {



            using (SqlConnection connects = new SqlConnection(connect.Connecting()))
            {
                connects.Open();

                int recordCount = Math.Min(status.Count, FeedBack.Count);
                for (int i = 0; i < recordCount; i++)
                {
                    // Assuming both ArrayLists have the same count
                    string currentStatus = status[i] as string;
                    string currentFeedback = FeedBack[i] as string;


                    if (currentStatus != null && currentFeedback != null) // Check for non-null values
                    {
                        var command = new SqlCommand("INSERT INTO LecturerClaims (email,status, FeedBack) VALUES (@email,@status, @FeedBack)", connects);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@status", currentStatus);
                        command.Parameters.AddWithValue("@FeedBack", currentFeedback);


                        command.ExecuteNonQuery();
                    }
                }
                connects.Close();

            }
        }
    }
}
