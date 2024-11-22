using Microsoft.Data.SqlClient;

namespace mecca17.Models
{
    public class LecturerClaim
    {
        public string username { get; set; }
        public string module { get; set; }
        public string hour_rate { get; set; }
        public string hours_worked { get; set; }
        public string supporting_document { get; set; }
        public string description { get; set; }

        //public string Total { get; set; }

        public string name { get; set; }

        public string USER_email { get; set; }
        public string role { get; set; }


        //connection
        connection connect = new connection();

        public string LecturerClaims(string username, string email, string module, string rate, string hours_worked, string description, string filename, string filepath)
        {


            //temp variable message
            string message = "";
            string user_ID = get_id();
            string user_EMAIL = get_email();

            string total = "" + (int.Parse(hours_worked) * int.Parse(rate));

            try
            {
                string query = "insert into LecturerClaims values('" + username + "','" + user_EMAIL + "','" + module + "','" + rate + "','" + hours_worked + "','" + description + "','" + total + "','" + filename + "','" + filepath + "','pending');";

                using (SqlConnection connects = new(connect.Connecting()))
                {

                    connects.Open();


                    using (SqlCommand done = new(query, connects))
                    {


                        done.ExecuteNonQuery();

                    }
                    connects.Close();


                }
            }
            catch (IOException error)
            {
                message = error.Message;
            }
            return message;


        }
        public string UpdateClaimStatus(string module, string newStatus)
        {
            // Temp variable message
            string message = "";

            try
            {
                // Prepare the SQL query to update the status
                string query = "UPDATE LecturerClaims SET status = @status WHERE  module = @module;";

                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();

                    // Using SqlCommand with parameters to prevent SQL injection
                    using (SqlCommand done = new SqlCommand(query, connects))
                    {
                        // Add parameters to the command
                        done.Parameters.AddWithValue("@status", newStatus);
                        done.Parameters.AddWithValue("@module", module);

                        // Execute the command
                        int rowsAffected = done.ExecuteNonQuery();

                        // Check if any rows were affected
                        if (rowsAffected > 0)
                        {
                            message = "Status updated successfully.";
                        }
                        else
                        {
                            message = "No claim found with the specified username and module.";
                        }
                    }
                    connects.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                message = "SQL Error: " + sqlEx.Message;
            }
            catch (Exception ex)
            {
                message = "Error: " + ex.Message;
            }

            return message;
        }

        //get id
        public string get_id()
        {
            //hold id varibale
            string hold_id = "";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("select * from active", connects))
                    {
                        using (SqlDataReader getID = prepare.ExecuteReader())

                        {
                            if (getID.HasRows)
                            {

                                //check all, but get one
                                while (getID.Read())
                                {
                                    //then get it
                                    hold_id = getID["id"].ToString();
                                }
                            }

                            getID.Close();


                        }
                    }

                    connects.Close();
                }

            }
            catch (IOException error)
            {
                Console.WriteLine(error.Message);
                hold_id = error.Message;
            }
            return hold_id;
        }

        //get email
        public string get_email()
        {
            //hold id varibale
            string hold_email = "";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();

                    using (SqlCommand prepare = new SqlCommand("select * from active", connects))
                    {
                        using (SqlDataReader getemail = prepare.ExecuteReader())

                        {
                            if (getemail.HasRows)
                            {

                                //check all, but get one
                                while (getemail.Read())
                                {
                                    //then get it
                                    hold_email = getemail["email"].ToString();
                                }
                            }
                            getemail.Close();

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
        public string ApproveLecturerClaim(int id)
        {
            string message = "";
            string query = "UPDATE LecturerClaim SET status = 'approved' WHERE id = @user_id;";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connects))
                    {
                        cmd.Parameters.AddWithValue("@user_id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        message = rowsAffected > 0 ? "Claim approved successfully." : "Claim not found.";
                    }
                    connects.Close();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }

        public string RejectLecturerClaim(int id)
        {
            string message = "";
            string query = "UPDATE LecturerClaim SET status = 'rejected' WHERE id = @user_id;";

            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connects))
                    {
                        cmd.Parameters.AddWithValue("@user_id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        message = rowsAffected > 0 ? "Claim rejected successfully." : "Claim not found.";
                    }
                    connects.Close();
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }
            return message;
        }

        internal string LecturerClaims(string username, string uSER_email, string module, decimal rate, int hoursWorked, string description, string supporting_document)
        {
            throw new NotImplementedException();
        }
    }
}
