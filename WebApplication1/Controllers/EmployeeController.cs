using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Repositories;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {

        readonly IRepository<Employee> employeeRepository;
        //injection de dépendance
        public EmployeeController(IRepository<Employee> empRepository)
        {

            employeeRepository = empRepository;

        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();

            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();

            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);

            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee e)
        {

            try
            {
                employeeRepository.Add(e);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(e);
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);
            if (employee == null)
            {
                return NotFound(); 
            }
            return View(employee);
            // il est utile de garantir que l'employé est bien récupéré avant de l'éditer.
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]    // Cette annotation indique que cette méthode répond à une requête HTTP de type POST, utilisée lorsque des données sont envoyées depuis un formulaire pour être traitées par le serveur, 
        [ValidateAntiForgeryToken]   // est une mesure de sécurité qui protège l'application contre les attaques CSRF (Cross-Site Request Forgery).
        public ActionResult Edit(int id, Employee newEmployee)
        {
            try
            {
                // Vérifier si l'employé existe dans le repository
                var employee = employeeRepository.FindByID(id);
                if (employee != null)  // Si l'employé est trouvé
                {
                    // Mettre à jour les informations de l'employé
                    employeeRepository.Update(id, newEmployee);

                    // Rediriger vers la page Index après la mise à jour
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si l'employé n'existe pas, retourner une page NotFound
                    return NotFound();
                }

                
            }
            catch
            {
                // En cas d'erreur, retourner la vue d'édition avec les données actuelles
                return View(newEmployee);
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            // Récupérer l'employé par son ID
            var employee = employeeRepository.FindByID(id);

            if (employee == null)
            {
                return NotFound(); // Si l'employé n'existe pas, retourner NotFound
            }
            return View(employee); // Retourner la vue de confirmation avec les infos de l'employé
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Récupérer l'employé par son ID
                var employee = employeeRepository.FindByID(id);

                if (employee != null)  // Si l'employé existe
                {
                    // Supprimer l'employé du repository
                    employeeRepository.Delete(id);

                    // Rediriger vers la liste des employés après suppression
                    return RedirectToAction(nameof(Index));

                    //add random comment 
                }
                else
                {
                    return NotFound(); // Si l'employé n'est pas trouvé
                }
               
            }
            catch
            {
                // En cas d'erreur, retourner la vue actuelle
                return View();
            }
        }

        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }




    }
}
