using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAgenda.Core.Domain;
using MVCAgenda.Core.MVCAgendaManagement;
using MVCAgenda.Data.DataBaseManager;
using MVCAgenda.Service.Factories;
using MVCAgenda.Service.Patients;

namespace MVCAgenda.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AgendaContext _context;
        private readonly IPatientServices _patientServices;
        private readonly IAgendaViewsFactory _agendaViewsFactory;

        public PatientsController(AgendaContext context,
            IPatientServices patientServices, IAgendaViewsFactory agendaViewsFactory)
        {
            _context = context;
            _patientServices = patientServices;
            _agendaViewsFactory = agendaViewsFactory;
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Pacienti
        public async Task<IActionResult> Index(string SearchByName, string SearchByPhoneNumber, string SearchByEmail, bool includeBlackList = false, bool isDeleted = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Patient> query = _context.Patient; // aici incepe syntaxa SQL (da' in c#)

                if (isDeleted)
                    query = query.Where(p => p.Visible == 0); // filtram date daca e
                else
                    query = query.Where(p => p.Visible == 1);

                if (includeBlackList)
                    query = query.Where(p => p.Blacklist == 1);

                if (!string.IsNullOrEmpty(SearchByName))
                    query = query.Where(p => p.FirstName.Contains(SearchByName));

                if (!string.IsNullOrEmpty(SearchByPhoneNumber))
                    query = query.Where(p => p.PhonNumber.Contains(SearchByPhoneNumber));

                if (!string.IsNullOrEmpty(SearchByEmail))
                    query = query.Where(p => p.Mail.Contains(SearchByEmail));

                var pacienti = await query.ToListAsync(); // aici aducem datele despre pacienti prin sintaxa SQL

                var pacientiModel = pacienti // lista de pacient adusa
                    .Select(patient => _agendaViewsFactory.PreperePatientViewModel(patient))
                    .ToList();

                var model = new MVCAgendaViewsManager
                {
                    PatientsList = pacientiModel
                };

                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpPost]
        public string Blacklist(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public async Task<IActionResult> Blacklist(string SearchByName, string SearchByPhoneNumber, string SearchByEmail)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Index(SearchByName, SearchByPhoneNumber, SearchByEmail, includeBlackList: true);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public string Deleted(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public async Task<IActionResult> Deleted(string SearchStringNume, string SearchStringNumarDeTelefon, string SearchStringEmail)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Index(SearchStringNume, SearchStringNumarDeTelefon, SearchStringEmail, isDeleted: true);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        


        // GET: Pacienti/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var patient = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
                if (patient == null)
                {
                    return NotFound();
                }

                var patientViewModel = _agendaViewsFactory.PreperePatientViewModel(patient);

                return View(patientViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Pacienti/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(new Patient());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    //var persoane = await _context.Pacient
                    //        .Where(p => p.Nume.Contains(pacient.Nume))
                    //        .Where(p => p.Prenume.Contains(pacient.Prenume))
                    //        .Where(p => p.NrDeTelefon.Contains(pacient.NrDeTelefon))
                    //        .ToListAsync();

                    //if (persoane.Count >= 1)
                    //{
                    //    string msg = $"Exista un pacient cu numele: {pacient.Nume}, prenumele: {pacient.Prenume} si numarul de telefon: {pacient.NrDeTelefon}";
                    //    ModelState.AddModelError(string.Empty, msg);
                    //}
                    //else
                    //{
                    //    FisaPacient FisaPacientCurent = new FisaPacient();
                    //    _context.Add(FisaPacientCurent);
                    //    await _context.SaveChangesAsync();

                    //    int lastID = _context.FisaPacient.Count();
                    //    pacient.FisaPacientId = lastID;
                    //    _context.Add(pacient);
                    //    await _context.SaveChangesAsync();
                    //    return RedirectToAction(nameof(Index));
                    //}

                    Patient _patient = new Patient()
                    {
                        FirstName = patient.FirstName,
                        SecondName = patient.SecondName,
                        PhonNumber = patient.PhonNumber,
                        Mail = patient.Mail,
                        Blacklist = patient.Blacklist
                    };

                    string result = await _patientServices.CreatePatientAsync(_patient);
                    if(result == "Ok")
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, result); 
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Pacienti/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var patient = await _context.Patient.FindAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Patient patient)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id != patient.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(patient);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PatientExists(patient.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(patient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Pacienti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var _patient = await _context.Patient
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (_patient == null)
                {
                    return NotFound();
                }

                return View(_patient);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Pacienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var patient = await _context.Patient.FindAsync(id);
                patient.Visible = 0;
                _context.Patient.Update(patient);
                //_context.Pacient.Remove(pacient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
