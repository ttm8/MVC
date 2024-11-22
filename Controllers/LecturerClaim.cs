using mecca17.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace mecca17.Controllers
{
    public class LecturerClaim : Controller
    {



        public connection conn = new connection();
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
                return RedirectToAction("Login", "LecturerClaim");

            }
            else
            {
                //track error output
                Console.Write(message); //redirect
                return RedirectToAction("Index", "LecturerClaim");

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
                return RedirectToAction("Details", "LecturerClaim");
            }
            else
            {
                Console.WriteLine(message);
                return RedirectToAction("Index", "LecturerClaim");
            }
        }
        [HttpPost]
        public IActionResult claim_sub(IFormFile file, Models.LecturerClaim claims)
        {

            string username = claims.username;
            string email = claims.USER_email;
            string module = claims.module;
            string hour_rate = claims.hour_rate;
            string hours_worked = claims.hours_worked;
            string description = claims.description;
            string documents = claims.supporting_document;
            //string total = claims.Total;
            //check file

            string file_found = "no";
            string filename = "none";
            string filePath = "";
            string folderPath = "";
            if (file != null && file.Length > 0)
            {

                file_found = "yes";
                // Get the file name
                filename = Path.GetFileName(file.FileName);
                // Define the folder path (pdf folder)
                folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdf");
                // Ensure the pdf folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Define the full path where the file will be saved
                filePath = Path.Combine(folderPath, filename);
                // Save the file to the specified path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    Console.WriteLine("File " + filename + " is sccessully uploaded. ");
                    //  under " + claim_id.id);


                }
            }


            string message = claims.LecturerClaims(username, email, module, hour_rate, hours_worked, description, filename, filePath);

            //ViewBag.Message("done");

            //then open connction

            return RedirectToAction("Details", "LecturerClaim");
        }

        //open dashboard
        public IActionResult CreateEdit(mecca17.Models.LecturerClaim model)
        {


            if (ModelState.IsValid)
            {



                // Save the claim to the database
                return RedirectToAction("Details");
            }

            return View();
        }

        // GET: LecturerClaim/Details
        public IActionResult Details()
        {
            // Instantiate the get_claims model to fetch data
            var all = new get_claims();

            //claimsModel.Gets_email();
            // Pass the model to the view
            return View(all);
        }
        // GET: /LecturerClaim/Delete/5
        // GET: /Claims/Delete/5
        // GET: /Claims/Delete/5
        public IActionResult Delete(int id)
        {
            // Create an instance of get_claims to access its properties
            var claims = new get_claims();

            // Find the specific claim to delete
            using (SqlConnection connects = new SqlConnection(conn.Connecting()))
            {
                connects.Open();
                string sql = "SELECT * FROM LecturerClaims WHERE id = @id";
                using (SqlCommand prepare = new SqlCommand(sql, connects))
                {
                    prepare.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = prepare.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the claims object with the record to be deleted
                            claims.Id.Add(reader["id"].ToString());
                            claims.username.Add(reader["username"].ToString());
                            claims.email.Add(reader["email"].ToString());
                            claims.module.Add(reader["module"].ToString());
                            claims.hours_worked.Add(reader["hours_worked"].ToString());
                            claims.rate.Add(reader["rate"].ToString());
                            claims.description.Add(reader["description"].ToString());
                            claims.total.Add(reader["total"].ToString());
                            claims.status.Add(reader["status"].ToString());
                            claims.filename.Add(reader["filename"]?.ToString() ?? "N/A");
                        }
                    }
                }
            }

            // Check if the claim was found
            if (claims.id.Count == 0)
            {
                return NotFound(); // Return 404 if no claim was found
            }

            return View(claims); // Pass the claims object to the view for confirmation
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var claims = new get_claims();
            try
            {
                bool isDeleted = claims.DeleteLecturerClaim(id); // Call the method to delete the claim

                if (isDeleted)
                {
                    TempData["Success"] = "Lecturer claim successfully deleted.";
                }
                else
                {
                    TempData["Error"] = "Lecturer claim not found or could not be deleted.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                TempData["Error"] = "An error occurred while deleting the claim: " + ex.Message;
            }

            return RedirectToAction("Details"); // Redirect to the appropriate action after deletion
        }

    }

}

