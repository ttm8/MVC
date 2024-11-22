using Microsoft.Data.SqlClient;

namespace mecca17.Models
{
    public class register
    {
        //getters and setters for user info collection
        public string username { get; set; }

        public string email { get; set; }

        public string role { get; set; }

        public string password { get; set; }


        //connection string class
        connection connect = new connection();


        public string insert_users(string name, string emails, string roles, string password)
        {

            //temp variable for message
            string message = "";

            //connect to database
            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    //open
                    connects.Open();

                    //query
                    string query = "insert into users values('" + name + "','" + emails + "', '" + password + "' , '" + roles + "');";

                    //execute command 
                    using (SqlCommand add_new_users = new SqlCommand(query, connects))
                    {
                        //then execute it
                        add_new_users.ExecuteNonQuery();

                        //assign the message
                        message = "done";

                    }

                    //then close connection
                    connects.Close();

                }

            }
            catch (IOException error)
            {
                //return the error
                message = error.Message;

            }

            return message;


        }
    }
}


