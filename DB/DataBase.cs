using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KursRadio.DB
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedWorker { get; set; }
        public string Equipment { get; set; }
    }
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public Area()
        {
        }

        public Area(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Profile()
        {
        }

        public Profile(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public class Picket
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double RadiationLevel { get; set; }
    }

    public class ProfileCoordinate
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class DataBase
    {
        private readonly string connectionString = "Data Source=DBSRV\\AG2023;Initial Catalog=107A1_MelnikSR;Integrated Security=True";
        public List<User> Users { get; private set; }
        public List<Project> Projects { get; private set; }

        public DataBase()
        {
            Users = new List<User>();
            Projects = new List<Project>();
        }
        public bool ValidateUser(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM Пользователь WHERE Логин = @Username AND Пароль = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        public void LoadData()
        {
            Users = GetUsers();
            Projects = GetProjects();
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            string query = "SELECT * FROM Пользователь";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.Username = reader["Логин"].ToString();
                    user.Password = reader["Пароль"].ToString();
                    users.Add(user);
                }
                reader.Close();
            }
            return users;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();

            string query = "SELECT * FROM Проект";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Project project = new Project();
                    project.Id = Convert.ToInt32(reader["Id"]);
                    project.Name = reader["Название"].ToString();
                    project.Description = reader["Описание"].ToString();
                    project.AssignedWorker = reader["Работник"].ToString();
                    project.Equipment = reader["Оборудование"].ToString();
                    projects.Add(project);
                }
                
                reader.Close();
            }
            return projects;
        }

        
        public List<Area> GetAreas(int projectId)
        {
            List<Area> areas = new List<Area>();
            


            string query = "SELECT * FROM Площади WHERE ПроектId = @ProjectId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProjectId", projectId);
                connection.Open();
                SqlDataReader reader2 = command.ExecuteReader();

                while (reader2.Read())
                {
                    
                    Area area = new Area();
                    area.Id = Convert.ToInt32(reader2["Id"]);
                    
                    area.Name = reader2["Название"].ToString();
                    
                    area.Description = reader2["Описание"].ToString();
                    areas.Add(area);
                }
                reader2.Close();
            }
            return areas;
        }

        public List<Profile> GetProfiles(int areaId)
        {
            List<Profile> profiles = new List<Profile>();

            string query = "SELECT * FROM Профили WHERE ПлощадьId = @AreaId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AreaId", areaId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Profile profile = new Profile();
                    profile.Id = Convert.ToInt32(reader["Id"]);
                    profile.Name = reader["Название"].ToString();
                    profile.Description = reader["Описание"].ToString();
                    profiles.Add(profile);
                }
                reader.Close();
            }
            return profiles;
        }

        public List<Picket> GetPickets(int areaId)
        {
            List<Picket> pickets = new List<Picket>();

            string query = "SELECT * FROM Пикеты WHERE ПлощадьId = @AreaId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@AreaId", areaId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Picket picket = new Picket();
                    picket.Id = Convert.ToInt32(reader["Id"]);
                    picket.X = Convert.ToDouble(reader["КоординатаX"]);
                    picket.Y = Convert.ToDouble(reader["КоординатаY"]);
                    picket.RadiationLevel = Convert.ToDouble(reader["УровеньРадиации"]);
                    pickets.Add(picket);
                }
                reader.Close();
            }
            return pickets;
        }

        public List<ProfileCoordinate> GetProfileCoordinates(int profileId)
        {
            List<ProfileCoordinate> coordinates = new List<ProfileCoordinate>();

            string query = "SELECT * FROM КоординатыПрофиля WHERE ПрофильId = @ProfileId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProfileId", profileId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProfileCoordinate coordinate = new ProfileCoordinate();
                    coordinate.Id = Convert.ToInt32(reader["Id"]);
                    coordinate.X = Convert.ToDouble(reader["КоординатаX"]);
                    coordinate.Y = Convert.ToDouble(reader["КоординатаY"]);
                    coordinates.Add(coordinate);
                }
                reader.Close();
            }
            return coordinates;
        }
    }

}

