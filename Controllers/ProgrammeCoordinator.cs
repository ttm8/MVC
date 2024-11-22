using mecca17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace mecca17.Controllers
{
    public class ProgrammeCoordinator : Controller
    {


        //create a connection object to the database
        public connection conn = new connection();
        private readonly Models.LecturerClaim _claimsModel;

        public ProgrammeCoordinator()
        {
            // Initialize your model here or inject via dependency injection
            _claimsModel = new Models.LecturerClaim();
        }
        public IActionResult Index()
        {
            //check the connection
            try
            {
                //get the connection string from the connection class
                connection conn = new connection();
                //then check
                using (SqlConnection connect = new SqlConnection(conn.Connecting()))
                {
                    //open the connection
                    connect.Open();
                    Console.WriteLine("connected");
                    connect.Close();
                }
            }
            catch (IOException error)
            {
                //error message
                Console.WriteLine("Error : " + error.Message);
            }
            return View();
        }

        //http post for the register
        //from the register form
        [HttpPost]

        public IActionResult Register_users(register add_users)
        {

            //collect user's value
            string name = add_users.username;
            string email = add_users.email;
            string password = add_users.password;
            string role = add_users.role;


            //check idf all are collected
            //Console.WriteLine("Name : " + name + "\nEmail: " + email + "Role: " + role);


            //pass all the values to insert method
            string message = add_users.insert_users(name, email, password, role);

            //then check if the user is inserted
            if (message == "done")

            {
                //track error output
                Console.Write(message);
                //direct
                return RedirectToAction("Login", "ProgrammeCoordinator");

            }
            else
            {
                //track error output
                Console.Write(message); //redirect
                return RedirectToAction("Index", "ProgrammeCoordinator");

            }


        }


        //for login page




        public IActionResult Login()
        {
            return View();
        }

        //login page
        [HttpPost]
        public IActionResult login_users(check_login users)
        {


            //then assign
            string email = users.email;
            string role = users.role;
            string password = users.password;

            string message = users.login_users(email, role, password);

            if (message == "found")
            {
                Console.WriteLine(message);
                return RedirectToAction("Claims", "ProgrammeCoordinator");
            }
            else
            {
                Console.WriteLine(message);
                return RedirectToAction("Index", "ProgrammeCoordinator");
            }
        }


        //claim page
        public IActionResult Claims()
        {
            // Instantiate the get_claims model to fetch data
            // Instantiate the get_claims model to fetch data
            var all = new get_claims();

            //claimsModel.Gets_email();
            // Pass the model to the view
            return View(all);
        }


        public ActionResult Review()
        {
            var claim = new get_claims(); // Assume it's populated with real data
            return View(claim); // Pass a single object
        }
        [HttpPost]
        public IActionResult Approves([FromBody] loads_all all)
        {
            return UpdateClaimStatus(all, "approved ");
        }

        [HttpPost]
        public IActionResult Reject([FromBody] loads_all all)
        {
            return UpdateClaimStatus(all, "rejected");
        }

        private IActionResult UpdateClaimStatus(loads_all all, string status)
        {
            try
            {
                using (SqlConnection connects = new SqlConnection(conn.Connecting()))
                {
                    connects.Open();
                    string id = all.Ids.Replace(")", "");

                    if (int.TryParse(id, out int claimId))
                    {
                        string update = "UPDATE LecturerClaims SET status = @status WHERE id = @id";
                        using (SqlCommand updates = new SqlCommand(update, connects))
                        {
                            updates.Parameters.AddWithValue("@status", status);
                            updates.Parameters.AddWithValue("@id", claimId);
                            updates.ExecuteNonQuery();
                            return Json(new { success = true });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, message = "Invalid claim ID format" });
                    }
                }
            }
            catch (Exception e)
            {
                // Handle exception (consider logging)
                return Json(new { success = false, message = "An error occurred: " + e.Message });
            }
        }
    }
}








