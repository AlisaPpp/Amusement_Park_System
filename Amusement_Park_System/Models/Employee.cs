namespace Amusement_Park_System.Models;

public abstract class Employee
    {
        protected Employee(string name, string surname, string contactInfo, DateTime birthDate, int yearsOfExperience)
        {
            Name = name;
            Surname = surname;
            ContactInfo = contactInfo;
            BirthDate = birthDate;
            YearsOfExperience = yearsOfExperience;

    
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty.");
                _name = value.Trim();
            }
        }

        private string _surname = "";
        public string Surname
        {
            get => _surname;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Surname cannot be empty.");
                _surname = value.Trim();
            }
        }

        private string _contactInfo = "";
        public string ContactInfo
        {
            get => _contactInfo;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("ContactInfo cannot be empty.");
                _contactInfo = value.Trim();
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value > DateTime.Now) throw new ArgumentException("BirthDate cannot be in the future.");
                if (value > DateTime.Now.AddYears(-18))
                    throw new ArgumentException("Employee must be at least 18 years old.");
                _birthDate = value;
            }
        }

        private int _yearsOfExperience;
        public int YearsOfExperience
        {
            get => _yearsOfExperience;
            set
            {
                if (value < 0) throw new ArgumentException("YearsOfExperience cannot be negative.");
                _yearsOfExperience = value;
            }
        }
        
        
    }