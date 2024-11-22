using Microsoft.Data.SqlClient;

namespace mecca17.Models
{
    public class check_login
    {
        public string email { get; set; }

        public string role { get; set; }

        public string password { get; set; }

        //connection string
        connection connect = new connection();

        //method to check the user

        public string login_users(string emails, string roles, string password)
        {
            //temp message
            string message = "";
            Console.WriteLine(email + " and " + password);
            try
            {
                //connect and open
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    //open connection
                    connects.Open();

                    //query
                    string query = "select * from users where email =  '" + emails + "'  and password='" + password + "' ;";


                    //prepare to execute
                    using (SqlCommand prepare = new SqlCommand(query, connects))
                    {

                        //read the data
                        using (SqlDataReader find_user = prepare.ExecuteReader())
                        {

                            //then check if the use is found
                            if (find_user.HasRows)
                            {

                                //then assign message
                                message = "found";
                            }

                            else
                            {
                                message = "not";
                            }
                        }
                    }
                    connects.Close();
                    if (message == "found")
                    {
                        update_active(email);
                    }
                }

            }
            catch (IOException erro_db)
            {
                //return error
                message = erro_db.Message;
            }
            return message;

        }
        //upodate active method
        public void update_active(string email)
        {
            try
            {
                using (SqlConnection connects = new SqlConnection(connect.Connecting()))
                {
                    connects.Open();

                    string query = "update active set email= '" + email + "'";
                    using (SqlCommand done = new SqlCommand(query, connects))
                    {
                        done.ExecuteNonQuery();
                    }

                    connects.Close();

                }

            }
            catch (IOException error)
            {
                Console.WriteLine("error" + error.Message);
            }
        }

    }
}
