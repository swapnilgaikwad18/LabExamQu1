using System.Data.SqlClient;
using System.Data;

namespace LabExamQu1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            bool request = true;

            while (request)
            {
                int val;
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("1: Insert a student details");
                Console.WriteLine("2: Display all student's Details.");
                Console.WriteLine("3: Update a student's Details.");
                Console.WriteLine("4: Delete a student's details.");
                Console.WriteLine("0: Exit");
                Console.Write("Select options from 0-4: ");
                val = Convert.ToInt32(Console.ReadLine());
                switch (val)
                {
                    //1: Insert a Customer.
                    case 1:
                        Console.WriteLine("Enter student's details to be inserted: (RollNo | Name | Marks)");
                        Student student1 = new Student();
                        Console.Write("Enter RollNo: ");
                        student1.RollNo = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        student1.Name = Console.ReadLine();
                        Console.Write("Enter Marks: ");
                        student1.Marks = Convert.ToInt32(Console.ReadLine());

                        InsertData(student1);
                        break;
                    case 2:
                        List<Student> allStudent = DisplayStudents();
                        Console.WriteLine("All Student's Details: ");
                        if (allStudent.Count > 0)
                        {
                            foreach (Student stu in allStudent)
                            {
                                Console.WriteLine("(RollNo | Name | Marks)");
                                Console.Write($"CustID: {stu.RollNo} | ");
                                Console.Write($"Name: {stu.Name} | ");
                                Console.Write($"marks: {stu.Marks} | ");
                                Console.WriteLine();
                            }
                        }
                        else
                    {
                            Console.WriteLine("No data available.");
                        }
                        break;
                    case 3:
                        Console.Write("Enter RollNo to be updated: ");
                        int RollNo = Convert.ToInt32(Console.ReadLine());

                        Student student2 = new Student();

                        Console.Write("Enter RollNo: ");
                        student2.RollNo = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        student2.Name = Console.ReadLine();
                        Console.Write("Enter Marks: ");
                        student2.Marks = Convert.ToInt32(Console.ReadLine());

                        updateStudent(student2);
                        break;
                    case 4:
                        Console.Write("Enter RollNo to be deleted: ");
                        int n = Convert.ToInt32(Console.ReadLine());
                        DeleteStudent(n);
                        break;
                    case 0:
                        request = false;
                        Console.WriteLine("Ended!");
                        break;
                    default:
                        Console.WriteLine("Option not Available. Please enter options between 0-4.");
                        break;
                }
            }
        }

        static void InsertData(Student student)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertStudent";
                cmd.Parameters.AddWithValue("@RollNo", student.RollNo);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Marks", student.Marks);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Students Details Inserted Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
            cn.Close();
            }
        }

        static List<Student> DisplayStudents()
        {
            List<Student> lstStu = new List<Student>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Students";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student student = new Student();
                    student.RollNo = dr.GetInt32("RollNo");
                    student.Name = dr.GetString("Name");
                    student.Marks = dr.GetInt32("Marks");
                    lstStu.Add(student);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
            return lstStu;
        }

        static void updateStudent(Student student)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Students SET Name = @Name, Marks = @Marks WHERE RollNo = @RollNo";
                cmd.Parameters.AddWithValue("@CustID", student.RollNo);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Marks", student.Marks);
             
                int check = cmd.ExecuteNonQuery();
                if (check > 0)
                {
                    Console.WriteLine("Student updated successfully...");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }


        static void DeleteStudent(int RollNo)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KTjune23;Integrated Security=True";
            cn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Students WHERE RollNo = @RollNo";
                cmd.Parameters.AddWithValue("@RollNo", RollNo);
                int check = cmd.ExecuteNonQuery();
                if (check > 0)
                {
                    Console.WriteLine("Student Deleted Successfully...");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

    }
    class Student
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public  int Marks { get; set; }
    }
}
