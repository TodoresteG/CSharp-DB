namespace SoftJail.DataProcessor
{

    using Data;
    using ImportDto;
    using Data.Models;
    using Data.Models.Enums;

    using System;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using System.IO;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var departmentDtos = JsonConvert.DeserializeObject<ImportDepartmentDto[]>(jsonString);

            var departments = new List<Department>();

            foreach (var dto in departmentDtos)
            {
                if (!IsValid(dto) || !dto.Cells.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department 
                {
                    Name = dto.Name
                };

                foreach (var cellDto in dto.Cells)
                {
                    var cell = new Cell 
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow
                    };

                    department.Cells.Add(cell);
                }

                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var prisonerDtos = JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);

            var prisoners = new List<Prisoner>();

            foreach (var dto in prisonerDtos)
            {
                if (!IsValid(dto) || !dto.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                DateTime? releaseDate = null;

                var isNull = DateTime.TryParseExact(dto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

                if (isNull)
                {
                    releaseDate = result;
                }

                var prisoner = new Prisoner
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    IncarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = releaseDate,
                    Bail = dto.Bail,
                    CellId = dto.CellId
                };

                foreach (var mailDto in dto.Mails)
                {
                    var mail = new Mail 
                    {
                        Description = mailDto.Description,
                        Sender = mailDto.Sender,
                        Address = mailDto.Address
                    };

                    prisoner.Mails.Add(mail);
                }

                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportOfficerDto[]), new XmlRootAttribute("Officers"));

            ImportOfficerDto[] officerDtos;

            using (var reader = new StringReader(xmlString))
            {
                officerDtos = (ImportOfficerDto[])serializer.Deserialize(reader);
            }

            var officers = new List<Officer>();
            
            foreach (var dto in officerDtos)
            {
                var isPositionValid = Enum.TryParse(dto.Position, out Position positionResult);
                var isWeaponValid = Enum.TryParse(dto.Weapon, out Weapon weaponResult);

                if (!IsValid(dto) || !isPositionValid || !isWeaponValid)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = new Officer 
                {
                    FullName = dto.Name,
                    Salary = dto.Money,
                    Position = positionResult,
                    Weapon = weaponResult,
                    DepartmentId = dto.DepartmentId
                };

                foreach (var prisonerDto in dto.Prisoners)
                {
                    var officerPrisoner = new OfficerPrisoner 
                    {
                        Officer = officer,
                        PrisonerId = prisonerDto.Id
                    };

                    officer.OfficerPrisoners.Add(officerPrisoner);
                }

                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(officers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object entity) 
        {
            var context = new ValidationContext(entity);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(entity,context, results, true);
        }
    }
}