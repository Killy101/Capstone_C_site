using CSiteCapstone.Models;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace CSiteCapstone.Controllers
{
  
    //MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=databasecsite");

    //string connectionString = "server=localhost;user id=root;database=databasecsite "; // Replace with your MySQL connection string
    public class HomeController : Controller
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;database=databasecsite");
        string connectionString = "server=localhost;user id=root;database=databasecsite";

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
           
                                                                                                //private readonly YourDbContext dbContext;
                                                                                                //private string GetUserType(string identifier)
                                                                                                //{
                                                                                                //    // Assuming you have a User table in your database with a UserType column
                                                                                                //    using (var dbContext = new YourDbContext()) // Replace MyDbContext with your actual DbContext class
                                                                                                //    {
                                                                                                //        var user = dbContext.Users.FirstOrDefault(u => u.Username == identifier || u.Id == identifier);
                                                                                                //        if (user != null)
                                                                                                //        {
                                                                                                //            return user.UserType;
                                                                                                //        }
                                                                                                //    }

            //    // Return a default UserType if the user is not found or the UserType is not available
            //    return "unknown";
            //}

            // GET: Home
            //public ActionResult Index()
            //{
            //MySqlConnector.MySqlDataAdapter adapter = new MySqlConnector.MySqlDataAdapter("SELECT * FROM `register` WHERE 1",connectionString);

            //    DataTable dt = new DataTable();
            //    adapter.Fill(dt);

            //    return View(dt);
            //}

            public ActionResult HomePage()
            {
                return View();
            }



            //Create Account Sample
            public ActionResult Register()
            {
                return View();

            }
            // this is wheere the user create account first before login
            [HttpPost]
            public ActionResult Register(User models)
            {

                using (var connection = new MySqlConnection(connectionString))
                {
                string usertype = "Owner";
                string query = "INSERT INTO `users`(`firstName`, `lastName`, `phone`, `email`, `username`, `password`, `usertype`) " +
                "VALUES (@firstname,@lastname,@phone,@email,@username,@password','usertype')";
                    var command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@firstname", models.fName);
                    command.Parameters.AddWithValue("@lastname", models.lName);
                    command.Parameters.AddWithValue("@phone", models.phone);
                    command.Parameters.AddWithValue("@email", models.email);
                    command.Parameters.AddWithValue("@username", models.username);
                    command.Parameters.AddWithValue("@password", models.password);
                    command.Parameters.AddWithValue("@usertype", usertype);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

            return RedirectToAction("Login");
        }

        //public ActionResult CampInfo()
        //{
        //    return View();
        //}
        //[HttpPost]
        ////Campsite Owner Registration
        //public ActionResult CampInfo(ImageFile objImage)
        //{
        //    foreach (var file in objImage.files)
        //    {
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            file.SaveAs(Path.Combine(Server.MapPath("/Uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
        //        }
        //    }
        //    using (var connection = new MySqlConnection(connectionString))
        //    {
                
        //        string query = "INSERT INTO `campinfo`(`CampName`, `CAmp_Location`,`Camp_Photo`, `Camp_Desc`) " +
        //            "VALUES (@name, @location, @img,@desc)";
        //        var command = new MySqlCommand(query, connection);

        //        command.Parameters.AddWithValue("@name", objImage.name);
        //        command.Parameters.AddWithValue("@location", objImage.location);
        //        command.Parameters.AddWithValue("@img", objImage.files);
        //        command.Parameters.AddWithValue("@desc", objImage.desc);
                

        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }

        //        return RedirectToAction("Login");
        //    }
            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public ActionResult Login(Login models)
            {
                if (ModelState.IsValid)
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        var query = "SELECT password FROM users WHERE username = @Username";
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", models.username);
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var storedPassword = reader.GetString("Password");
                                    if (VerifyPassword(models.password, storedPassword))
                                    {
                                        // Successful login
                                        //string userType = GetUserType(username);
                                        //Session["email"] = userType;

                                        // You can create a session or authentication token here to maintain the user's login state
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                            }
                        }
                    }
                }


                // Invalid login
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(models);
            }
            //public ActionResult SomeAction()
            //{
            //    // Check if the user is logged in
            //    if (Session["username"] != null)
            //    {
            //        // User is logged in
            //        // Retrieve the user's UserType from the session
            //        string userType = Session["username"] as string;

            //        // Use the user's UserType to display appropriate information
            //        if (userType == "admin")
            //        {
            //            // Display information or perform actions specific to the admin user type
            //            ViewBag.Message = "Welcome, Admin!";
            //        }
            //        else if (userType == "owener")
            //        {
            //            // Display information or perform actions specific to the client user type
            //            ViewBag.Message = "Welcome, Client!";
            //        }

            //        return View();
            //    }
            //    else
            //    {
            //        // User is not logged in, redirect to the login page
            //        return RedirectToAction("Login", "Account");
            //    }
            //}



            private bool VerifyPassword(string enteredPassword, string storedPassword)
            {
                // Implement your password verification logic (e.g., using bcrypt or other secure hashing algorithm)
                // Return true if the entered password matches the stored password
                return enteredPassword == storedPassword;
                // You can use BCrypt.Net.BCrypt.Verify() or any other library for password verification

            }




            public ActionResult LandingPage()
            {
                return View();
            }
            public ActionResult LogOut()
            {
                return View();
            }


            public ActionResult OwnnerdashBoard()
            {
                return View();
            }

            public ActionResult Ownnermanage()
            {
                return View();
            }
            public ActionResult Ownnerchat()
            {
                return View();
            }
            public ActionResult Ownnerreservation()
            {
                return View();
            }
            public ActionResult OwnnerInfo()
            {
                return View();
            }
            public ActionResult Ownnereports()
            {
                return View();
            }






            //admin
            public ActionResult AdmindashBoard()
            {
                return View();
            }
            public ActionResult AdminManage()
            {
                return View();
            }
            public ActionResult AdminPost()
            {
                return View();
            }
            public ActionResult AdminTrackUser()
            {
                return View();
            }
            public ActionResult AdminReports()
            {
                return View();
            }
            public ActionResult AdminInfo()
            {
                return View();
            }
            public ActionResult AdminNotificatiions()
            {
                return View();
            }

        }
    }

