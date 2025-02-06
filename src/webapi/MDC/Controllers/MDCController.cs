using MDC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MDC.Controllers;

[ApiController]
[Route("[controller]")]
public class MDCController : ControllerBase
{
    DataContext _context;

    public MDCController(DataContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Upload")]
    public bool UploadTest(Patient patient)
    {
        try
        {
            // deserialize content:
            // assemble patient, judge if patient exists
            // assemble test, save
            if (!_context.Patients.Any(p => p.PatientId == patient.PatientId))
            {
                _context.Add(patient);
            }
            else
            {
                patient = _context.Patients.Single(p => p.PatientId == patient.PatientId);
            }

            if (patient.Tests.Any())
            {
                patient.Tests.ForEach(t =>
                {
                    if (!_context.Tests.Any(p => p.TestId == t.TestId))
                    {
                        _context.Tests.Add(t);
                    }
                });
            }

            _context.SaveChanges();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    [HttpGet]
    [Route("Tests")]
    public List<Test> GetTests()
    {
        return _context.Tests.ToList();
    }

    [HttpGet]
    [Route("Patients")]
    public List<Patient> GetPatients()
    {
        return _context.Patients
            .Include(p => p.Tests)
            .ThenInclude(t => t.TestFiles)
            .OrderByDescending(p => p.Id)
            .ToList();
    }

    [HttpGet]
    [Route("Patients/{keyword}")]
    public List<Patient> GetPatients(string keyword)
    {
        return _context.Patients
            .Where(p => p.PatientId.Contains(keyword) || p.Name.Contains(keyword))
            .Include(p => p.Tests)
            .ThenInclude(t => t.TestFiles)
            .ToList();
    }

    [HttpGet]
    [Route("Patient/{patientId}")]
    public Patient Get(string patientId)
    {
        return _context.Patients.SingleOrDefault(p => p.PatientId == patientId);
    }
}
