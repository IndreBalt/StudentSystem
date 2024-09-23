using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using StudentSystem.Database.Entitties;
using StudentSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Presentation
{
    public class UserUI
    {
        private IDepartmentsServise _departmentsServise;
        private IStudentsService _studentsService;
        private ILecturesService _lecturesService;

        public UserUI(IDepartmentsServise departmentsServise, IStudentsService studentsService, ILecturesService lecturesService)
        {
            _departmentsServise = departmentsServise;
            _studentsService = studentsService;
            _lecturesService = lecturesService;
        }
        public void Run()
        {
            MainMeniu();
            MainMeniuHandle();

        }



        #region ---- MAIN MENIU ----


        public void Title() 
        {
            Console.Clear();
            Console.WriteLine("----------------------------");
            Console.WriteLine("STUDENTU INFORMACINE SISTEMA");
            Console.WriteLine("----------------------------");

        }
        public void MainMeniu() //Spausdina Pradini meniu
        {
            do
            {
                Console.Clear();
                Title();
                Console.WriteLine("Pasirinkite");
                Console.WriteLine();
                Console.WriteLine("1. Departamentai");
                Console.WriteLine("2. Studentai");
                Console.WriteLine("3. Paskaitos");
                MainMeniuHandle();
            } while (true);

        }
        public void MainMeniuHandle() //Pradinio meniu switch
        {
            int.TryParse(Console.ReadLine(), out int mainMeniuSelect);
            switch (mainMeniuSelect)
            {
                case 1:
                    Console.WriteLine("Departamentai");
                    DepartmentMeniu(); // nukreipia i Departamento pagrindini meniu
                    break;
                case 2:
                    Console.WriteLine("Studentai");
                    StudentMeniu(); // nukreipia i Studento pagrindini meniu
                    break;
                case 3:
                    Console.WriteLine("Paskaitos");
                    LectureMeniu(); // nukreipia i Paskaitu pagrindini meniu
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
        }
        #endregion



        #region ---- DEPARTMENT ----


        public void DepartmentMeniu() // Spausdina Departamento pagrindini meniu
        {
            bool exit = true;
            do
            {
                Console.Clear();
                Title();
                Console.WriteLine("DEPARTAMENTAI");
                Console.WriteLine();
                Console.WriteLine("Pasirinkite");
                Console.WriteLine();
                Console.WriteLine("1. Sukurti departamenta");
                Console.WriteLine("2. Prideti studentus/paskaitas į departamenta");
                Console.WriteLine("3. Perziureti departamento studentus");
                Console.WriteLine("4. Perziureti Departamento paskaitas");
                Console.WriteLine();
                Console.WriteLine("Noredami iseiti spauskite X");
                exit = DepartmentMeniuHandle();
            
            } while (exit == true);
        }
        public bool DepartmentMeniuHandle() //Pradinio meniu switch
        {
            bool ok = true;
            string departmentMeniuSelect = Console.ReadLine().ToLower();
            switch (departmentMeniuSelect)
            {
                case "1":
                    Console.WriteLine("Sukurti Departamenta");
                    CreateDepartmentUI(); // nukreipia i Departamento kurimo UI
                    break;
                case "2":
                    Console.WriteLine("Prideti studentus/paskaitas į departamenta");
                    AddDepartmentStudentsAndLecturesUI(); // nukreipia i meniu, kur galima pasirinkti prie Departamento prideti Paskaitas arba Studentus
                    break;
                case "3":
                    Console.WriteLine("Perziureti departamento studentus");
                    PrintStudentsByDepartment(); // nukreipia kur galima pasirinkti Departamenta ir atspausdinami jo Studentai
                    break;
                case "4":
                    Console.WriteLine("Perziureti Departamento paskaitas");
                    PrintDepartmentsLecturesUI(); // nukreipia kur galima pasirinkti Departamenta ir atspausdinama jo Paskaitos
                    break;
                case "x": // iseinama is meniu
                    ok = false;
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
            return ok;
        }
        public Department CreateDepartmentUI() //Departamento kurimo UI
        {
            bool lengthValid = true;
            bool symbolsValid = true;
            string departmentCode = "";
            string departmentName = "";
            Department department = new Department();
            bool ifDeaprtmentExist = true;
            do
            {
                do
                {
                    Title();
                    Console.WriteLine("Sukurti Departamenta");
                    Console.WriteLine();
                    Console.WriteLine("Iveskite 6 simbolių departamento kodą");
                    departmentCode = Console.ReadLine().ToUpper();
                    lengthValid = InputsValidations.StringLength6AndNotEmpty(departmentCode); //Departamento kodo ilgio validacija
                    symbolsValid = InputsValidations.IsLettersAndNumbers(departmentCode); //Departamento kodo simboliu validacija
                } while (lengthValid == false || symbolsValid == false);

                do
                {
                    Title();
                    Console.WriteLine("Sukurti Departamenta");
                    Console.WriteLine();
                    Console.WriteLine($"Kodas: {departmentCode}");
                    Console.WriteLine("Iveskite Departamento pavadinima (3-100 simboliai)");
                    departmentName = Console.ReadLine();
                    lengthValid = InputsValidations.StringLengthFrom3To100AndNotEmpty(departmentName); //Departamento pavadinimo ilgio validacija
                    symbolsValid = InputsValidations.IsLettersAndNumbers(departmentName); //Departamento pavadinimo simboliu validacija
                } while (lengthValid == false || symbolsValid == false);
                ifDeaprtmentExist = _departmentsServise.IfDepartmentExist(departmentCode); //patikrina ar jau Departamentas egzistuoja
                if (ifDeaprtmentExist == false)
                {
                    department = _departmentsServise.CreateDepartment(departmentCode, departmentName);//iskvieciamas servisas Departamento sukurimui, veliau saugojimui DB
                }
                else
                {
                    Console.WriteLine("Departamentas su tokiu kodu jau egzistuoja");
                    Console.ReadKey();
                }

            } while (ifDeaprtmentExist == true);

            Title();
            Console.WriteLine("Sukurti Departamenta");
            Console.WriteLine();
            Console.WriteLine($"Kodas: {departmentCode}");
            Console.WriteLine($"Pavadinimas {departmentName}");
            Console.WriteLine();
            Console.WriteLine("DEPARTAMENTAS SEKMINGAI SUKURTAS");
            Console.WriteLine("Ar norite pridėti studentų/paskaitu į departamentą?(y/n)"); //Departamentas jau egzistuoja DB ir jam galima prideti Studentu/Paskaitu
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                AddDepartmentStudentsAndLecturesMeniu(department); //iskviecimas meniu prideti Departamentui Paskaitas/Studentus
            }
            else if (answer != "n")
            {
                Console.WriteLine("Netinkamas pasirinkimas");
            }
            return department;
        }
        public void AddDepartmentStudentsAndLecturesUI() //Meniu punkto prideti Departamentui Paskaitas/Studentus UI
        {
            Title();
            Console.WriteLine("Pasirinkite dapartamenta:");
            Department department = ChooseDepartmentFromListUI(); //grazinasmas Departamentas kuriam bus pridetos Paskaitos/Studentai
            if (department != null)
            {
                AddDepartmentStudentsAndLecturesMeniu(department);  //iskviecimas meniu prideti Departamentui Paskaitas/Studentus
            }
        }
        public Department ChooseDepartmentFromListUI() //UI grazina pasirinkta Departamenta is saraso
        {
            string departmentCodeInput = "";
            Department department = new Department();
            do
            {                
                Console.WriteLine();
                _departmentsServise.PrintAllDepartments(); //servisas kuris spausdina visus Departamentus, kurie yra DB
                Console.WriteLine();
                Console.WriteLine("Iveskite departamento koda. Noredami iseti spauskite X");
                departmentCodeInput = Console.ReadLine().ToUpper();               
                if(departmentCodeInput != "X")
                {
                    department = _departmentsServise.GetDepartmentByCode(departmentCodeInput); //servisas kuris grazina Departamenta pagal DepartmentCode
                    if (department == null) //tikrina ar Departamentas egzistuoja
                    {
                        Console.WriteLine("Tokio departamento nera");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Departamentas pasirinktas sekmingai");
                        Console.ReadKey();
                    }
                }
                
            } while (departmentCodeInput != "X" && department == null);
            return department;
        }
        public void PrintDepartmentsLecturesUI() //UI Departamento Paskaitu spausdinimui
        {
            Department department = ChooseDepartmentFromListUI(); //UI grazina Departamenta is saraso
            Title();
            Console.WriteLine("Departamento paskaitos:");
            Console.WriteLine();
            List<Lecture> lectures = _departmentsServise.PrintDepartmentsLectures(department); //servisas kuris spausdina pasirinkto departamento Paskaitas
            if(lectures == null) //tikrina ar Departamentas turi paskaitu
            {
                Console.WriteLine("Departamentui priskirtu paskaitu nera");
            }
            Console.WriteLine("Noredami iseitis spauskite bet koki mygtuka");
            Console.ReadKey();
        }
        public void AddDepartmentStudentsAndLecturesMeniu(Department department) //meniu prideti Paskaitoms/Studentams i parametrus paduotam Departamentui
        {
            bool result = true;
            do
            {
                Title();
                Console.WriteLine($"{department.DepartmentCode}, {department.DepartmentName}");
                Console.WriteLine("Pasirinkite");
                Console.WriteLine();
                Console.WriteLine("1. Prideti studenta");
                Console.WriteLine("2. Prideti paskaitu");
                Console.WriteLine();
                Console.WriteLine("Noredami iseiti spauskite X");
                result = AddDepartmentStudentsAndLecturesMeniuHandle(department); //switch Paskaitu/Studentu pridejimo i Departamenta, gazina bool ar dar rodyt meniu
            } while (result == true);

        }
        public bool AddDepartmentStudentsAndLecturesMeniuHandle(Department department) //switch Departamento pridjimo Paskaitu/Studentu
        {
            bool result = true;
            string meniuSelect = Console.ReadLine().ToLower();
            switch (meniuSelect)
            {
                case "1":
                    Console.WriteLine("Prideti studenta");
                    AddDepartmentStudentsMeniu(department); //pasirinkto Departamento Studento pridejimo meniu
                    break;
                case "2":
                    Console.WriteLine("Prideti paskaitu");
                    AddDepartmentLecturesMeniu(department); //pasirinkto Departamento Paskaitu pridejimo meniu
                    break;
                case "x":
                    result = false; //grazina i ankstesni Departamento meniu
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
            return result;
        }
        public void AddDepartmentLecturesMeniu(Department department) // spausdina i parametus paduoto Departamento Paskaitos pridejimo meniu
        {
            Title();
            Console.WriteLine($"{department.DepartmentCode}, {department.DepartmentName}");
            Console.WriteLine("Pasirinkite");
            Console.WriteLine();
            Console.WriteLine("1. Kurti paskaita");
            Console.WriteLine("2. Pasirinkti paskaita is saraso");
            Console.WriteLine();
            Console.WriteLine("Noredami iseiti spauskite X");
            AddDepartmentLectureMeniuHandle(department); // nukreipia i meniu switch
        }
        public void AddDepartmentLectureMeniuHandle(Department department) // i parametrus paduoto Departamento Paskaitos pridejimo maniu switch
        {
            string meniuSelect = Console.ReadLine().ToLower();
            switch (meniuSelect)
            {
                case "1":
                    Console.WriteLine("Sukurti Paskaita");
                    CreateLectureUI(department); // Paskaitos sukurimo i parametrus paduotam Departamentui UI
                    break;
                case "2": // Paskaitos is saraso paduotam i parametrus Departamentui pridejimo UI
                    Console.WriteLine("Pasirinkti paskaita is saraso");
                    Lecture lecture = ChooseLectureFromList(); //grazina pasirinkta Paskaita is visu paskaitu DB
                    if (lecture.LectureName != null)
                    {
                        AddLectureDepartment(department, lecture); //prideda Paskaita i parametrus paduotam Departamentui
                    }

                    break;
                case "x": //iseina is meniu
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
        }
        public void AddDepartmentStudentsMeniu(Department department) // spausdina i parametus paduoto Departamento Studento pridejimo meniu
        {
            Title();
            Console.WriteLine($"{department.DepartmentCode}, {department.DepartmentName}");
            Console.WriteLine("Pasirinkite");
            Console.WriteLine();
            Console.WriteLine("1. Kurti studenta");
            Console.WriteLine("2. Pasirinkti studenta is saraso");
            Console.WriteLine();
            Console.WriteLine("Noredami iseiti spauskite X");
            AddDepartmentStudentsMeniuHandle(department); //nukreipia i meniu switch
        }
        public void AddDepartmentStudentsMeniuHandle(Department department) // i parametrus paduoto Departamento Paskaitos pridejimo maniu switch
        {
            string meniuSelect = Console.ReadLine().ToLower();
            switch (meniuSelect)
            {
                case "1":
                    Console.WriteLine("Sukurti Studenta");
                    CreateStudentUI(department); //Studento sukurimo i parametrus paduotam Departamentui UI
                    break;
                case "2":// Studento is saraso paduotam i parametrus Departamentui pridejimo UI
                    Console.WriteLine("Pasirinkti studenta is saraso");
                    Student student = ChooseStudentFromList(); //grazina pasirinkta Studenta is saraso
                    if (student.StudentNumber != 0)
                    {
                        ChangeStudentDepartment(department, student); //Studentui priskiria i parametrus paduodta Departamenta
                    }
                    break;
                case "x": // grazina i ankstesni meniu
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
        }
        #endregion


        #region ---- STUDENT ----


        public void StudentMeniu() // spausdina pagrindini Studento meniu
        {
            bool exit = false;
            do
            {
                Console.Clear();
                Title();
                Console.WriteLine("STUDENTAI");
                Console.WriteLine();
                Console.WriteLine("Pasirinkite");
                Console.WriteLine();
                Console.WriteLine("1. Sukurti studenta");
                Console.WriteLine("2. Perkelti studenta i kita departamenta");
                Console.WriteLine("3. Perziureti studento paskaitas");
                Console.WriteLine();
                Console.WriteLine("Noredami iseiti spauskite X");
                exit = StudentMeniuHandle();
            } while (exit == false);
        }
        public bool StudentMeniuHandle() // pagrindinio Studento meniu switch
        {
            bool exit = false;
            string studentMeniuSelect = Console.ReadLine().ToLower();
            Student student = new Student();
            switch (studentMeniuSelect)
            {
                case "1":
                    Console.WriteLine("Sukurti studenta");
                    Title();
                    Console.WriteLine("Pasirinkite departamenta prie kurio norite prideti studenta:");
                    Department department = ChooseDepartmentFromListUI(); //grazina pasirinkta Departamenta prie kurio bus pridetas kuriamas Studentas
                    if(department.DepartmentCode != null)
                    {
                        CreateStudentUI(department); //Studento kurimo UI kuriam bus priskirtas i parametrus paduotas departamentas
                    }                   
                    break;
                case "2": //esamam Studentui priskiriamas egzistuojantis Departamentas
                    Console.WriteLine("Perkelti studenta i kita departamenta");
                    student = ChooseStudentFromList(); //grazina Studenta is DB esanciu studentu saraso
                    if(student.StudentNumber != 0)
                    {
                        ChangeStudentsDepartment(student); //paduoto i parametrus Studento Departamento keitimo UI
                    }
                    break;
                case "3":
                    Console.WriteLine("Perziureti studento paskaitas");
                    student = ChooseStudentFromList(); //pasirnekamas studentas is saraso DB
                    if(student.StudentNumber != 0)
                    {
                        PrintStudentsLectures(student); //spausdinamos i paramtrus paduoto Studento Paskaitos UI
                    }

                    break;
                case "x": //grazina i ankstesni meniu
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
            return exit;
        }
        public Student CreateStudentUI(Department department) //i parametrus paduoto Deparatmento Studento kurimo UI
        {
            bool lengthValid = true;
            bool symbolsValid = true;
            string firstName = "";
            string lastName = "";
            string studentNumberString = "";
            int studentNumberInt = 0;
            bool ifStudentExist = true;
            string email = "";
            Student student = new Student();
            do
            {
                do
                {
                    Title();
                    Console.WriteLine("Sukurti Studenta");
                    Console.WriteLine();
                    Console.WriteLine("Iveskite varda (2-50 raidziu)");
                    firstName = Console.ReadLine();
                    lengthValid = InputsValidations.StringLengthFrom2To50AndNotEmpty(firstName); //Studento vardo ilgio validacija
                    symbolsValid = InputsValidations.IsOnlyLetters(firstName); //Studento vardo simboliu validacija


                } while (lengthValid == false || symbolsValid == false);

                do
                {
                    Title();
                    Console.WriteLine("Sukurti Studenta");
                    Console.WriteLine();
                    Console.WriteLine($"Vardas: {firstName}");
                    Console.WriteLine("Iveskite pavarde (2-50 raidziu)");
                    lastName = Console.ReadLine();
                    lengthValid = InputsValidations.StringLengthFrom2To50AndNotEmpty(lastName); //Studento pavardes ilgio validacija
                    symbolsValid = InputsValidations.IsOnlyLetters(lastName); //Studento pavardes simboliu validacija

                } while (lengthValid == false || symbolsValid == false);

                do
                {
                    Title();
                    Console.WriteLine("Sukurti Studenta");
                    Console.WriteLine();
                    Console.WriteLine($"Vardas: {firstName}");
                    Console.WriteLine($"Pavarde: {lastName}");
                    Console.WriteLine("Iveskite studento numeri (8 skaitmenys)");
                    studentNumberString = (Console.ReadLine());
                    lengthValid = InputsValidations.StringLength8AndIsNotEmpty(studentNumberString); //Studento numerio ilgio validacija
                    symbolsValid = InputsValidations.IsOnlyNumbers(studentNumberString); //Studento numerio simboliu validacija
                    if (lengthValid != false && symbolsValid != false) // jei Studento kodo ivestis validi, parsina i int tipo kintamaji
                    {
                        studentNumberInt = int.Parse(studentNumberString);
                    }                        

                } while (lengthValid == false || symbolsValid == false);
                do
                {
                    Title();
                    Console.WriteLine("Sukurti Studenta");
                    Console.WriteLine();
                    Console.WriteLine($"Vardas: {firstName}");
                    Console.WriteLine($"Pavarde: {lastName}");
                    Console.WriteLine($"Studento numeris: {studentNumberInt}");
                    Console.WriteLine("Iveskite el.pasta");
                    email = (Console.ReadLine());

                    //truksta validacijos---------------------------------------------------

                } while (lengthValid == false || symbolsValid == false);

                ifStudentExist = _studentsService.IfStudentExist(studentNumberInt); //tikrina ar egzistuoja Studentas
                if (ifStudentExist == false)
                {
                    List<Lecture> departmentLectures = department.Lectures.ToList(); //priskiriamos Departamento Paskaitos
                    student = _studentsService.CreateStudent(firstName, lastName, studentNumberInt, email, department.DepartmentCode, departmentLectures); //sukuriamas studentas
                }
                else
                {
                    Console.WriteLine("Studentas su tokiu numeriu jau egzistuoja");
                    Console.ReadKey();
                }
            } while (ifStudentExist == true);

            Title();
            Console.WriteLine("Sukurti Studenta");
            Console.WriteLine();
            Console.WriteLine($"Vardas: {firstName}");
            Console.WriteLine($"Pavarde: {lastName}");
            Console.WriteLine($"Studento numeris: {studentNumberInt}");
            Console.WriteLine($"Epastas: {email}");
            Console.WriteLine();
            Console.WriteLine("STUDENTAS SUKURTAS SEKMINGAI");
            Console.ReadKey();
            return student;
        }        
        public Student ChangeStudentsDepartment(Student student)
        {
            Title();
            Console.WriteLine("Pasirinkite departamenta i kur norite pakeisti:");
            Department department = ChooseDepartmentFromListUI();
            student = _studentsService.AddStudentDepartmentAndDepartmentLectures(student, department);
            if (student.DepartmentCode == department.DepartmentCode)
            {
                Console.WriteLine("Depatramentas pakeistas sekmingai");
                Console.ReadKey();
            }
            return student;
        }
        public List<Lecture> PrintStudentsLectures(Student student)
        {
            Console.WriteLine($"Studento {student.FirstName}, {student.LastName}, {student.StudentNumber} ");
            Console.WriteLine("Paskaitos:");
            List<Lecture> studentLectures = _studentsService.PrintStudentLectures(student.StudentNumber);
            Console.WriteLine();
            Console.WriteLine("Noredami iseiti spauskite bet koki mygtuka");
            Console.ReadKey();
            return studentLectures;
        }
        public void PrintStudentsByDepartment()
        {
            Title();
            Console.WriteLine("Pasirinkite departamenta:");
            Department department = ChooseDepartmentFromListUI();
            Title();
            Console.WriteLine("Departamento studentai:");
            Console.WriteLine();
            List<Student> departmentStudents = _studentsService.PrintAllStudentsByDepartment(department);
            if(departmentStudents == null)
            {
                Console.WriteLine("Departamentui nera priskirta studentu");
            }
            Console.WriteLine("Noredami iseitis spauskite bet koki mygtuka");
            Console.ReadKey();
        }
        public Student ChooseStudentFromList() //Grazina pasirinkta studenta is saraso
        {
            string studentNumberInput = "";
            Student student = new Student();    
            do
            {
                Title();
                Console.WriteLine("Pasirinkite studenta:");
                Console.WriteLine();
                _studentsService.PrintAllStudentsWithDepartment();
                Console.WriteLine();
                Console.WriteLine("Iveskite studento numeri. Noredami iseti spauskite X");
                studentNumberInput = Console.ReadLine().ToUpper();
                if(studentNumberInput != "X")
                {
                    int.TryParse(studentNumberInput, out int studentNumberInt);
                    student = _studentsService.GetStudentByNumber(studentNumberInt);
                    if(student != null)
                    {
                        Console.WriteLine("Studentas pasirinktas sekmingai");
                        Console.ReadKey();
                    }
                    
                }                
                if (student == null && studentNumberInput != "X")
                {
                    Console.WriteLine("Tokio studento nera");
                    Console.ReadKey();
                };
            } while (studentNumberInput != "X" && student == null);
            return student;
        }
        public Student ChangeStudentDepartment(Department department, Student student)
        {
            _studentsService.AddStudentDepartmentAndDepartmentLectures(student, department);
            Console.WriteLine("Departamentas pakeistas/pidetas sekmingai");
            Console.ReadKey();
            return student;
        }
        #endregion


        #region ---- LECTURE ----


        public void LectureMeniu()
        {
            bool result = true;
            do
            {
                Console.Clear();
                Title();
                Console.WriteLine("PASKAITOS");
                Console.WriteLine();
                Console.WriteLine("Pasirinkite");
                Console.WriteLine();
                Console.WriteLine("1. Sukurti paskaita");
                Console.WriteLine("2. Paskaitu sarasas");
                Console.WriteLine();
                Console.WriteLine("Noredami iseiti spauskite X");
                result = LectureMeniuHandle();
            }while (result == true);            
        }
        public bool LectureMeniuHandle()
        {
            bool result = true;
            string lectureMeniuSelect = Console.ReadLine().ToLower();
            switch (lectureMeniuSelect)
            {
                case "1":
                    Console.WriteLine("Sukurti paskaita");
                    Title();
                    Console.WriteLine("Pasirinkite dapartamenta prie kurionorite prideti paskaita:");
                    Department department = ChooseDepartmentFromListUI();
                    CreateLectureUI(department);
                    break;
                case "2":
                    Console.WriteLine("Paskaitu sarasas");
                    PrintAllLecturesUI();
                    break;
                case "x":
                    result = false;
                    break;
                default:
                    Console.WriteLine("Tokio pasirinkimo nera, bandykit dar karta");
                    Console.ReadKey();
                    break;
            }
            return result;
        }
        public Lecture CreateLectureUI(Department department)
        {
            Lecture lecture = new Lecture();
            bool lengthValid = true;
            bool symbolsValid = true;
            string lectureName = "";
            bool ifLectureExist = false;
            TimeOnly lectureStartTime = new TimeOnly();
            TimeOnly lectureEndTime = new TimeOnly();
            do
            {
                do
                {
                    Title();
                    Console.WriteLine("Sukurti Paskaita");
                    Console.WriteLine();
                    Console.WriteLine("Iveskite ne maziau 5 raidziu paskaitos pavadinima");
                    lectureName = Console.ReadLine().ToLower();
                    lengthValid = InputsValidations.StringLengthNotLessThan5AndIsNotEmpty(lectureName);

                } while (lengthValid == false);
                do
                {
                    do
                    {
                        Title();
                        Console.WriteLine("Sukurti Paskaita");
                        Console.WriteLine();
                        Console.WriteLine($"Pavadinimas: {lectureName}");
                        Console.WriteLine("Iveskite pradzios laika (HH:mm:ss)");
                        lectureStartTime = InputsValidations.IsTimeOnlyValid(Console.ReadLine());

                    } while (lectureStartTime == default);
                    do
                    {
                        Title();
                        Console.WriteLine("Sukurti Paskaita");
                        Console.WriteLine();
                        Console.WriteLine($"Pavadinimas: {lectureName}");
                        Console.WriteLine($"Pradzios laikas: {lectureStartTime.ToString()}");
                        Console.WriteLine("Iveskite pabaigos laika (HH:mm:ss)");
                        lectureEndTime = InputsValidations.IsTimeOnlyValid(Console.ReadLine());            


                    } while (lectureEndTime == default);
                    if (lectureStartTime >= lectureEndTime)
                    {
                        Console.WriteLine("Netinkamas pradzios ir pabaigos laikas");
                        Console.ReadKey();
                    }
                } while (lectureStartTime >= lectureEndTime);


                ifLectureExist = _lecturesService.IfLectureExist(lectureName);
                if (ifLectureExist == false)
                {
                    lecture = _lecturesService.CreateLecture(lectureName, lectureStartTime, lectureEndTime, department);
                }
                else
                {
                    Console.WriteLine("Paskaita su tokiu pavadinimu jau egzistuoja");
                    Console.ReadKey();
                }

            } while (ifLectureExist == true);

            Title();
            Console.WriteLine("Sukurti Paskaita");
            Console.WriteLine();
            Console.WriteLine($"Pavadinimas: {lectureName}");
            Console.WriteLine($"Laikas: {lectureStartTime} - {lectureEndTime}");
            Console.WriteLine();
            Console.WriteLine("PASKAITA SEKMINGAI SUKURTA");
            Console.ReadKey();
            return lecture;
        }
        public void PrintAllLecturesUI()
        {
            Title();
            Console.WriteLine("PASKAITOS");
            _lecturesService.PrintAllLectures();
            Console.ReadKey();
            Console.WriteLine("Noredami iseiti spauskite bet koki mygtuka");
        }
        public Lecture AddDepartmentToLecture(Lecture lecture)
        {
            Department department = ChooseDepartmentFromListUI();
            lecture = _lecturesService.AddLectureDepartment(lecture, department);
            Console.WriteLine("Paskaita prideta sekmingai");
            Console.ReadKey();
            return lecture;
        }
        public Lecture ChooseLectureFromList() //Grazina pasirinkta paskaita is saraso
        {
            string lectureNameInput = "";
            Lecture lecture = new Lecture();
            do
            {
                Title();
                Console.WriteLine("Pasirinkite paskaita:");
                Console.WriteLine();
                _lecturesService.PrintAllLectures();
                Console.WriteLine();
                Console.WriteLine("Iveskite paskaitos pavadinima. Noredami iseti spauskite X");
                lectureNameInput = Console.ReadLine();                
                if (lectureNameInput != "X" && lectureNameInput != "x")
                {
                    lecture = _lecturesService.GetLectureByName(lectureNameInput);
                    if (lecture.LectureName == null && lectureNameInput != "X")
                    {
                        Console.WriteLine("Tokios paskaitos nera");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Paskaita pasirinkta sekmingai");
                        Console.ReadKey();
                    }
                }             

            } while (lectureNameInput != "X" && lectureNameInput != "x"  && lecture.LectureName == null);
            return lecture;
        }
        public Lecture AddLectureDepartment(Department department, Lecture lecture)
        {            
            return _lecturesService.AddLectureDepartment(lecture, department);
        }
        #endregion
    }
}
