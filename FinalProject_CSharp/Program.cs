using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProject_CSharp
{

    public class Teacher
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string Section { get; set; }
    }

    class Program
    {
        class FileHandlingOperations
        {
            public void WriteData()
            {
                FileStream fs = new FileStream("D:\\vs\\Final Project - C#\\TeacherData.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                Teacher teacher = new Teacher();
                Console.Write("Enter Teacher Id : ");
                teacher.ID = Console.ReadLine();
                Console.Write("Enter Teacher Name : ");
                teacher.Name = Console.ReadLine();
                Console.Write("Enter Teacher Class : ");
                teacher.Class = Console.ReadLine();
                Console.Write("Enter Teacher Section : ");
                teacher.Section = Console.ReadLine();
                Console.WriteLine("-----------------------------");

                string teacherData1 = teacher.ID + "," + teacher.Name + "," + teacher.Class + "," + teacher.Section;
                sw.WriteLine(teacherData1);

                Console.WriteLine("The teacher content written to the file succefully");
                sw.Close();
                fs.Close();
            }
            public void StoreData()
            {
                try
                {
                    string fileData = File.ReadAllText("D:\\vs\\Final Project - C#\\TeacherData.txt");

                    //Store data to text file. 
                    using (FileStream fs = new FileStream("D:\\vs\\Final Project - C#\\TeacherData.txt", FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            // Add the variables' values to the text file.
                            Teacher teacher = new Teacher();
                            Console.Write("Enter Teacher Id : ");
                            teacher.ID = Console.ReadLine();
                            if (fileData.Contains(teacher.ID))
                            {
                                Console.WriteLine("This ID already exists");
                            }
                            else
                            {
                                Console.Write("Enter Teacher Name : ");
                                teacher.Name = Console.ReadLine();
                                Console.Write("Enter Teacher Class : ");
                                teacher.Class = Console.ReadLine();
                                Console.Write("Enter Teacher Section : ");
                                teacher.Section = Console.ReadLine();
                                Console.WriteLine("-----------------------------");
                                string teacherData1 = teacher.ID + "," + teacher.Name + "," + teacher.Class + "," + teacher.Section;
                                sw.WriteLine(teacherData1);
                                Console.WriteLine("The teacher content written to the file succefully");
                                sw.Close();
                                fs.Close();
                            }
                        }
                    }
                }

                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public void ReadData()
            {
                FileStream fs = new FileStream("D:\\vs\\Final Project - C#\\TeacherData.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                string str = sr.ReadLine();
                while (str != null)
                {
                    Console.WriteLine(str);
                    str = sr.ReadLine();
                }
                sr.Close();
                fs.Close();
            }
        }
        static void Main(string[] args)
        {
            {
                FileHandlingOperations FOP = new FileHandlingOperations();
                string path = "D:\\vs\\Final Project - C#\\TeacherData.txt";
                if (!File.Exists(path))
                {
                    FOP.WriteData();
                }
                else
                {
                    char MainOpt;
                    try
                    {
                        do
                        {
                            //Declaring the text file, then covert it into array.
                            string fileData = File.ReadAllText("D:\\vs\\Final Project - C#\\TeacherData.txt");
                            string[] arrData = fileData.Split("".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            //Split the data to four variables to deal with them individually.
                            List<Teacher> teacherList = new List<Teacher>();
                            foreach (string teacherRecord in arrData)
                            {
                                Teacher teacher = new Teacher();
                                string[] splitData = teacherRecord.Split(',');
                                teacher.ID = splitData[0];
                                teacher.Name = splitData[1];
                                teacher.Class = splitData[2];
                                teacher.Section = splitData[3];
                                teacherList.Add(teacher);
                            }
                            //The five main processes.
                            Console.WriteLine("1 - Store Teacher Data");
                            Console.WriteLine("2 - Sort Teacher Records");
                            Console.WriteLine("3 - Search about Teacher Information");
                            Console.WriteLine("4 - Update Teacher Records");
                            Console.WriteLine("5 - Retrieve Teacher Data");
                            Console.WriteLine("Please Select Your Main Choice : ");

                            int MainChoice = int.Parse(Console.ReadLine());
                            switch (MainChoice)
                            {
                                case 1:
                                    FOP.StoreData();
                                    break;

                                case 2:
                                    char opt;
                                    do
                                    {
                                        // Sorting Process 
                                        Console.WriteLine("1 - Sorting by ID");
                                        Console.WriteLine("2 - Sorting by Name");
                                        Console.WriteLine("Please Select Your Sort Choice: ");
                                        int choice = int.Parse(Console.ReadLine());

                                        switch (choice)
                                        {
                                            case 1:
                                                //Sort the records depend on teacher ID.
                                                var sortedlistID = teacherList.OrderBy(s => s.ID);

                                                foreach (var teacherRec in sortedlistID)
                                                {
                                                    Console.WriteLine(string.Format("{0} , {1} , {2} , {3}", teacherRec.ID, teacherRec.Name, teacherRec.Class, teacherRec.Section));
                                                }
                                                break;

                                            case 2:
                                                //Sort the records depend on teacher Name
                                                var sortedlistName = teacherList.OrderBy(s => s.Name);
                                                foreach (var teacherRec in sortedlistName)
                                                {
                                                    Console.WriteLine(string.Format("{0} , {1} , {2} , {3}", teacherRec.Name, teacherRec.ID, teacherRec.Class, teacherRec.Section));
                                                }
                                                break;

                                            default:
                                                Console.WriteLine("Invalid Choice!!");
                                                break;
                                        }
                                        Console.Write("Do you wish to Continue Sorting? (Y / N) : ");
                                        opt = char.Parse(Console.ReadLine());
                                    } while (opt == 'y' || opt == 'Y');
                                    break;

                                case 3:
                                    do
                                    {
                                        Console.WriteLine("1 - Searching by Teacher ID");
                                        Console.WriteLine("2 - Searching by Teacher Name");
                                        Console.WriteLine("Please Select Your Search choice : ");
                                        int choice = int.Parse(Console.ReadLine());

                                        switch (choice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter Teacher ID to search:");
                                                string searchID = Console.ReadLine();

                                                //Search by Teacher ID
                                                foreach (var teacherRec in teacherList)
                                                {
                                                    //Searching process depend on teacher ID
                                                    if (teacherRec.ID == searchID)
                                                    {
                                                        Console.WriteLine(string.Format("{0} , {1} , {2} , {3}", teacherRec.ID, teacherRec.Name, teacherRec.Class, teacherRec.Section));
                                                    }
                                                    if (!fileData.Contains(searchID))
                                                    {
                                                        Console.WriteLine("Not Found");
                                                        break;
                                                    }
                                                }
                                                break;

                                            case 2:
                                                Console.WriteLine("Enter Teacher Name to Search:");
                                                string searchName = Console.ReadLine();

                                                //Searching process depend on teacher Name.
                                                foreach (var teacherRec in teacherList)
                                                {                                                   
                                                    if (teacherRec.Name.Contains(searchName))
                                                    {

                                                        Console.WriteLine(string.Format("{0} , {1} , {2} , {3}", teacherRec.Name, teacherRec.ID, teacherRec.Class, teacherRec.Section));
                                                    }

                                                    if (!fileData.Contains(searchName))
                                                    {
                                                        Console.WriteLine("Not Found");
                                                        break;

                                                    }
                                                }

                                                break;

                                            default:
                                                Console.WriteLine("Invalid Choice!!");
                                                break;
                                        }
                                        Console.Write("Do you wish to Continue Searching? (Y / N) : ");
                                        opt = char.Parse(Console.ReadLine());
                                    } while (opt == 'y' || opt == 'Y');

                                    break;

                                case 4:
                                    do
                                    {
                                        //Redeclaring the file to get the last updated version of the file.
                                        string fileData2 = File.ReadAllText("D:\\vs\\Final Project - C#\\TeacherData.txt");
                                        Console.WriteLine("1 - Update The Name");
                                        Console.WriteLine("2 - Update The Class");
                                        Console.WriteLine("3 - Update The Section");
                                        Console.WriteLine("Please Select Your Update Choice : ");
                                        int choice = int.Parse(Console.ReadLine());

                                        switch (choice)
                                        {
                                            case 1:
                                                Console.WriteLine("Enter Teacher ID:");
                                                string TID1 = Console.ReadLine();

                                                //Updating process by using ID
                                                foreach (var teacherRec in teacherList)
                                                {
                                                    if (teacherRec.ID == TID1)
                                                    {
                                                        //Update teacher name.
                                                        Console.WriteLine("Enter the New Name:");
                                                        string newName = Console.ReadLine();
                                                        //Use Replace() method  to change the record. 
                                                        string newtext = fileData2.Replace(teacherRec.Name, newName);
                                                        File.WriteAllText("D:\\vs\\Final Project - C#\\TeacherData.txt", newtext);
                                                        Console.WriteLine("Successfully Updated.");
                                                    }
                                                    if (!fileData2.Contains(TID1))
                                                    {
                                                        Console.WriteLine("Not Found");
                                                        break;
                                                    }
                                                }

                                                break;

                                            case 2:
                                                Console.WriteLine("Enter Teacher ID:");
                                                string TID2 = Console.ReadLine();

                                                //Update teacher records, Then save the results in the same file.
                                                foreach (var teacherRec in teacherList)
                                                {
                                                    if (teacherRec.ID == TID2)
                                                    {
                                                        //Update teacher class.
                                                        Console.WriteLine("Enter the new class:");
                                                        string newClass = Console.ReadLine();

                                                        string newtext = fileData2.Replace(teacherRec.Class, newClass);
                                                        File.WriteAllText("D:\\vs\\Final Project - C#\\TeacherData.txt", newtext);
                                                        Console.WriteLine("Successfully Updated.");
                                                    }
                                                    if (!fileData2.Contains(TID2))
                                                    {
                                                        Console.WriteLine("Not Found");
                                                        break;
                                                    }
                                                }
                                                
                                                break;

                                            case 3:
                                                Console.WriteLine("Enter Teacher ID:");
                                                string TID3 = Console.ReadLine();

                                                foreach (var teacherRec in teacherList)
                                                {
                                                    if (teacherRec.ID == TID3)
                                                    {
                                                        //Update teacher section.
                                                        Console.WriteLine("Enter the new section:");
                                                        string newSection = Console.ReadLine();

                                                        string newtext = fileData2.Replace(teacherRec.Section, newSection);
                                                        File.WriteAllText("D:\\vs\\Final Project - C#\\TeacherData.txt", newtext);
                                                        Console.WriteLine("Successfully Updated.");
                                                    }

                                                    if (!fileData2.Contains(TID3))
                                                    {
                                                        Console.WriteLine("Not Found");
                                                        break;

                                                    }
                                                }

                                                break;

                                            default:
                                                Console.WriteLine("Invalid Choice!!");
                                                break;
                                        }

                                        Console.Write("Do you wish to Continue Updating? (Y / N) : ");
                                        opt = char.Parse(Console.ReadLine());
                                    } while (opt == 'y' || opt == 'Y');

                                    break;


                                case 5:
                                    FOP.ReadData();
                                    break;


                                default:
                                    Console.WriteLine("Invalid Choice!!");
                                    break;
                            }
                            Console.Write("Would you like to return to the main options? (Y / N) : ");
                            MainOpt = char.Parse(Console.ReadLine());
                        } while (MainOpt == 'y' || MainOpt == 'Y');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unknown Value ");
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            Console.ReadKey();
        }
    }
}
