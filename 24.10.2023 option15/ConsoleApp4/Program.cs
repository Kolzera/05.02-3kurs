using System;
using System.Collections.Generic;

namespace PatientManagement
{
    class Doctor
    {
        public string Name { get; set; }
        public List<Patient> Patients { get; set; }

        public Doctor(string name)
        {
            Name = name;
            Patients = new List<Patient>();
        }
    }

    class Patient
    {
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Patient(string name, DateTime appointmentDate)
        {
            Name = name;
            AppointmentDate = appointmentDate;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Doctor> doctors = new List<Doctor>();

            // Создаем несколько врачей
            Doctor doctor1 = new Doctor("Врач 1");
            Doctor doctor2 = new Doctor("Врач 2");

            doctors.Add(doctor1);
            doctors.Add(doctor2);

            // Записываем пациентов к врачам
            doctor1.Patients.Add(new Patient("Пациент 1", DateTime.Now));
            doctor1.Patients.Add(new Patient("Пациент 2", DateTime.Now.AddDays(1)));
            doctor2.Patients.Add(new Patient("Пациент 3", DateTime.Now.AddDays(2)));

            // Получаем информацию о записанных пациентах к конкретному врачу
            Console.WriteLine("Укажите номер врача (1 или 2):");
            int doctorNumber = Convert.ToInt32(Console.ReadLine());

            if (doctorNumber >= 1 && doctorNumber <= doctors.Count)
            {
                Doctor selectedDoctor = doctors[doctorNumber - 1];
                Console.WriteLine($"Пациенты, записанные к врачу {selectedDoctor.Name}:");
                foreach (Patient patient in selectedDoctor.Patients)
                {
                    Console.WriteLine(patient.Name);
                }
            }
            else
            {
                Console.WriteLine("Неверный номер врача.");
            }

            // Получаем информацию о количестве пациентов, принятых врачом за указанный период времени
            Console.WriteLine("Укажите начальную дату (в формате дд.мм.гггг):");
            DateTime startDate = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Укажите конечную дату (в формате дд.мм.гггг):");
            DateTime endDate = Convert.ToDateTime(Console.ReadLine());

            int totalPatients = 0;
            foreach (Doctor doctor in doctors)
            {
                foreach (Patient patient in doctor.Patients)
                {
                    if (patient.AppointmentDate >= startDate && patient.AppointmentDate <= endDate)
                    {
                        totalPatients++;
                    }
                }
            }

            Console.WriteLine($"Врачи приняли {totalPatients} пациентов за указанный период времени.");
        }
    }
}