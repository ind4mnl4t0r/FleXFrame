using AutoMapper;
using FleXFrame.AcademicSuite.Interfaces.IRepositories;
using FleXFrame.AcademicSuite.Models;
using FleXFrame.UtilityHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateStudentAsync(Student student)
        {
            string newStudentID = student.StudentID ?? GenerateDefaultStudentID();

            student.StudentID = newStudentID;
            student.DateCreated = DateTime.Now;

            await _studentRepository.AddStudentAsync(student);

            return newStudentID;

        }

        // Default StudentID
        private string GenerateDefaultStudentID()
        {
            // Retrieve the latest user to determine the current sequence number
            var latestUser = _studentRepository.GetLastAddedStudentAsync();

            int sequenceNumber = 1; // Default sequence if no users exist yet

            if (latestUser?.Result != null)
            {
                // Parse the sequence part of the ID from the latest user
                var sequencePart = latestUser.Result.StudentID.Substring(latestUser.Result.StudentID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1; // Increment the sequence number
                }
            }

            // Generate a new unique ID using the provided pattern
            return IDGenerator.GenerateID("STD-{S}", sequenceNumber);
        }
    }
}
