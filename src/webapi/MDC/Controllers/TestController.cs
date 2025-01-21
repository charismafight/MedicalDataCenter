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
    [Route("[controller]/Upload")]
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
    [Route("[controller]/Tests")]
    public List<Test> GetTests()
    {
        return _context.Tests.ToList();
    }

    [HttpGet]
    [Route("[controller]/Patients")]
    public List<Patient> GetPatients()
    {
        return _context.Patients.Include(p => p.Tests).ToList();
    }

    [HttpGet]
    [Route("[controller]/Paitent/{patientId}")]
    public Patient Get(string patientId)
    {
        return _context.Patients.SingleOrDefault(p => p.PatientId == patientId);
    }
}
